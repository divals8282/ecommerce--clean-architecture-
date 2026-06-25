using App.Domain.Entities;

namespace App.Domain.Interfaces.Repositories;

public interface IProductRepository
{
    public Task<List<ProductEntity>> GetList(int offset, int limit);

    public Task<ProductEntity?> GetByIdAsync(int id);

    public Task<bool> Add(ProductEntity product);

    public Task<bool> Delete(ProductEntity product);

    public Task SaveChangesAsync();

}