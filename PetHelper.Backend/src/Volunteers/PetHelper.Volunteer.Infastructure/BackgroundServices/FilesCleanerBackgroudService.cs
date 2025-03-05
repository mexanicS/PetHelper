using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PetHelper.Volunteer.Application;
using PetHelper.Volunteer.Infastructure.Files;

namespace PetHelper.Volunteer.Infastructure.BackgroundServices;

public class FilesCleanerBackgroudService : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    
    public FilesCleanerBackgroudService(
        IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await using var scope = _serviceScopeFactory.CreateAsyncScope();
        var filesCleanerService = scope.ServiceProvider.GetRequiredService<FilesCleanerService>();
        while (!stoppingToken.IsCancellationRequested)
        {
            await filesCleanerService.Process(stoppingToken);
        }
        
        await Task.CompletedTask;
    }
}