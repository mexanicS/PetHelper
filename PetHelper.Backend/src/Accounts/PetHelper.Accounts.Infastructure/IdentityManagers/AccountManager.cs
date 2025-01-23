using CSharpFunctionalExtensions;
using PetHelper.Accounts.Application.Interfaces;
using PetHelper.Accounts.Domain.AccountModels;
using PetHelper.Accounts.Infastructure.DbContexts;
using PetHelper.SharedKernel;

namespace PetHelper.Accounts.Infastructure.IdentityManagers;

public class AccountManager(WriteAccountsDbContext writeAccountsDbContext) : IAccountManager
{
    public async Task<UnitResult<ErrorList>> CreateAdminAccount(AdminAccount adminAccount)
    {
        try
        {
            await writeAccountsDbContext.AdminAccounts.AddAsync(adminAccount);
            await writeAccountsDbContext.SaveChangesAsync();
            return UnitResult.Success<ErrorList>();
        }
        catch (Exception e)
        {
            return Error.Failure("could.not.create.admin_account", e.Message).ToErrorList();
        }
    }

    public Task<UnitResult<ErrorList>> CreateParticipantAccount(ParticipantAccount adminAccount)
    {
        throw new NotImplementedException();
    }

    public Task<UnitResult<ErrorList>> CreateVolunteerAccount(VolunteerAccount adminAccount)
    {
        throw new NotImplementedException();
    }
}