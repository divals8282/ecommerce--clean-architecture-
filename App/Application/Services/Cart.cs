using App.Domain.Entities;
using App.Domain.Interfaces.Repositories;
using App.Domain.Interfaces.Services;

namespace App.Application.Services;

public class CartService : ICartService
{

    private readonly IProductRepository _productRepo;

    private readonly ICartRepository _cartRepo;

    public CartService(IProductRepository productRepo, ICartRepository cartRepo)
    {
        _productRepo = productRepo;
        _cartRepo = cartRepo;
    }

    public async Task<CartEntity?> GetCart(int identityId)
    {
        var cart = await _cartRepo.GetByIdentityId(identityId);

        return cart;
    }

    public async Task<bool> AddProduct(int identityId, int productId)
    {
        var cart = await _cartRepo.GetByIdentityId(identityId);
        var product = await _productRepo.GetByIdAsync(productId);

        if (cart != null && product != null)
        {
            cart.Products.Add(product);

            await _cartRepo.SaveChangesAsync();


            return true;
        }

        return false;
    }

    public async Task<bool> DeleteProduct(int identityId, int productId)
    {
        var cart = await _cartRepo.GetByIdentityId(identityId);
        var product = await _productRepo.GetByIdAsync(productId);


        if (cart != null && product != null)
        {
            cart.Products.Remove(product);

            await _cartRepo.SaveChangesAsync();


            return true;
        }

        return false;
    }

    public async Task<bool> DeleteCart(int identityId)
    {
        var cart = await _cartRepo.GetByIdentityId(identityId);

        if (cart != null)
        {
            await _cartRepo.RemoveCart(cart);

            return true;
        }

        return false;
    }
}