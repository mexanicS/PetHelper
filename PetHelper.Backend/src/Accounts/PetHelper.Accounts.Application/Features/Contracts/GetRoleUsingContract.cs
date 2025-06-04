using CSharpFunctionalExtensions;
using PetHelper.Accounts.Application.Interfaces;
using PetHelper.Accounts.Contracts.UserManagment;
using PetHelper.SharedKernel;
using PetHelper.SharedKernel.ValueObjects.RolePermission;

namespace PetHelper.Accounts.Application.Features.Contracts;

public class GetRoleContract : IGetRoleContract
{
    private readonly IAccountRepository _repository;

    public GetRoleContract(IAccountRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<RoleId,Error>> GetRole(string name, CancellationToken ct)
    {
        var roleName = RoleName.Create(name).Value;
        var roleResult = await _repository.GetRole(roleName);
        if (roleResult.IsFailure)
            return roleResult.Error;
        
        var roleId = RoleId.Create(roleResult.Value.Id).Value;

        return roleId;
    }
}