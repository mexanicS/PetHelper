using Microsoft.Extensions.Options;
using PetHelper.Accounts.Infastructure.DbContexts;
using PetHelper.Core.Abstractions;
using PetHelper.Core.Extensions;
using PetHelper.SharedKernel;

namespace PetHelper.Accounts.Infastructure.Contracts;

public class HardDeleteExpiredAccountEntitiesContract : IHardDeleteEntitiesContract
{
    private readonly WriteAccountsDbContext _accountsDbContext;
    private readonly SoftDeleteConfig _config;

    public HardDeleteExpiredAccountEntitiesContract(
        WriteAccountsDbContext accountsDbContext,
        IOptions<SoftDeleteConfig> config)
    {
        _accountsDbContext = accountsDbContext;
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
    }

    private async Task HardDeleteAdmins(CancellationToken cancellationToken)
    {
        var usersToDelete = _accountsDbContext.AdminAccounts
            .GetExpiredEntitiesQuery(_config.DaysToHardDelete);
        
        _accountsDbContext.RemoveRange(usersToDelete);
        await _accountsDbContext.SaveChangesAsync(cancellationToken);
    }
    
    private async Task HardDeleteParticipant(CancellationToken cancellationToken)
    {
        var usersToDelete = _accountsDbContext.ParticipantAccounts
            .GetExpiredEntitiesQuery(_config.DaysToHardDelete);
        
        _accountsDbContext.RemoveRange(usersToDelete);
        await _accountsDbContext.SaveChangesAsync(cancellationToken);
    }
    
    private async Task HardDeleteVolunteers(CancellationToken cancellationToken)
    {
        var usersToDelete = _accountsDbContext.VolunteerAccounts
            .GetExpiredEntitiesQuery(_config.DaysToHardDelete);
        
        _accountsDbContext.RemoveRange(usersToDelete);
        await _accountsDbContext.SaveChangesAsync(cancellationToken);
    }
}