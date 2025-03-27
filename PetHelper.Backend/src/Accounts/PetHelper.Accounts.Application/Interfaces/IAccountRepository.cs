using CSharpFunctionalExtensions;
using PetHelper.Accounts.Domain;
using PetHelper.SharedKernel;

namespace PetHelper.Accounts.Application.Interfaces;

public interface IAccountRepository
{
    public Task<Result<User, Error>> GetUserById(Guid userId, CancellationToken cancellationToken);
}