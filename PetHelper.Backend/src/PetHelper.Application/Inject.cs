using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PetHelper.Application.File;
using PetHelper.Application.Volunteers.CreateVolunteers;
using PetHelper.Application.Volunteers.DeleteVolunteer;
using PetHelper.Application.Volunteers.UpdateDetailsForAssistance;
using PetHelper.Application.Volunteers.UpdateMainInfo;
using PetHelper.Application.Volunteers.UpdateSocialNetworkList;

namespace PetHelper.Application;

public static class Inject
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreateVolunteerHandler>();
        services.AddScoped<UpdateMainInfoHandler>();
        services.AddScoped<UpdateSocialNetworkListHandler>();
        services.AddScoped<UpdateDetailsForAssistanceHandler>();
        services.AddScoped<DeleteVolunteerHandler>();
        services.AddScoped<AddFileHandler>();
        services.AddScoped<DeleteFileHandler>();
        services.AddScoped<GetFileByNameHandler>();
        
        services.AddValidatorsFromAssembly(typeof(Inject).Assembly);
        
        return services;
    }
}