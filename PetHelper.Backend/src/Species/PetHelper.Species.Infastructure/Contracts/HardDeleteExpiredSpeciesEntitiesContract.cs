using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PetHelper.Core.Abstractions;
using PetHelper.Core.Extensions;
using PetHelper.SharedKernel;
using PetHelper.Species.Infastructure.DbContexts;

namespace PetHelper.Species.Infastructure.Contracts;

public class HardDeleteExpiredSpeciesEntitiesContract : IHardDeleteEntitiesContract
{
    private readonly SpeciesWriteDbContext _speciesWriteDbContext;
    private readonly ILogger<IHardDeleteEntitiesContract> _logger;
    private readonly SoftDeleteConfig _config;

    public HardDeleteExpiredSpeciesEntitiesContract(
        SpeciesWriteDbContext speciesWriteDbContext,
        IOptions<SoftDeleteConfig> config,
        ILogger<IHardDeleteEntitiesContract> logger)
    {
        _speciesWriteDbContext = speciesWriteDbContext;
        _logger = logger;
        _config = config.Value;
    }
    public async Task HardDeleteExpiredEntities(CancellationToken cancellationToken)
    {
        await Task.WhenAll(
            HardDeleteSpecies(cancellationToken));
    }
    
    private async Task HardDeleteSpecies(CancellationToken cancellationToken)
    {
        var speciesToDelete = _speciesWriteDbContext.Species
            .GetExpiredEntitiesQuery(_config.DaysToHardDelete);
        
        _speciesWriteDbContext.RemoveRange(speciesToDelete);
        await _speciesWriteDbContext.SaveChangesAsync(cancellationToken);
        
        _logger.LogInformation($"{nameof(HardDeleteExpiredSpeciesEntitiesContract)} deleted {speciesToDelete.Count()} Species");
    }
}