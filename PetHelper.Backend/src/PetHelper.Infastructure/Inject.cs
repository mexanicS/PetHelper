using Microsoft.Extensions.DependencyInjection;
using PetHelper.Application.Volunteers;
using PetHelper.Infastructure.Repository;

namespace PetHelper.Infastructure;

public static class Inject
{
    public static IServiceCollection AddInfastructure(this IServiceCollection services)
    {
        services.AddScoped<ApplicationDbContext>();
        services.AddScoped<IVolunteersRepository, VolunteersRepository>();
        
        return services;
    }
}