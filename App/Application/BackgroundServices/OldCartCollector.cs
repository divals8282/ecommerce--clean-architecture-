using App.Domain.Interfaces.Services;

namespace App.Application.BackgroundServices;

public class OldCartCollectorBackgroundService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;

    public OldCartCollectorBackgroundService(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var now = DateTime.Now;
            var nextRun = now.Date.AddDays(1);

            var delay = nextRun - now;

            await Task.Delay(delay, stoppingToken);

            using var scope = _scopeFactory.CreateScope();

            var CartService = scope.ServiceProvider.GetRequiredService<ICartService>();
    
            await CartService.DeleteInactiveCartsAsync();
        }
    }
}