using App.Domain.Entities;
using App.Domain.Interfaces.Repositories;
using App.Domain.Interfaces.Services;

namespace App.Application.Services;

public class CartService : ICartService
{
    private readonly IProductRepository _productRepo;
    private readonly IIdentityService _identityService;

    private readonly ICartRepository _cartRepo;

    public CartService(IProductRepository productRepo, ICartRepository cartRepo, IIdentityService identityService)
    {
        _productRepo = productRepo;
        _cartRepo = cartRepo;
        _identityService = identityService;
    }

    public async Task<CartEntity?> GetCart(string? identityId)
    {
        if (identityId == null)
        {
            return null;
        }

        var cart = await _identityService.GetCart(int.Parse(identityId));

        return cart;
    }

    public async Task<int?> AddProduct(string? identityId, int productId)
    {
        var product = await _productRepo.GetByIdAsync(productId);

        if (product == null)
        {
            return null;
        }

        if (identityId == null)
        {
            var newCart = new CartEntity();
            var newIdentity = await _identityService.CreateIdentity(newCart);

            await _cartRepo.AddProduct(newCart.Id, product);
            await _cartRepo.SaveChangesAsync();

            return newIdentity.Id;
        }

        var cart = await _cartRepo.GetByIdentityId(int.Parse(identityId));

        if (cart == null)
        {
            return null;
        }

        await _cartRepo.AddProduct(cart.Id, product);

        return int.Parse(identityId);
    }

    public async Task<bool> DeleteProduct(string? identityId, int productId)
    {
        if (identityId == null)
        {
            return false;
        }

        var cart = await _cartRepo.GetByIdentityId(int.Parse(identityId));
        var product = await _productRepo.GetByIdAsync(productId);


        if (cart == null || product == null)
        {
            return false;
        }

        await _cartRepo.DeleteProduct(cart.Id, product);
        await _cartRepo.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteCart(string? identityId)
    {
        if (identityId == null)
        {
            return false;
        }

        var cart = await _cartRepo.GetByIdentityId(int.Parse(identityId));

        if (cart == null)
        {
            return false;
        }

        await _cartRepo.RemoveCart(cart);
        await _cartRepo.SaveChangesAsync();
        return true;
    }
}