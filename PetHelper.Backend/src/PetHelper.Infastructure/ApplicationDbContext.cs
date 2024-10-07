using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PetHelper.Domain.Models;
using PetHelper.Domain.Models.Species;
using PetHelper.Infastructure.Interceptors;

namespace PetHelper.Infastructure
{
    public class ApplicationDbContext(
        IConfiguration configuration
        ) : DbContext
    {
        private const string DataBaseName = nameof(Database);
        
        public DbSet<Volunteer> Volunteers { get; set; }
        
        public DbSet<Species> Specieses { get; set; }
        
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
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }

        private ILoggerFactory CreateLoggerFactory() => 
            LoggerFactory.Create(builder => { builder.AddConsole(); });

    }
}
