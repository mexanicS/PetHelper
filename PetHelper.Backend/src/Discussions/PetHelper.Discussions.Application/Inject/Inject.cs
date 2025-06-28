using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PetHelper.Core.Abstractions.Commands;
using PetHelper.Core.Abstractions.Queries;

namespace PetHelper.Discussions.Application.Inject;

public static class Inject
{
    public static IServiceCollection AddDiscussionApplication(this IServiceCollection services)
    {
        var assembly = typeof(Inject).Assembly;
        
        services.Scan(scan => scan.FromAssemblies(assembly)
            .AddClasses(classes => classes
                .AssignableToAny(
                    typeof(ICommandHandler<>), typeof(ICommandHandler<,>),
                    typeof(IQueryHandler<>), typeof(IQueryHandler<,>)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime());

        services.AddValidatorsFromAssembly(assembly);

        return services;
    }
}