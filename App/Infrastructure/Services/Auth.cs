using App.Application.Authentication.JWT;
using App.Domain.Entities;
using App.Infrastructure.Authentication.JWT;
using App.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;

namespace App.Infrastructure.Services;

public class AuthService {
    private PasswordHasher<object> hasher = new PasswordHasher<object>();
    private readonly AuthRepository _authRepo;
    private readonly JwtTokenGenerator _jwt;
    

    public AuthService(AuthRepository authRepository, JwtTokenGenerator jwt) {
        _authRepo = authRepository;
        _jwt = jwt;
    }

    public async Task<bool> ComparePasswordAsync(UserEntity u, string cleanPassword) {
        var hashedPassword = hasher.HashPassword(new (), cleanPassword);
        var user = await _authRepo.GetByIdAsync(u.Id);

        if(user != null) {
            return user.Password == hashedPassword;
        }

        return false;
    }

    public async Task<string> CreateTokenAsync(UserEntity user, ETokenType tokenType) {
        if(tokenType == ETokenType.REFRESH) {
            var u = await _authRepo.GetByIdAsync(user.Id);

            if(u != null) {
                u.RefreshToken = _jwt.GenerateToken(u, tokenType);

                await _authRepo.SaveChangesAsync();


                return u.RefreshToken;
            }
        }
        
        return _jwt.GenerateToken(user, tokenType);
    }

    public async Task KillActualRefreshTokenAsync(UserEntity user) {
        var u = await _authRepo.GetByIdAsync(user.Id);

        if(u != null) {
            u.RefreshToken = "";
            await _authRepo.SaveChangesAsync();
        }
    }
}