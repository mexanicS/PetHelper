using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace PetHelper.Species.Infastructure.DbContexts;

public class SpeciesWriteDbContext() : DbContext
{
    private readonly string _connectionString;
    
    public SpeciesWriteDbContext(string connectionString) : this()
    {
        _connectionString = connectionString;
    }
    public DbSet<Domain.Models.Species> Species { get; set; }
        
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_connectionString);
        optionsBuilder.UseSnakeCaseNamingConvention();
        optionsBuilder.UseLoggerFactory(CreateLoggerFactory());
        optionsBuilder.EnableSensitiveDataLogging();
            
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(SpeciesWriteDbContext).Assembly, 
            type => type.FullName?.Contains("Configurations.Write") ?? false);
        
        //modelBuilder.HasDefaultSchema("PetHelper_Species");
    }

    private ILoggerFactory CreateLoggerFactory() => 
        LoggerFactory.Create(builder => { builder.AddConsole(); });

}