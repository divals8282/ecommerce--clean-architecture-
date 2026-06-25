using App.Domain.Entities;
using App.Infrastructure.Repositories;

namespace App.Application.Services;

public class CardService {

    private ProductRepository _productRepo;
    
    private CardRepository _cardRepo;

    public CardService(CardRepository cardRepo, IdentityRepository identityRepo, ProductRepository productRepo) {
        _cardRepo = cardRepo;
        _productRepo = productRepo;
    }

    public async Task<CardEntity?> GetCard(int identityId) {
        var card = await _cardRepo.GetByIdentityId(identityId);

        return card;
    }

    public async Task<bool> AddProduct(int identityId, int productId) {
        var card = await _cardRepo.GetByIdentityId(identityId);
        var product = await _productRepo.GetByIdAsync(productId);
        
        if(card != null && product != null) {
            card.Products.Add(product);

            await _cardRepo.SaveChangesAsync();


            return true;
        }

        return false;
    }

    public async Task<bool> DeleteProduct(int identityId, int productId) {
        var card = await _cardRepo.GetByIdentityId(identityId);
        var product = await _productRepo.GetByIdAsync(productId);


        if(card != null && product != null) {
            card.Products.Remove(product);

            await _cardRepo.SaveChangesAsync();


            return true;
        }

        return false;
    }

    public async Task<bool> DeleteCard(int identityId) {
        var card = await _cardRepo.GetByIdentityId(identityId);

        if(card != null) {
            await _cardRepo.RemoveCard(card);

            return true;
        }
        
        return false;
    }
}