using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PetHelper.Application.Abstractions.Commands;
using PetHelper.Application.Abstractions.Queries;
using PetHelper.Application.File;

namespace PetHelper.Application;

public static class Inject
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        //эти еще не наследовал от ICommand
        services.AddScoped<AddFileHandler>();
        services.AddScoped<DeleteFileHandler>();
        services.AddScoped<GetFileByNameHandler>();

        services.AddCommands()
            .AddQueries()
            .AddValidatorsFromAssembly(typeof(Inject).Assembly);;
        
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