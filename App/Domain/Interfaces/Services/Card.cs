using App.Domain.Entities;

namespace App.Domain.Interfaces.Services;

public interface ICartService
{
    public Task<CartEntity?> GetCart(string? identityId);

    public Task<int?> AddProduct(string? identityId, int productId);
    public Task<bool> DeleteProduct(string? identityId, int productId);
    public Task<bool> DeleteInactiveCartsAsync();

    public Task<bool> DeleteCart(string? identityId);
}