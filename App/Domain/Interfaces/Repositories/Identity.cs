using App.Domain.Entities;

namespace App.Domain.Interfaces.Repositories;

public interface IIdentityRepository
{
    public Task<IdentityEntity?> GetByIdAsync(int id);
    public Task<IdentityEntity> Add(IdentityEntity identity);
    public Task<bool> Remove(IdentityEntity identity);
    public Task SaveChangesAsync();

}