using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetHelper.Accounts.Application.Interfaces;
using PetHelper.Accounts.Domain;
using PetHelper.Accounts.Infastructure.DbContexts;
using PetHelper.SharedKernel;

namespace PetHelper.Accounts.Infastructure.DataBase.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly WriteAccountsDbContext _accountsDbContext;

    public AccountRepository(WriteAccountsDbContext accountsDbContext)
    {
        _accountsDbContext = accountsDbContext;
    }
    
    public async Task<Result<User, Error>> GetUserById(Guid userId, CancellationToken cancellationToken)
    {
        var result = await _accountsDbContext.Users
            .Include(u=>u.AdminAccount)
            .Include(u=>u.ParticipantAccount)
            .Include(u=>u.VolunteerAccount)
            .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
        
        if(result is null)
            return Error.NotFound("user-not-found", "User does not exist");
        
        return result;
    }
}