using CSharpFunctionalExtensions;
using PetHelper.Accounts.Application.Interfaces;
using PetHelper.Accounts.Domain;
using PetHelper.Core.Abstractions.Queries;
using PetHelper.SharedKernel;

namespace PetHelper.Accounts.Application.AccountsManagement.Queries.GetUserInformation;

public class GetUserInformationHandler
    : IQueryHandler<User, GetUserInformationCommand>
{
    
    private readonly IAccountRepository _repository;

    public GetUserInformationHandler(IAccountRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<User, ErrorList>> Handle(
        GetUserInformationCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _repository.GetUserById(command.UserId, cancellationToken);
        
        if(result.IsFailure)
            return result.Error.ToErrorList();

        return result.Value;
    }
}