using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PetHelper.Accounts.Infastructure.DbContexts;
using PetHelper.Core.Abstractions;
using PetHelper.Core.Extensions;
using PetHelper.SharedKernel;

namespace PetHelper.Accounts.Infastructure.Contracts;

public class HardDeleteExpiredAccountEntitiesContract : IHardDeleteEntitiesContract
{
    private readonly WriteAccountsDbContext _accountsDbContext;
    private readonly ILogger<HardDeleteExpiredAccountEntitiesContract> _logger;
    private readonly SoftDeleteConfig _config;

    public HardDeleteExpiredAccountEntitiesContract(
        WriteAccountsDbContext accountsDbContext,
        IOptions<SoftDeleteConfig> config,
        ILogger<HardDeleteExpiredAccountEntitiesContract> logger)
    {
        _accountsDbContext = accountsDbContext;
        _logger = logger;
        _config = config.Value;
    }
    public async Task HardDeleteExpiredEntities(CancellationToken cancellationToken)
    {
        await Task.WhenAll(
            HardDeleteUsers(cancellationToken),
            HardDeleteAdmins(cancellationToken),
            HardDeleteParticipant(cancellationToken),
            HardDeleteVolunteers(cancellationToken));
    }
    
    private async Task HardDeleteUsers(CancellationToken cancellationToken)
    {
        var usersToDelete = _accountsDbContext.Users
            .GetExpiredEntitiesQuery(_config.DaysToHardDelete);
        
        _accountsDbContext.RemoveRange(usersToDelete);
        await _accountsDbContext.SaveChangesAsync(cancellationToken);
        
        _logger.LogInformation($"{nameof(HardDeleteExpiredAccountEntitiesContract)} deleted {usersToDelete.Count()} Users");
    }

    private async Task HardDeleteAdmins(CancellationToken cancellationToken)
    {
        var adminsToDelete = _accountsDbContext.AdminAccounts
            .GetExpiredEntitiesQuery(_config.DaysToHardDelete);
        
        _accountsDbContext.RemoveRange(adminsToDelete);
        await _accountsDbContext.SaveChangesAsync(cancellationToken);
        
        _logger.LogInformation($"{nameof(HardDeleteExpiredAccountEntitiesContract)} deleted {adminsToDelete.Count()} AdminAccounts");
    }
    
    private async Task HardDeleteParticipant(CancellationToken cancellationToken)
    {
        var participantToDelete = _accountsDbContext.ParticipantAccounts
            .GetExpiredEntitiesQuery(_config.DaysToHardDelete);
        
        _accountsDbContext.RemoveRange(participantToDelete);
        await _accountsDbContext.SaveChangesAsync(cancellationToken);
        
        _logger.LogInformation($"{nameof(HardDeleteExpiredAccountEntitiesContract)} deleted {participantToDelete.Count()} ParticipantAccounts");
    }
    
    private async Task HardDeleteVolunteers(CancellationToken cancellationToken)
    {
        var volunteerAccountsToDelete = _accountsDbContext.VolunteerAccounts
            .GetExpiredEntitiesQuery(_config.DaysToHardDelete);
        
        _accountsDbContext.RemoveRange(volunteerAccountsToDelete);
        await _accountsDbContext.SaveChangesAsync(cancellationToken);
        
        _logger.LogInformation($"{nameof(HardDeleteExpiredAccountEntitiesContract)} deleted {volunteerAccountsToDelete.Count()} VolunteerAccounts");
    }
}