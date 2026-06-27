using App.Domain.Entities;

namespace App.Domain.Interfaces.Repositories;

public interface ICheckoutRepository
{
    public Task<CheckoutEntity?> GetByIdAsync(int id);

    public Task<CheckoutEntity> AddNewCheckout(CheckoutEntity checkout);

    public Task<List<CheckoutEntity>> All();

    public Task<List<CheckoutEntity>> GetByUserId(int userId);

    public Task SaveChangesAsync();

}