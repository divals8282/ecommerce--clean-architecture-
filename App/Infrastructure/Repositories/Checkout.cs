using App.Domain.Entities;
using App.Infrastructure.Presistence;

namespace App.Infrastructure.Repositories;


public class CheckoutRepository {

     private readonly AppDbContext _db;

    public CheckoutRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<CheckoutEntity?> GetByIdAsync(int id)
    {
        return await _db.Checkouts.FindAsync(id);
    }
}