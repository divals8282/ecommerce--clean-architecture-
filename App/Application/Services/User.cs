using App.Application.DTOS.Auth;
using App.Domain.Entities;
using System.Security.Claims;
using App.Infrastructure.Authentication.JWT;
using App.Domain.Interfaces.Repositories;
using App.Application.Enums.JWT;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using App.Domain.Interfaces.Services;
using App.Domain.Enums;

namespace App.Application.Services;

public class UserService : IUserService
{

    private PasswordHasher<object> hasher = new PasswordHasher<object>();
    private readonly IUserRepository _userRepo;
    private readonly IAuthService _authService;

    private readonly IHttpContextAccessor _httpContextAccessor;

    private readonly IOptions<AppOptions> _appOptions;

    public UserService(IUserRepository userRepo, IAuthService authService, IHttpContextAccessor httpContextAccessor, IOptions<AppOptions> appOptions)
    {
        _userRepo = userRepo;
        _authService = authService;
        _appOptions = appOptions;
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

    public async Task<bool> SetNewRole(UserEntity user, ERole role)
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
        var hashedPassword = hasher.HashPassword(user, user.Password);

        user.Password = hashedPassword;

        await _userRepo.AddAsync(user);
        await _userRepo.SaveChangesAsync();

        return true;
    }

    public async Task<string[]?> Login(SignInRequestDTO user)
    {
        var u = await _userRepo.GetByFieldName("UserName", user.UserName);

        if (u == null)
        {
            return null;
        }

        var isPasswordValid = await _authService.ComparePasswordAsync(u, user.Password);
        if (!isPasswordValid)
        {
            return null;
        }
        
        var accessToken = await _authService.CreateTokenAsync(u, ETokenType.ACCESS);
        var refreshToken = await _authService.CreateTokenAsync(u, ETokenType.REFRESH);
       
        return [accessToken, refreshToken];
    }

    public bool CheckSuperSecretValidity(string providedSuperSecret)
    {
        return providedSuperSecret == _appOptions.Value.SUPER_SECRET;
    }

    public async Task<UserEntity?> GetCurrentUser()
    {
        var currentAuthUser = SerializeJWTPayloadAuthUser();

        var u = await _userRepo.GetByIdAsync(currentAuthUser.NameIdentifier);


        return u;
    }
}