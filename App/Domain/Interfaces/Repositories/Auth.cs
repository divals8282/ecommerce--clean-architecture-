using App.Domain.Entities;

namespace App.Domain.Interfaces.Repositories;

public interface IAuthRepository
{
    public Task<UserEntity?> GetByIdAsync(int id);

    public Task SaveChangesAsync();
}
