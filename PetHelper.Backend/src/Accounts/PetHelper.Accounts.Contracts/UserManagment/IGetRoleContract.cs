using CSharpFunctionalExtensions;
using PetHelper.SharedKernel;
using PetHelper.SharedKernel.ValueObjects.RolePermission;

namespace PetHelper.Accounts.Contracts.UserManagment;

public interface IGetRoleContract
{
    public Task<Result<RoleId, Error>> GetRole(string name, CancellationToken ct);
}