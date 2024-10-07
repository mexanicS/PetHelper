using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using PetHelper.Application.Providers;
using PetHelper.Application.Species;
using PetHelper.Application.Volunteers;
using PetHelper.Infastructure.Options;
using PetHelper.Infastructure.Providers;
using PetHelper.Infastructure.Repository;

namespace PetHelper.Infastructure;

public static class Inject
{
    public static IServiceCollection AddInfastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<ApplicationDbContext>();
        
        services.AddScoped<IVolunteersRepository, VolunteersRepository>();
        services.AddScoped<ISpeciesRepository, SpeciesRepository>();
        
        services.AddMinio(configuration);
        
        return services;
    }
    
    private static IServiceCollection AddMinio(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<MinioOptions>(configuration.GetSection(MinioOptions.MINIO));
        
        services.AddMinio(options =>
        {
            var minioOptions = configuration.GetSection(MinioOptions.MINIO).Get<MinioOptions>()
                               ?? throw new ApplicationException("Missing minio configuration");
            
            options.WithEndpoint(minioOptions.EndPoint);
            options.WithCredentials(minioOptions.AccessKey, minioOptions.SecretKey);
            options.WithSSL(minioOptions.WithSSL);
        });
        
        services.AddScoped<IFileProvider, MinioProvider>();
        
        return services;
    }
}