using PetHelper.Core.Abstractions.Queries;

namespace PetHelper.Accounts.Application.AccountsManagement.Queries.GetUserInformation;

public record GetUserInformationCommand(Guid UserId) : IQuery;