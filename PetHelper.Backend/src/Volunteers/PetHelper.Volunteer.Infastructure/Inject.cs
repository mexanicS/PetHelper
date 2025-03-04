using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using PetHelper.Core;
using PetHelper.Core.DataBase;
using PetHelper.Core.Messaging;
using PetHelper.Core.Providers;
using PetHelper.SharedKernel;
using PetHelper.Volunteer.Application;
using PetHelper.Volunteer.Infastructure.BackgroundServices;
using PetHelper.Volunteer.Infastructure.DbContexts;
using PetHelper.Volunteer.Infastructure.Files;
using PetHelper.Volunteer.Infastructure.MessageQueues;
using PetHelper.Volunteer.Infastructure.Options;
using PetHelper.Volunteer.Infastructure.Services;
using FileInfo = PetHelper.Core.FileProvider.FileInfo;

namespace PetHelper.Volunteer.Infastructure;

public static class Inject
{
    public static IServiceCollection AddVolunteerInfastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddDbContexts(configuration)
            .AddMinio(configuration)
            .AddRepositories()
            .AddServices()
            .AddHostedServices(configuration)
            .AddMessageQueues()
            .AddUnitOfWork();
        
        services.AddScoped<ISqlConnectionFactory, SqlConnectionFactory>();
        
        return services;
    }
    
    private static IServiceCollection AddMessageQueues(
        this IServiceCollection services)
    {
        services.AddSingleton<IMessageQueue<IEnumerable<FileInfo>>, InMemoryMessageQueue<IEnumerable<FileInfo>>>();
        
        return services;
    }
    
    private static IServiceCollection AddHostedServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddHostedService<FilesCleanerBackgroudService>();
        
        services.Configure<SoftDeleteConfig>(configuration.GetSection("SoftDeleteConfig"));
        
        return services;
    }
    
    private static IServiceCollection AddServices(
        this IServiceCollection services)
    {
        services.AddScoped<FilesCleanerService>();
        
        return services;
    }
    
    private static IServiceCollection AddRepositories(
        this IServiceCollection services)
    {
        services.AddScoped<IVolunteersRepository, VolunteersRepository>();
        
        return services;
    }
    
    private static IServiceCollection AddDbContexts(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<VolunteerWriteDbContext>(_ => 
            new VolunteerWriteDbContext(configuration.GetConnectionString("Database")!));
        
        services.AddScoped<IReadDbContext, VolunteerReadDbContext>(_ => 
            new VolunteerReadDbContext(configuration.GetConnectionString("Database")!));
        
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
    
    private static IServiceCollection AddUnitOfWork(this IServiceCollection services)
    {
        services.AddKeyedScoped<IUnitOfWork, UnitOfWork>(Constants.Context.VolunteerManagement);
        return services;
    }
}