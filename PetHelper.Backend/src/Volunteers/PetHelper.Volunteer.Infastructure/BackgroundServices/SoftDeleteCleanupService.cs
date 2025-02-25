using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using PetHelper.Volunteer.Infastructure.DbContexts;

namespace PetHelper.Volunteer.Infastructure.BackgroundServices;

public class SoftDeleteCleanupService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ISoftDeleteConfig _config;

    public SoftDeleteCleanupService(
        IServiceProvider serviceProvider,
        IOptions<SoftDeleteConfig> config)
    {
        _serviceProvider = serviceProvider;
        _config = config.Value;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await CleanupDeletedEntities();
                await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
            }
            catch (Exception ex)
            {
            }
        }
    }
    
    private async Task CleanupDeletedEntities()
    {
        using var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<VolunteerWriteDbContext>();
        
        var thresholdDate = DateTime.UtcNow.AddDays(-_config.DeletedEntitiesRetentionDays);
        
        await dbContext.SaveChangesAsync();
    }
}



public interface ISoftDeleteConfig
{
    int DeletedEntitiesRetentionDays { get; }
}

public class SoftDeleteConfig : ISoftDeleteConfig
{
    public int DeletedEntitiesRetentionDays { get; set; }
}