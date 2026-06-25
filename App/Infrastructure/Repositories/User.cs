using App.Domain.Entities;
using App.Domain.Interfaces.Repositories;
using App.Infrastructure.Presistence;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{

    private readonly AppDbContext _db;

    public UserRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<UserEntity?> GetByIdAsync(int id)
    {
        return await _db.Users.FindAsync(id);
    }

    public async Task<UserEntity?> GetByFieldName(string fieldName, string value)
    {
        var u = await _db.Users.FirstOrDefaultAsync((u) => EF.Property<string>(u, fieldName) == value);

        return u;
    }

    public async Task AddAsync(UserEntity user)
    {
        await _db.Users.AddAsync(user);
        await _db.SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _db.SaveChangesAsync();
    }
}