namespace App.Domain.Interfaces.Services;

public interface ICheckoutService
{
    public Task<bool> ArchivateCard(int identityId);
}