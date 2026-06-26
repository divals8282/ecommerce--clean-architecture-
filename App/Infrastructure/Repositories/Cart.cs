using App.Domain.Entities;
using App.Domain.Interfaces.Repositories;
using App.Infrastructure.Presistence;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Repositories;


public class CartRepository : ICartRepository
{

    private readonly AppDbContext _db;

    public CartRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<CartEntity?> GetByIdAsync(int id)
    {
        return await _db.Carts.FindAsync(id);
    }

    public async Task<CartEntity?> GetByIdentityId(int identityId)
    {

        return await _db.Carts.Include((c) => c.Identity).FirstOrDefaultAsync((c) => c.Identity.Id == identityId);
    }

    public async Task<bool> RemoveCart(CartEntity cart)
    {
        _db.Carts.Remove(cart);
        await _db.SaveChangesAsync();

        return true;
    }

    public async Task SaveChangesAsync()
    {
        await _db.SaveChangesAsync();
    }
}