using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetHelper.Accounts.Application.Database;
using PetHelper.Accounts.Application.Interfaces;
using PetHelper.Accounts.Domain;
using PetHelper.Accounts.Infastructure.DataBase.Repositories;
using PetHelper.Accounts.Infastructure.DataSeeding;
using PetHelper.Accounts.Infastructure.DbContexts;
using PetHelper.Accounts.Infastructure.IdentityManagers;
using PetHelper.Accounts.Infastructure.Options;
using PetHelper.Core;
using PetHelper.Core.Options;
using PetHelper.SharedKernel;

namespace PetHelper.Accounts.Infastructure;

public static class Inject
{
    public static IServiceCollection AddAccountsInfrastructure(this IServiceCollection services,
    IConfiguration configuration)
    {
        services
            .AddDbContexts(configuration)
            .AddManagers()
            .AddInfrastructureIdentity(configuration)
            .AddAccountsSeeding()
            .AddUnitOfWork()
            .AddRepositories();
        
        return services;
    }
    
    private static IServiceCollection AddUnitOfWork(this IServiceCollection services)
    {
        services.AddKeyedScoped<IUnitOfWork, UnitOfWork>(Constants.Context.AccountManagement);
        return services;
    }
    
    private static IServiceCollection AddDbContexts(this IServiceCollection services
        ,IConfiguration configuration)
    {
        services.AddScoped<WriteAccountsDbContext>(_ => 
            new WriteAccountsDbContext(configuration.GetConnectionString("Database")!));
        
        services.AddScoped<IAccountsReadDbContext, ReadAccountsDbContext>(_ => 
            new ReadAccountsDbContext(configuration.GetConnectionString("Database")!));
        
        return services;
    }
    
    private static IServiceCollection AddManagers(this IServiceCollection services)
    {
        services.AddScoped<PermissionManager>();
        services.AddScoped<RolePermissionManager>();
        services.AddScoped<IAccountManager, AccountManager>();
        services.AddScoped<IRefreshSessionManager, RefreshSessionManager>();
        
        return services;
    }
    
    private static IServiceCollection AddInfrastructureIdentity(this IServiceCollection services
        , IConfiguration configuration)
    {
        services.AddOptions<JwtOptions>();
        
        services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.JWT));
        services.Configure<AdminOptions>(configuration.GetSection(AdminOptions.ADMIN));
        
        services.AddTransient<ITokenProvider, JwtTokenProvider>();
        
        services.AddIdentity<User, Role>(options =>
            {
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<WriteAccountsDbContext>();
        
        return services;
    }
    
    private static IServiceCollection AddAccountsSeeding(this IServiceCollection services)
    {
        services.AddSingleton<AccountsSeeder>();
        services.AddScoped<AccountsSeederService>();
        
        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IAccountRepository, AccountRepository>();
        
        return services;
    }
}