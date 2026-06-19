using App.Domain.Entities;
using App.Infrastructure.Presistence;

namespace App.Infrastructure.Repositories;

public class UserRepository {

     private readonly AppDbContext _db;

    public UserRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<UserEntity?> GetByIdAsync(int id)
    {
        return await _db.Users.FindAsync(id);
    }
}