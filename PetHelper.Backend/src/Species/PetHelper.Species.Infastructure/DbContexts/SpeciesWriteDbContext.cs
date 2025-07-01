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
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("PetHelper_Species");

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SpeciesReadDbContext).Assembly,
            type => type.FullName?.ToLower().Contains("write.configuration") ?? false);
    }

    private ILoggerFactory CreateLoggerFactory() => 
        LoggerFactory.Create(builder => { builder.AddConsole(); });

}