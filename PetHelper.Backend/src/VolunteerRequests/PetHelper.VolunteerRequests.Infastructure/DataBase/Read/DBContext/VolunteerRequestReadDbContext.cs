using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetHelper.VolunteerRequests.Application.DataBase.Dto;
using PetHelper.VolunteerRequests.Application.DataBase.Interfaces;

namespace PetHelper.VolunteerRequests.Infastructure.DataBase.Read.DBContext;

public class VolunteerRequestReadDbContext() : DbContext, IVolunteerRequestReadDbContext
{ 
    public IQueryable<VolunteerRequestDto> VolunteerRequests => Set<VolunteerRequestDto>();

    private readonly string _connectionString;

    public VolunteerRequestReadDbContext(string connectionString
        = "Host=host.docker.internal;Port=5434;Database=pet_helper;Username=postgres;Password=postgres") : this()
    {
        _connectionString = connectionString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        
        optionsBuilder.UseNpgsql(_connectionString);
        optionsBuilder.UseSnakeCaseNamingConvention();
        optionsBuilder.UseLoggerFactory(CreateLoggerFactory());
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }

    private ILoggerFactory CreateLoggerFactory() =>
        LoggerFactory.Create(builder => { builder.AddConsole(); });

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.HasDefaultSchema("VolunteerRequests");

        builder.ApplyConfigurationsFromAssembly(typeof(VolunteerRequestReadDbContext).Assembly,
            type => type.FullName?.ToLower().Contains("read.configuration") ?? false);
    }
}