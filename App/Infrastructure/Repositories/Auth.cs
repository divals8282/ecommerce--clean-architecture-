using App.Domain.Entities;
using App.Infrastructure.Presistence;

namespace App.Infrastructure.Repositories;


public class AuthRepository {

     private readonly AppDbContext _db;

    public AuthRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<UserEntity?> GetByIdAsync(int id)
    {
        return await _db.Users.FindAsync(id);
    }
    
    public async Task SaveChangesAsync()
    {
        await _db.SaveChangesAsync();
    }
}