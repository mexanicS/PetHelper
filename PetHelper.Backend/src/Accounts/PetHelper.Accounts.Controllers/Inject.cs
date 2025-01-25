using Microsoft.Extensions.DependencyInjection;
using PetHelper.Accounts.Contracts;

namespace PetHelper.Accounts.Controllers;

public static class Inject
{
    public static IServiceCollection AddAccountsPresentation(this IServiceCollection services)
    {
        services.AddScoped<IAccountContracts, AccountContracts>();
        
        return services;
    }
}