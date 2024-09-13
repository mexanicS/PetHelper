using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PetHelper.Domain.Models;

namespace PetHelper.Infastructure
{
    public class ApplicationDbContext(IConfiguration configuration) : DbContext
    {
        private const string DataBaseName = nameof(Database);

        #region DbSet
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Volunteer> Volunteers { get; set; }
        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(configuration.GetConnectionString(DataBaseName));
            optionsBuilder.UseSnakeCaseNamingConvention();
            optionsBuilder.UseLoggerFactory(CreateLoggerFactory());

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
