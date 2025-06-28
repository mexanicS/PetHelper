using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetHelper.Accounts.Domain;
using PetHelper.Core;
using PetHelper.Core.Options;

namespace PetHelper.Discussions.Infastructure;

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