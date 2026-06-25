using App.Application.DTOS.Auth;
using App.Domain.Entities;
using App.Domain.Enums;
using App.Infrastructure.Repositories;
using App.Infrastructure.Services;
using App.Application.Authentication.JWT;
using System.Security.Claims;
using App.Infrastructure.Authentication.JWT;
using App.Domain.Interfaces.Repositories;

namespace App.Application.Services;

public class UserService
{
    private readonly IUserRepository _userRepo;
    private readonly AuthService _authService;
    private readonly IConfiguration _config;

    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(IUserRepository userRepo, AuthService authService, IConfiguration config, IHttpContextAccessor httpContextAccessor)
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
        await _userRepo.AddAsync(user);
        await _userRepo.SaveChangesAsync();

        return true;
    }

    public async Task<object> Login(SignInRequestDTO user)
    {
        var u = await _userRepo.GetByFieldName("username", user.UserName);

        if (u == null)
        {
            return new { status = false, authTokens = new { } };
        }

        var isPasswordValid = await _authService.ComparePasswordAsync(u, user.Password);

        if (!isPasswordValid)
        {
            return new { };
        }

        var accessToken = await _authService.CreateTokenAsync(u, ETokenType.ACCESS);
        var refreshToken = await _authService.CreateTokenAsync(u, ETokenType.REFRESH);

        return new
        {
            accessToken,
            refreshToken
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