using App.Domain.Entities;

namespace App.Domain.Interfaces.Services;

public interface ICardService
{
    public Task<CardEntity?> GetCard(int identityId);

    public Task<bool> AddProduct(int identityId, int productId);
    public Task<bool> DeleteProduct(int identityId, int productId);

    public Task<bool> DeleteCard(int identityId);
}