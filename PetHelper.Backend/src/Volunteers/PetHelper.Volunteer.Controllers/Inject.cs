using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetHelper.Volunteer.Contracts;

namespace PetHelper.Volunteer.Controllers;

public static class Inject
{
    public static IServiceCollection AddVolunteersPresentation(
        this IServiceCollection services)
    {
        services.AddScoped<IVolunteerContracts, VolunteerContracts>();
        
        return services;
    }
}