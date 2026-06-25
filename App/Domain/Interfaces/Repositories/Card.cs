using App.Domain.Entities;

namespace App.Domain.Interfaces.Repositories;

public interface ICardRepository
{
    public Task<CardEntity?> GetByIdAsync(int id);

    public Task<CardEntity?> GetByIdentityId(int identityId);

    public Task<bool> RemoveCard(CardEntity card);

    public Task SaveChangesAsync();

}