using CSharpFunctionalExtensions;
using PetHelper.Accounts.Contracts.Responses;
using PetHelper.Core.Abstractions.Commands;
using PetHelper.SharedKernel;

namespace PetHelper.Accounts.Application.AccountsManagement.Commands.Login;

public class LoginHandler : ICommandHandler<LoginResponse, LoginCommand>
{
    public Task<Result<LoginResponse, ErrorList>> Handle(
        LoginCommand command, 
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}