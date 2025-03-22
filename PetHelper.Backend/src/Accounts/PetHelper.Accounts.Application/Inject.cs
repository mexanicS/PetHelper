using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PetHelper.Accounts.Application.Interfaces;
using PetHelper.Core.Abstractions.Commands;
using PetHelper.Core.Abstractions.Queries;

namespace PetHelper.Accounts.Application;

public static class Inject
{
    public static IServiceCollection AddAccountsApplication(this IServiceCollection services)
    {
        var assembly = typeof(Inject).Assembly;
        
        services.AddValidatorsFromAssembly(assembly);   
        
        services.Scan(scan => scan.FromAssemblies(assembly)
            .AddClasses(c => c.AssignableToAny(typeof(ICommandHandler<,>), typeof(ICommandHandler<>)))
            .AsSelfWithInterfaces().WithScopedLifetime());
        
        services.Scan(scan => scan.FromAssemblies(assembly)
            .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
            .AsSelfWithInterfaces().WithScopedLifetime());
        
        return services;
    }
}