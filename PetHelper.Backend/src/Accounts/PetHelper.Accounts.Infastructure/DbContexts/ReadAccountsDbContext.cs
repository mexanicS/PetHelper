using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetHelper.Accounts.Application.Database;

namespace PetHelper.Accounts.Infastructure.DbContexts;

public class ReadAccountsDbContext(string connectionString) : DbContext, IAccountsReadDbContext
{
    
    //Models
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(connectionString);
        optionsBuilder.UseSnakeCaseNamingConvention();
        optionsBuilder.UseLoggerFactory(CreateLoggerFactory());
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ReadAccountsDbContext).Assembly,
            x => x.FullName!.Contains("Configurations.Read"));
        //modelBuilder.HasDefaultSchema("PetHelper_Accounts");
    }
    
    private ILoggerFactory CreateLoggerFactory() =>
        LoggerFactory.Create(builder => {builder.AddConsole();});
}