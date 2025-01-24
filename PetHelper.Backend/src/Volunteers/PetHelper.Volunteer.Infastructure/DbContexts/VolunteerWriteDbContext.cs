using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace PetHelper.Volunteer.Infastructure.DbContexts
{
    public class VolunteerWriteDbContext : DbContext
    {
        private readonly string _connectionString;

        public VolunteerWriteDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }
        public DbSet<Domain.Volunteer> Volunteers { get; set; }
        
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString);
            optionsBuilder.UseSnakeCaseNamingConvention();
            optionsBuilder.UseLoggerFactory(CreateLoggerFactory());
            optionsBuilder.EnableSensitiveDataLogging();
            //optionsBuilder.AddInterceptors(new SoftDeleteInterceptor());
            
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(VolunteerWriteDbContext).Assembly, 
                type => type.FullName?.Contains("Configurations.Write") ?? false);
            
            //modelBuilder.HasDefaultSchema("PetHelper_Volunteers");
        }

        private ILoggerFactory CreateLoggerFactory() => 
            LoggerFactory.Create(builder => { builder.AddConsole(); });

    }
}
