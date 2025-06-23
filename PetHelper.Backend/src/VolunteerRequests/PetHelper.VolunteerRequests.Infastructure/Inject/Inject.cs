using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetHelper.Core;
using PetHelper.SharedKernel;
using PetHelper.VolunteerRequests.Application.DataBase.Interfaces;
using PetHelper.VolunteerRequests.Infastructure.DataBase.Read.DBContext;
using PetHelper.VolunteerRequests.Infastructure.DataBase.Write;
using PetHelper.VolunteerRequests.Infastructure.DataBase.Write.Repositories;

namespace PetHelper.VolunteerRequests.Infastructure.Inject;

public static class Inject
{
    public static IServiceCollection AddVolunteerRequestInfrastructure(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<VolunteerRequestDbContext>(_ =>
            new VolunteerRequestDbContext(configuration.GetConnectionString(Constants.DATABASE)!));
        services.AddScoped<IVolunteerRequestReadDbContext, VolunteerRequestReadDbContext>(_ =>
            new VolunteerRequestReadDbContext(configuration.GetConnectionString(Constants.DATABASE)!));

        services.AddScoped<IVolunteerRequestRepository, VolunteerRequestRepository>();

        services.AddKeyedScoped<IUnitOfWork, UnitOfWork>(Constants.Context.VolunteersRequest);

        return services;
    }
}
