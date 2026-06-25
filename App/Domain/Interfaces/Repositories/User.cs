using App.Domain.Entities;

namespace App.Domain.Interfaces.Repositories;

public interface IUserRepository
{
    public Task<UserEntity?> GetByIdAsync(int id);

    public Task<UserEntity?> GetByFieldName(string fieldName, string value);

    public Task AddAsync(UserEntity user);

    public Task SaveChangesAsync();

}