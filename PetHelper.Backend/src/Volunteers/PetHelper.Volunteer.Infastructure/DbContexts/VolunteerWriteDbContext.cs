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
            
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("PetHelper_Volunteers");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(VolunteerWriteDbContext).Assembly,
                type => type.FullName?.ToLower().Contains("write.configuration") ?? false);
        }

        private ILoggerFactory CreateLoggerFactory() => 
            LoggerFactory.Create(builder => { builder.AddConsole(); });

    }
}
