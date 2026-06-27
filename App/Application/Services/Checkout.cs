using App.Domain.Entities;
using App.Domain.Enums;
using App.Domain.Interfaces.Repositories;
using App.Domain.Interfaces.Services;

namespace App.Application.Services;


public class CheckoutService : ICheckoutService
{
    private readonly IProductRepository _productRepo;

    private readonly ICartRepository _cartRepo;

    private IIdentityRepository _identityRepo;

    private ICheckoutRepository _checkoutRepo;
    private IUserService _userService;

    public CheckoutService(ICartRepository cartRepo, IIdentityRepository identityRepo, IProductRepository productRepo, IUserService userService, ICheckoutRepository checkoutRepo)
    {
        _cartRepo = cartRepo;
        _identityRepo = identityRepo;
        _userService = userService;
        _checkoutRepo = checkoutRepo;
        _productRepo = productRepo;
    }


    public async Task<bool> ArchivateCart(int identityId)
    {
        var identity = await _identityRepo.GetByIdAsync(identityId);

        if (identity == null)
        {
            return false;
        }

        var cart = await _cartRepo.GetByIdentityId(identity.Id);

        if (cart == null)
        {
            return false;
        }

        if (cart.Products.Count() == 0)
        {
            return false;
        }

        var user = await _userService.GetCurrentUser();

        if (user == null)
        {

            return false;
        }

        var newCheckout = new CheckoutEntity()
        {
            User = user,
            Products = cart.Products,
        };

        await _checkoutRepo.AddNewCheckout(newCheckout);


        return true;
    }

    public async Task<List<CheckoutEntity>?> List(int identityId)
    {
        var identity = await _identityRepo.GetByIdAsync(identityId);

        if (identity == null)
        {
            return null;
        }

        var user = await _userService.GetCurrentUser();

        if (user == null)
        {
            return null;
        }

        if(user.Role == ERole.CONTENT_MANAGER)
        {
            var allCheckouts = await _checkoutRepo.All();

            return allCheckouts;
        }

        var userCheckouts = await _checkoutRepo.GetByUserId(user.Id);

        return userCheckouts;
    }
}