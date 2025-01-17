using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using PetHelper.Application.Database;
using PetHelper.Application.FileProvider;
using PetHelper.Application.Messaging;
using PetHelper.Application.Providers;
using PetHelper.Application.Species;
using PetHelper.Application.Volunteers;
using PetHelper.Infastructure.BackgroundServices;
using PetHelper.Infastructure.DbContexts;
using PetHelper.Infastructure.Files;
using PetHelper.Infastructure.MessageQueues;
using PetHelper.Infastructure.Options;
using PetHelper.Infastructure.Providers;
using PetHelper.Infastructure.Repository;
using FileInfo = PetHelper.Application.FileProvider.FileInfo;

namespace PetHelper.Infastructure;

public static class Inject
{
    public static IServiceCollection AddInfastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddDbContexts()
            .AddMinio(configuration)
            .AddRepositories()
            .AddServices()
            .AddHostedServices()
            .AddMessageQueues();
        
        return services;
    }
    
    private static IServiceCollection AddMessageQueues(
        this IServiceCollection services)
    {
        services.AddSingleton<IMessageQueue<IEnumerable<FileInfo>>, InMemoryMessageQueue<IEnumerable<FileInfo>>>();
        
        return services;
    }
    
    private static IServiceCollection AddHostedServices(
        this IServiceCollection services)
    {
        services.AddHostedService<FilesCleanerBackgroudService>();
        
        return services;
    }
    
    private static IServiceCollection AddServices(
        this IServiceCollection services)
    {
        services.AddScoped<IFilesCleanerService, FilesCleanerService>();
        
        return services;
    }
    
    private static IServiceCollection AddRepositories(
        this IServiceCollection services)
    {
        services.AddScoped<IVolunteersRepository, VolunteersRepository>();
        services.AddScoped<ISpeciesRepository, SpeciesRepository>();
        
        return services;
    }
    
    private static IServiceCollection AddDbContexts(
        this IServiceCollection services)
    {
        services.AddScoped<WriteDbContext>();
        services.AddScoped<IReadDbContext, ReadDbContext>();
        
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