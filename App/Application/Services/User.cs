using App.Application.DTOS.Auth;
using App.Domain.Entities;
using App.Domain.Enums;
using System.Security.Claims;
using App.Infrastructure.Authentication.JWT;
using App.Domain.Interfaces.Repositories;
using App.Application.Enums.JWT;
using Microsoft.AspNetCore.Identity;

namespace App.Application.Services;

public class UserService : IUserService
{

    private PasswordHasher<object> hasher = new PasswordHasher<object>();
    private readonly IUserRepository _userRepo;
    private readonly IAuthService _authService;
    private readonly IConfiguration _config;

    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(IUserRepository userRepo, IAuthService authService, IConfiguration config, IHttpContextAccessor httpContextAccessor)
    {
        _userRepo = userRepo;
        _authService = authService;
        _config = config;
        _httpContextAccessor = httpContextAccessor;
    }

    private RJwtPayload SerializeJWTPayloadAuthUser()
    {
        var NameIdentifier = int.Parse(
            _httpContextAccessor.HttpContext!
                .User
                .FindFirst(ClaimTypes.NameIdentifier)!
                .Value);

        var Name = _httpContextAccessor.HttpContext!
                .User
                .FindFirst(ClaimTypes.Name)!
                .Value;

        var Role = _httpContextAccessor.HttpContext!
                .User
                .FindFirst(ClaimTypes.Role)!
                .Value;

        var AuthenticationMethod = _httpContextAccessor.HttpContext!
                .User
                .FindFirst(ClaimTypes.AuthenticationMethod)!
                .Value;

        return new RJwtPayload(NameIdentifier, Name, AuthenticationMethod, Role == "ACCESS" ? ETokenType.ACCESS : ETokenType.REFRESH);
    }

    public async Task<bool> SetNewRole(UserEntity user, RoleEnum role)
    {
        var u = await _userRepo.GetByIdAsync(user.Id);

        if (u != null)
        {
            u.Role = role;
            await _userRepo.SaveChangesAsync();

            return true;
        }

        return false;
    }

    public async Task<bool> RegisterUser(UserEntity user)
    {
        var hashedPassword = hasher.HashPassword(new(), user.Password);

        user.Password = hashedPassword;

        await _userRepo.AddAsync(user);
        await _userRepo.SaveChangesAsync();

        return true;
    }

    public async Task<SignInResponseDTO> Login(SignInRequestDTO user)
    {
        var u = await _userRepo.GetByFieldName("UserName", user.UserName);

        if (u == null)
        {
            return new SignInResponseDTO()
            {
                status = false,
                message = "Username or Password invalid"
            };
        }

        var isPasswordValid = await _authService.ComparePasswordAsync(u, user.Password);

        if (!isPasswordValid)
        {
            return new SignInResponseDTO()
            {
                status = false,
                message = "Username or Password invalid"
            };
        }

        var accessToken = await _authService.CreateTokenAsync(u, ETokenType.ACCESS);
        var refreshToken = await _authService.CreateTokenAsync(u, ETokenType.REFRESH);
       
        return new SignInResponseDTO()
        {
            status = true,
            message = "Success",
            data = {
                    accessToken = accessToken,
                    refreshToken = refreshToken
                }
        };
    }

    public bool CheckSuperSecretValidity(string superSecret)
    {
        var realSuperSecret = _config["SUPER_SECRET"];

        return superSecret == realSuperSecret;
    }

    public async Task<UserEntity?> GetCurrentUser()
    {
        var currentAuthUser = SerializeJWTPayloadAuthUser();

        var u = await _userRepo.GetByIdAsync(currentAuthUser.NameIdentifier);

        return u;
    }
}