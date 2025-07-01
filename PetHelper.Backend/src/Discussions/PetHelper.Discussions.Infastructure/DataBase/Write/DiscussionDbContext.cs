using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetHelper.Discussions.Domain;

namespace PetHelper.Discussions.Infastructure.DataBase.Write;

public class DiscussionDbContext : DbContext
{
    public DbSet<Discussion> Discussions => Set<Discussion>();
    
    public DbSet<Relation> Relations => Set<Relation>();
    
    private readonly string _conntecitonString;
    
    public DiscussionDbContext(string connectionString)
    {
        _conntecitonString = connectionString;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseNpgsql(_conntecitonString);
        optionsBuilder.UseSnakeCaseNamingConvention();
        optionsBuilder.UseLoggerFactory(CreateLoggerFactory());
        optionsBuilder.EnableSensitiveDataLogging();
    }

    private ILoggerFactory CreateLoggerFactory() =>
        LoggerFactory.Create(builder => { builder.AddConsole(); });

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.HasDefaultSchema("Discussions");

        builder.ApplyConfigurationsFromAssembly(typeof(DiscussionDbContext).Assembly,
            type => type.FullName?.ToLower().Contains("write.configuration") ?? false);
    }
}