using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetHelper.Species.Contracts;
using PetHelper.Species.Infastructure;

namespace PetHelper.Species.Controllers;

public static class Inject
{
    public static IServiceCollection AddSpeciesInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);
        services.AddScoped<ISpeciesContract, SpeciesContracts>();
        
        return services;
    }
}