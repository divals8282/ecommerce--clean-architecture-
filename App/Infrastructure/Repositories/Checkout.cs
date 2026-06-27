using App.Domain.Entities;
using App.Domain.Interfaces.Repositories;
using App.Infrastructure.Presistence;
using Microsoft.EntityFrameworkCore;

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

    public async Task<List<CheckoutEntity>> All()
    {
        return await _db.Checkouts.Include(c => c.Products).Include(c => c.User).ToListAsync();
    }

    public async Task<List<CheckoutEntity>> GetByUserId(int userId)
    {
        return await _db.Checkouts.Include(c => c.Products).Where(c => c.User.Id == userId).ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _db.SaveChangesAsync();
    }
}