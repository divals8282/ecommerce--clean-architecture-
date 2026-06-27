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
        return await _db.Carts.Include(c => c.Products).FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<bool> AddProduct(int cartId, ProductEntity product)
    {
        var cart = await _db.Carts.Include(c => c.Products).FirstOrDefaultAsync(c => c.Id == cartId);

        if (cart == null)
        {
            return false;
        }

        cart.Products.Add(product);
        cart.LastUpdated = DateTime.UtcNow;

        await _db.SaveChangesAsync();

        return true;
    }
    
    public async Task<bool> DeleteProduct(int cartId, ProductEntity product)
    {
        var cart = await _db.Carts.Include(c => c.Products).FirstOrDefaultAsync(c => c.Id == cartId);

        if (cart == null)
        {
            return false;
        }

        cart.Products.Remove(product);
        cart.LastUpdated = DateTime.UtcNow;
        
        await _db.SaveChangesAsync();
        
        return true;
    }

    public async Task<CartEntity?> GetByAnoUserId(int anoUserId)
    {
        return await _db.Carts
            .Include((c) => c.AnoUser)
            .Include((c) => c.Products)
            .FirstOrDefaultAsync((c) => c.AnoUser.Id == anoUserId);
    }

    public async Task<bool> RemoveCart(CartEntity cart)
    {
        _db.Carts.Remove(cart);
        await _db.SaveChangesAsync();

        return true;
    }

    public async Task<List<CartEntity>> GetInactiveCartsAsync()
    {
        return await _db.Carts
            .Include(c => c.Products)
            .Where(c => c.LastUpdated < new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddDays(-7))
            .ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _db.SaveChangesAsync();
    }
}