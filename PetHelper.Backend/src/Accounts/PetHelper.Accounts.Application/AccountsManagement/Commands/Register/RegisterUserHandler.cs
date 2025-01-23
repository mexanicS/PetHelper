using CSharpFunctionalExtensions;
using PetHelper.Core.Abstractions.Commands;
using PetHelper.SharedKernel;

namespace PetHelper.Accounts.Application.AccountsManagement.Commands.Register;

public class RegisterUserHandler : ICommandHandler<RegisterUserCommand>
{
    public Task<UnitResult<ErrorList>> Handle(
        RegisterUserCommand command, 
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}