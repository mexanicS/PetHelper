using CSharpFunctionalExtensions;
using PetHelper.Accounts.Domain;
using PetHelper.SharedKernel;
using PetHelper.SharedKernel.ValueObjects.RolePermission;

namespace PetHelper.Accounts.Application.Interfaces;

public interface IAccountRepository
{
    public Task<Result<User, Error>> GetUserById(Guid userId, CancellationToken cancellationToken);
    
    public Task<Result<Role, Error>> GetRole(RoleName roleName);
    
    public Task<Result<Role, Error>> GetRole(Guid roleId);
    
    public Task AddUser(User user, CancellationToken ct);
}