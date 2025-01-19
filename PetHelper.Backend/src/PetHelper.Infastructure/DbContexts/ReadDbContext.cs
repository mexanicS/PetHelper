using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PetHelper.Application.Database;
using PetHelper.Application.DTOs;
using PetHelper.Application.DTOs.Pet;
using PetHelper.Application.DTOs.ReadDtos;
using PetHelper.Domain.Models.Pet;
using PetHelper.Infastructure.Interceptors;

namespace PetHelper.Infastructure.DbContexts;

public class ReadDbContext(
    IConfiguration configuration
) : DbContext, IReadDbContext
{
    private const string DataBaseName = nameof(Database);
        
    public IQueryable<VolunteerDto> Volunteers => Set<VolunteerDto>();
    
    public IQueryable<PetDto> Pets => Set<PetDto>();
    
    public IQueryable<SpeciesDto> Species => Set<SpeciesDto>();
    
    public IQueryable<BreedDto> Breeds => Set<BreedDto>();
        
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(configuration.GetConnectionString(DataBaseName));
        optionsBuilder.UseSnakeCaseNamingConvention();
        optionsBuilder.UseLoggerFactory(CreateLoggerFactory());
        optionsBuilder.EnableSensitiveDataLogging();
        
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(WriteDbContext).Assembly, 
            type => type.FullName?.Contains("Configurations.Read") ?? false);
    }

    private ILoggerFactory CreateLoggerFactory() => 
        LoggerFactory.Create(builder => { builder.AddConsole(); });

}