using App.Domain.Entities;

namespace App.Domain.Interfaces.Services;

public interface IIdentityService
{
    public Task<IdentityEntity> CreateIdentity(CartEntity cart);
    public Task<bool> DeleteIdentity(string? identityId);

    public Task<IdentityEntity?> GetIdentityById(int identityId);
}