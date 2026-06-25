using App.Application.Authentication.JWT;
using App.Application.Enums.JWT;
using App.Domain.Entities;
using App.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Identity;

namespace App.Application.Services;

public class AuthService : IAuthService
{
    private PasswordHasher<object> hasher = new PasswordHasher<object>();
    private readonly IAuthRepository _authRepo;
    private readonly IJwtTokenGenerator _jwt;


    public AuthService(IAuthRepository authRepository, IJwtTokenGenerator jwt)
    {
        _authRepo = authRepository;
        _jwt = jwt;
    }

    public async Task<bool> ComparePasswordAsync(UserEntity u, string cleanPassword)
    {
        var hashedPassword = hasher.HashPassword(new(), cleanPassword);
        var user = await _authRepo.GetByIdAsync(u.Id);

        if (user != null)
        {
            return user.Password == hashedPassword;
        }

        return false;
    }

    public async Task<string> CreateTokenAsync(UserEntity user, ETokenType tokenType)
    {
        if (tokenType == ETokenType.REFRESH)
        {
            var u = await _authRepo.GetByIdAsync(user.Id);

            if (u != null)
            {
                u.RefreshToken = _jwt.GenerateToken(u, tokenType);

                await _authRepo.SaveChangesAsync();


                return u.RefreshToken;
            }
        }

        return _jwt.GenerateToken(user, tokenType);
    }

    public async Task KillActualRefreshTokenAsync(UserEntity user)
    {
        var u = await _authRepo.GetByIdAsync(user.Id);

        if (u != null)
        {
            u.RefreshToken = "";
            await _authRepo.SaveChangesAsync();
        }
    }
}