using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetHelper.Accounts.Domain;
using PetHelper.Core;
using PetHelper.Core.Options;
using PetHelper.Discussions.Application.Database.Interfaces;
using PetHelper.Discussions.Infastructure.DataBase.Read.DbContext;
using PetHelper.Discussions.Infastructure.DataBase.Write;
using PetHelper.Discussions.Infastructure.DataBase.Write.Repositories;
using PetHelper.SharedKernel;

namespace PetHelper.Discussions.Infastructure;

public static class Inject
{
    public static IServiceCollection AddDiscussionInfrastructure(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<DiscussionDbContext>(_ =>
            new DiscussionDbContext(configuration.GetConnectionString("Database")!));
        services.AddScoped<IDiscussionReadDbContext, DiscussionReadDbContext>(_ =>
            new DiscussionReadDbContext(configuration.GetConnectionString("Database")!));

        services.AddScoped<IDiscussionRepository, DiscussionRepository>();

        services.AddKeyedScoped<IUnitOfWork, UnitOfWork>(Constants.Context.Discussions);

        return services;
    }
}