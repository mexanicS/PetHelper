using Microsoft.Extensions.DependencyInjection;
using PetHelper.Species.Contracts;

namespace PetHelper.Species.Controllers;

public static class Inject
{
    public static IServiceCollection AddSpeciesPresentation(this IServiceCollection services)
    {
        services.AddScoped<ISpeciesContract, SpeciesContracts>();
        
        return services;
    }
}