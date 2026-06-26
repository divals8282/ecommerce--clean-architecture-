using App.Domain.Entities;

namespace App.Domain.Interfaces.Services;

public interface ICartService
{
    public Task<CartEntity?> GetCart(int identityId);

    public Task<bool> AddProduct(int identityId, int productId);
    public Task<bool> DeleteProduct(int identityId, int productId);

    public Task<bool> DeleteCart(int identityId);
}