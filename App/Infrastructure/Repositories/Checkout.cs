using App.Domain.Entities;
using App.Domain.Interfaces.Repositories;
using App.Infrastructure.Presistence;

namespace App.Infrastructure.Repositories;


public class CheckoutRepository : ICheckoutRepository
{

    private readonly AppDbContext _db;

    public CheckoutRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<CheckoutEntity?> GetByIdAsync(int id)
    {
        return await _db.Checkouts.FindAsync(id);
    }

    public async Task<CheckoutEntity> AddNewCheckout(CheckoutEntity checkout)
    {
        _db.Checkouts.Add(checkout);

        await SaveChangesAsync();

        return checkout;
    }

    public async Task SaveChangesAsync()
    {
        await _db.SaveChangesAsync();
    }
}