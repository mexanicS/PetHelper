using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PetHelper.Application.Volunteers.CreateVolunteers;
using PetHelper.Application.Volunteers.UpdateMainInfo;

namespace PetHelper.Application;

public static class Inject
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreateVolunteerHandler>();
        services.AddScoped<UpdateMainInfoHandler>();

        
        services.AddValidatorsFromAssembly(typeof(Inject).Assembly);
        
        return services;
    }
}