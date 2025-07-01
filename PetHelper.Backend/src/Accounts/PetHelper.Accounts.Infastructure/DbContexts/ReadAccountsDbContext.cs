using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetHelper.Accounts.Application.Database;
using PetHelper.Accounts.Domain;
using PetHelper.SharedKernel;

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
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("PetHelper_Accounts");

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ReadAccountsDbContext).Assembly,
            type => type.FullName?.ToLower().Contains("read.configuration") ?? false);
    }
    
    private ILoggerFactory CreateLoggerFactory() =>
        LoggerFactory.Create(builder => {builder.AddConsole();});

}