using App.Domain.Entities;
using App.Domain.Interfaces.Repositories;
using App.Domain.Interfaces.Services;

namespace App.Application.Services;


public class CheckoutService : ICheckoutService
{

    private readonly IProductRepository _productRepo;

    private readonly ICardRepository _cardRepo;

    private IIdentityRepository _identityRepo;

    private ICheckoutRepository _checkoutRepo;
    private UserService _userService;

    public CheckoutService(ICardRepository cardRepo, IIdentityRepository identityRepo, IProductRepository productRepo, UserService userService, ICheckoutRepository checkoutRepo)
    {
        _cardRepo = cardRepo;
        _identityRepo = identityRepo;
        _userService = userService;
        _checkoutRepo = checkoutRepo;
        _productRepo = productRepo;
    }


    public async Task<bool> ArchivateCard(int identityId)
    {
        var identity = await _identityRepo.GetByIdAsync(identityId);

        if (identity == null)
        {
            return false;
        }

        var card = await _cardRepo.GetByIdentityId(identity.Id);

        if (card == null)
        {
            return false;
        }

        if (card.Products.Count() == 0)
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
            Products = card.Products,
        };

        await _checkoutRepo.AddNewCheckout(newCheckout);


        return true;
    }
}