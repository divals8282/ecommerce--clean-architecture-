using App.Domain.Entities;
using App.Domain.Interfaces.Repositories;
using App.Domain.Interfaces.Services;

namespace App.Application.Services;

public class IdentityService : IIdentityService
{
    private IIdentityRepository _identityRepo;

    public IdentityService(IIdentityRepository identityRepo)
    {
        _identityRepo = identityRepo;
    }

    public async Task<IdentityEntity> CreateIdentity(CartEntity cart)
    {
        var newIdentity = new IdentityEntity()
        {
            Cart = cart
        };

        var identity = await _identityRepo.Add(newIdentity);
        
        await _identityRepo.SaveChangesAsync();

        return identity;
    }

    public async Task<bool> DeleteIdentity(string? identityId)
    {
        if(identityId == null)
        {
            return false;
        } 

        var identity = await _identityRepo.GetByIdAsync(int.Parse(identityId));

        if (identity == null)
        {
            return false;
        }

        await _identityRepo.Remove(identity);
        await _identityRepo.SaveChangesAsync();

        return true;
    }

    public async Task<IdentityEntity?> GetIdentityById(int identityId)
    {
        return await _identityRepo.GetByIdAsync(identityId);
    }
}