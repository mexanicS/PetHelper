using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PetHelper.Volunteer.Application;

namespace PetHelper.Volunteer.Infastructure.BackgroundServices;

public class FilesCleanerBackgroudService : BackgroundService
{
    private readonly ILogger<FilesCleanerBackgroudService> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    
    public FilesCleanerBackgroudService(
        ILogger<FilesCleanerBackgroudService> logger, 
        IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await using var scope = _serviceScopeFactory.CreateAsyncScope();
        var filesCleanerService = scope.ServiceProvider.GetRequiredService<IFilesCleanerService>();
        while (!stoppingToken.IsCancellationRequested)
        {
            await filesCleanerService.Process(stoppingToken);
        }
        
        await Task.CompletedTask;
    }
}