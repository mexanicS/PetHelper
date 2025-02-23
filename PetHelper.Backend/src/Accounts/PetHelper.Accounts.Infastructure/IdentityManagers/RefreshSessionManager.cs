using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetHelper.Accounts.Application.Interfaces;
using PetHelper.Accounts.Domain;
using PetHelper.Accounts.Infastructure.DbContexts;
using PetHelper.SharedKernel;

namespace PetHelper.Accounts.Infastructure.IdentityManagers;

public class RefreshSessionManager(WriteAccountsDbContext writeAccountsDbContext) : IRefreshSessionManager
{
    public async Task<Result<RefreshSession, ErrorList>> GetByRefreshToken(Guid refreshToken, 
        CancellationToken cancellationToken)
    {
        var refreshSession =  await writeAccountsDbContext.RefreshSessions
            .Include(rs=>rs.User)
            .FirstOrDefaultAsync(x => x.RefreshToken == refreshToken, cancellationToken);

        if (refreshSession is null)
            return Errors.General.NotFound().ToErrorList();

        return refreshSession;
    }
    
    public void Delete(RefreshSession refreshSession)
    {
        writeAccountsDbContext.RefreshSessions.Remove(refreshSession);
        
    }
}