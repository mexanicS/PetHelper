using PetHelper.SharedKernel.ValueObjects.RolePermission;
using PetHelper.SharedKernel.ValueObjects.User;

namespace PetHelper.Accounts.Contracts.UserManagment;

public interface ICreateUserContract
{
    public Task<UserId> CreateUser(RoleId roleId, CancellationToken ct);
}