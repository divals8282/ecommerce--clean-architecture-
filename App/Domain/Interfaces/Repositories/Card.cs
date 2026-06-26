using App.Domain.Entities;

namespace App.Domain.Interfaces.Repositories;

public interface ICartRepository
{
    public Task<CartEntity?> GetByIdAsync(int id);

    public Task<CartEntity?> GetByIdentityId(int identityId);

    public Task<bool> RemoveCart(CartEntity cart);

    public Task SaveChangesAsync();

}