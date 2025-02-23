using CSharpFunctionalExtensions;
using PetHelper.Accounts.Domain;
using PetHelper.SharedKernel;

namespace PetHelper.Accounts.Application.Interfaces;

public interface IRefreshSessionManager
{
    public Task<Result<RefreshSession, ErrorList>> GetByRefreshToken(Guid refreshToken,
        CancellationToken cancellationToken);

    public void Delete(RefreshSession refreshSession);
}