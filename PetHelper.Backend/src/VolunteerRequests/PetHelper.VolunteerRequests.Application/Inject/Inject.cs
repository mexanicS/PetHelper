using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PetHelper.Core.Abstractions.Commands;
using PetHelper.Core.Abstractions.Queries;
using PetHelper.VolunteerRequests.Application.Features.Contract;
using PetHelper.VolunteerRequests.Contracts;

namespace PetHelper.VolunteerRequests.Application.Inject;

public static class Inject
{
    public static IServiceCollection AddVolunteerRequestApplication(this IServiceCollection services)
    {
        var assembly = typeof(Inject).Assembly;
        
        services.Scan(scan => scan.FromAssemblies(assembly)
            .AddClasses(classes => classes
                .AssignableToAny(
                    typeof(ICommandHandler<>), typeof(ICommandHandler<,>),
                    typeof(IQueryHandler<>), typeof(IQueryHandler<,>)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime());

        services.AddScoped<ICreateVolunteerRequestContract,CreateVolunteerRequestUsingContract>();
 
        services.AddValidatorsFromAssembly(assembly);

        return services;
    }
}