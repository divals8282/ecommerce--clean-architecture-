namespace App.Domain.Interfaces.Services;

public interface ICheckoutService
{
    public Task<bool> ArchivateCart(int identityId);
}