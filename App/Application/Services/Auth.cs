using App.Application.Enums.JWT;
using App.Domain.Entities;
using App.Domain.Interfaces.Authentication.JWT;
using App.Domain.Interfaces.Repositories;
using App.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Identity;

namespace App.Application.Services;

public class AuthService : IAuthService
{
    private PasswordHasher<UserEntity> hasher = new PasswordHasher<UserEntity>();
    private readonly IAuthRepository _authRepo;
    private readonly IJwtTokenGenerator _jwt;


    public AuthService(IAuthRepository authRepository, IJwtTokenGenerator jwt)
    {
        _authRepo = authRepository;
        _jwt = jwt;
    }

    public async Task<bool> ComparePasswordAsync(UserEntity u, string cleanPassword)
    {
        var user = await _authRepo.GetByIdAsync(u.Id);

        if (user != null)
        {
            var verificationResult = hasher.VerifyHashedPassword(
                user,
                user.Password,
                cleanPassword
            );

            return verificationResult == PasswordVerificationResult.Success;
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