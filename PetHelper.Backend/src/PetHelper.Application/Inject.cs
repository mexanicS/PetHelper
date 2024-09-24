using Microsoft.Extensions.DependencyInjection;
using PetHelper.Application.Volunteers.CreateVolunteers;

namespace PetHelper.Application;

public static class Inject
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreateVolunteerHandler>();
        
        return services;
    }
}