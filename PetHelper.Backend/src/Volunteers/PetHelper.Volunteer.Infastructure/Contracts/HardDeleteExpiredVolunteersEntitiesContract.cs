using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PetHelper.Core.Abstractions;
using PetHelper.Core.Extensions;
using PetHelper.SharedKernel;
using PetHelper.Volunteer.Infastructure.DbContexts;

namespace PetHelper.Volunteer.Infastructure.Contracts;

public class HardDeleteExpiredVolunteersEntitiesContract : IHardDeleteEntitiesContract
{
    private readonly VolunteerWriteDbContext _volunteerWriteDbContext;
    private readonly ILogger<IHardDeleteEntitiesContract> _logger;
    private readonly SoftDeleteConfig _config;

    public HardDeleteExpiredVolunteersEntitiesContract(
        VolunteerWriteDbContext volunteerWriteDbContext,
        IOptions<SoftDeleteConfig> config,
        ILogger<IHardDeleteEntitiesContract> logger)
    {
        _volunteerWriteDbContext = volunteerWriteDbContext;
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
        var speciesToDelete = _volunteerWriteDbContext.Volunteers
            .GetExpiredEntitiesQuery(_config.DaysToHardDelete);
        
        _volunteerWriteDbContext.RemoveRange(speciesToDelete);
        await _volunteerWriteDbContext.SaveChangesAsync(cancellationToken);
        
        _logger.LogInformation($"{nameof(HardDeleteExpiredVolunteersEntitiesContract)} deleted {speciesToDelete.Count()} Species");
    }
}