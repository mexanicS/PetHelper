using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetHelper.Core;
using PetHelper.SharedKernel;
using PetHelper.Species.Application.Database;
using PetHelper.Species.Application.Interfaces;
using PetHelper.Species.Infastructure.DbContexts;

namespace PetHelper.Species.Infastructure;

public static class Inject
{
    public static IServiceCollection AddSpeciesInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContexts(configuration)
            .AddRepositories()
            .AddUnitOfWork();
        
        return services;
    }

    private static IServiceCollection AddUnitOfWork(this IServiceCollection services)
    {
        services.AddKeyedScoped<IUnitOfWork, UnitOfWork>(Constants.Context.SpeciesManagement);
        return services;
    }
    
    private static IServiceCollection AddDbContexts(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<SpeciesWriteDbContext>(_ => 
            new SpeciesWriteDbContext(configuration.GetConnectionString("Database")!));
        
        services.AddScoped<IReadDbContext, SpeciesReadDbContext>(_ => 
            new SpeciesReadDbContext(configuration.GetConnectionString("Database")!));

        return services;
    }
    
    private static IServiceCollection AddRepositories(
        this IServiceCollection services)
    {
        services.AddScoped<ISpeciesRepository, SpeciesRepository>();
        return services;
    }
}