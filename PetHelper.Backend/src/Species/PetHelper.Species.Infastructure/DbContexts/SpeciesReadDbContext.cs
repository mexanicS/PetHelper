using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetHelper.Core.DTOs.ReadDtos;
using PetHelper.Species.Application.Database;

namespace PetHelper.Species.Infastructure.DbContexts;


public class SpeciesReadDbContext(string ConnectionString) : DbContext, IReadDbContext
{
    private const string DataBaseName = nameof(Database);
    
    public IQueryable<SpeciesDto> Species => Set<SpeciesDto>();
    
    public IQueryable<BreedDto> Breeds => Set<BreedDto>();
        
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(ConnectionString);
        optionsBuilder.UseSnakeCaseNamingConvention();
        optionsBuilder.UseLoggerFactory(CreateLoggerFactory());
        optionsBuilder.EnableSensitiveDataLogging();
        
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(SpeciesReadDbContext).Assembly, 
            type => type.FullName?.Contains("Configurations.Read") ?? false);
        
        //modelBuilder.HasDefaultSchema("PetHelper_Species");
    }

    private ILoggerFactory CreateLoggerFactory() => 
        LoggerFactory.Create(builder => { builder.AddConsole(); });

}