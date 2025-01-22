using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PetHelper.Core.Abstractions.Commands;
using PetHelper.Core.Abstractions.Queries;
using PetHelper.Core.File;

namespace PetHelper.Volunteer.Application;

public static class Inject
{
    public static IServiceCollection AddVolunteersApplication(this IServiceCollection services)
    {
        var assembly = typeof(Inject).Assembly;
        
        services
            .AddValidatorsFromAssembly(assembly)
            .AddCommands()
            .AddQueries();;
        
        services.AddScoped<AddFileHandler>();
        services.AddScoped<DeleteFileHandler>();
        services.AddScoped<GetFileByNameHandler>();

        return services;
    }
    
    private static IServiceCollection AddCommands(this IServiceCollection services)
    {
        return services.Scan(scan => scan.FromAssemblies(typeof(Inject).Assembly)
            .AddClasses(classes => classes
                .AssignableToAny(typeof(ICommandHandler<,>),typeof(ICommandHandler<>)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime());
    }
    
    private static IServiceCollection AddQueries(this IServiceCollection services)
    {
        return services.Scan(scan => scan.FromAssemblies(typeof(Inject).Assembly)
            .AddClasses(classes => classes
                .AssignableTo(typeof(IQueryHandler<,>)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime());
    }
}