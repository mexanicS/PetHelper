using Microsoft.Extensions.DependencyInjection;
using PetHelper.Accounts.Application.Interfaces;
using PetHelper.Accounts.Contracts.UserManagment;
using PetHelper.Accounts.Domain;
using PetHelper.Core;
using PetHelper.SharedKernel;
using PetHelper.SharedKernel.ValueObjects;
using PetHelper.SharedKernel.ValueObjects.Common;
using PetHelper.SharedKernel.ValueObjects.RolePermission;
using PetHelper.SharedKernel.ValueObjects.User;

namespace PetHelper.Accounts.Application.Features.Contracts;
public class CreateUserUsingContract : ICreateUserContract
{
    private readonly IAccountRepository _repository; 
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserUsingContract(
        IAccountRepository repository,
        [FromKeyedServices(Constants.Context.AccountManagement)] IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<UserId> CreateUser(RoleId roleId, CancellationToken ct)
    {
        var email = Email.Create("Emas2fgoiL123@mail.com").Value;
        var userName = Name.Create("Ivanov Ivan").Value;
        var role = _repository.GetRole(roleId.Value).Result.Value;
        var user = User.Create(email, userName, role).Value;
        var userId = UserId.Create(user.Id).Value;

        var transaction = await _unitOfWork.BeginTransaction(ct);
        await _repository.AddUser(user, ct);
        transaction.Commit();
        await _unitOfWork.SaveChangesAsync(ct);

        return userId;
    }
}
