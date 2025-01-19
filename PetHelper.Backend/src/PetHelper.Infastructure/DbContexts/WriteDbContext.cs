using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PetHelper.Domain.Models.Species;
using PetHelper.Domain.Models.Volunteer;
using PetHelper.Infastructure.Interceptors;

namespace PetHelper.Infastructure.DbContexts
{
    public class WriteDbContext(
        IConfiguration configuration
        ) : DbContext
    {
        private const string DataBaseName = nameof(Database);
        
        public DbSet<Volunteer> Volunteers { get; set; }
        
        public DbSet<Species> Species { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(configuration.GetConnectionString(DataBaseName));
            optionsBuilder.UseSnakeCaseNamingConvention();
            optionsBuilder.UseLoggerFactory(CreateLoggerFactory());
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.AddInterceptors(new SoftDeleteInterceptor());
            
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(WriteDbContext).Assembly, 
                type => type.FullName?.Contains("Configurations.Write") ?? false);
        }

        private ILoggerFactory CreateLoggerFactory() => 
            LoggerFactory.Create(builder => { builder.AddConsole(); });

    }
}
