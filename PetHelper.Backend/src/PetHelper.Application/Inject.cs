using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PetHelper.Application.File;
using PetHelper.Application.Species;
using PetHelper.Application.Species.AddBreed;
using PetHelper.Application.Species.Create;
using PetHelper.Application.Volunteers.AddPet;
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
        services.AddScoped<CreateSpeciesHandler>();
        services.AddScoped<AddBreedHandler>();
        services.AddScoped<AddPetHandler>();
        
        services.AddValidatorsFromAssembly(typeof(Inject).Assembly);
        
        return services;
    }
}