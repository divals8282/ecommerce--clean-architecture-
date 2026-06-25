using App.Domain.Entities;
using App.Domain.Interfaces.Repositories;
using App.Infrastructure.Presistence;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Repositories;


public class ProductRepository : IProductRepository
{

    private readonly AppDbContext _db;

    public ProductRepository(AppDbContext db)
    {
        _db = db;
    }


    public async Task<List<ProductEntity>> GetList(int offset, int limit)
    {
        return await _db.Products.Skip(offset).Take(limit).ToListAsync();
    }

    public async Task<ProductEntity?> GetByIdAsync(int id)
    {
        return await _db.Products.FindAsync(id);
    }

    public async Task<bool> Add(ProductEntity product)
    {
        _db.Products.Add(product);

        await SaveChangesAsync();

        return true;
    }

    public async Task<bool> Delete(ProductEntity product)
    {
        _db.Products.Remove(product);

        await SaveChangesAsync();

        return true;
    }

    public async Task SaveChangesAsync()
    {
        await _db.SaveChangesAsync();
    }
}