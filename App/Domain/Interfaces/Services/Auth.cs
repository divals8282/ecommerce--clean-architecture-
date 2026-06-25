using App.Application.Enums.JWT;
using App.Domain.Entities;

namespace App.Application.Services;

public interface IAuthService
{
    public Task<bool> ComparePasswordAsync(UserEntity u, string cleanPassword);

    public Task<string> CreateTokenAsync(UserEntity user, ETokenType tokenType);
    public Task KillActualRefreshTokenAsync(UserEntity user);
}