using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetHelper.Volunteer.Contracts;
using PetHelper.Volunteer.Infastructure;

namespace PetHelper.Volunteer.Controllers;

public static class Inject
{
    public static IServiceCollection AddVolunteersInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddInfastructure(configuration);

        services.AddScoped<IVolunteerContracts, VolunteerContracts>();
        return services;
    }
}