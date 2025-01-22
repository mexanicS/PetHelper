using PetHelper.Accounts.Application.Interfaces;
using PetHelper.Accounts.Infastructure.DbContexts;

namespace PetHelper.Accounts.Infastructure.IdentityManagers;

public class RefreshSessionManager(WriteAccountsDbContext writeAccountsDbContext) : IRefreshSessionManager
{
    
}