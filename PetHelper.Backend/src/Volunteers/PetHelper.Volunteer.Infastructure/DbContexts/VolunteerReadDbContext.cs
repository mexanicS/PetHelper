using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetHelper.Core.DTOs.ReadDtos;
using PetHelper.Volunteer.Application;

namespace PetHelper.Volunteer.Infastructure.DbContexts;

public class VolunteerReadDbContext(string connectionString) : DbContext, IReadDbContext
{ 
    public IQueryable<VolunteerDto> Volunteers => Set<VolunteerDto>();
    
    public IQueryable<PetDto> Pets => Set<PetDto>();
        
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(connectionString);
        optionsBuilder.UseSnakeCaseNamingConvention();
        optionsBuilder.UseLoggerFactory(CreateLoggerFactory());
        optionsBuilder.EnableSensitiveDataLogging();
        
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("PetHelper_Volunteers");

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(VolunteerReadDbContext).Assembly,
            type => type.FullName?.ToLower().Contains("read.configuration") ?? false);
    }

    private ILoggerFactory CreateLoggerFactory() => 
        LoggerFactory.Create(builder => { builder.AddConsole(); });

}