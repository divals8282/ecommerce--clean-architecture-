using App.Domain.Entities;
using App.Domain.Interfaces.Repositories;
using App.Infrastructure.Presistence;

namespace App.Infrastructure.Repositories;


public class IdentityRepository : IIdentityRepository
{

    private readonly AppDbContext _db;

    public IdentityRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IdentityEntity?> GetByIdAsync(int id)
    {
        return await _db.Identites.FindAsync(id);
    }

    public async Task SaveChangesAsync()
    {
        await _db.SaveChangesAsync();
    }
}