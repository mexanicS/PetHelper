using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using PetHelper.Core.Abstractions;
using PetHelper.SharedKernel;

namespace PetHelper.Accounts.Infastructure.BackgroundServices;

public class SoftDeleteCleanupService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly SoftDeleteConfig _config;

    public SoftDeleteCleanupService(
        IServiceProvider serviceProvider,
        IServiceScopeFactory scopeFactory,
        IOptions<SoftDeleteConfig> config)
    {
        _serviceProvider = serviceProvider;
        _scopeFactory = scopeFactory;
        _config = config.Value;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            CleanupDeletedEntities(stoppingToken);
            await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
        }
    }
    
    private async void CleanupDeletedEntities(CancellationToken cancellationToken = default)
    {
        using var scope = _scopeFactory.CreateScope();
        var hardDeleteSoftDeletedEntitiesServices = 
            scope.ServiceProvider.GetServices<IHardDeleteEntitiesContract>();
            
        var hardDeleteTasks = hardDeleteSoftDeletedEntitiesServices
            .Select(task => task.HardDeleteExpiredEntities(cancellationToken));

        await Task.WhenAll(hardDeleteTasks);
    }
}