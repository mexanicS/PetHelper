using PetHelper.Core.Abstractions.Commands;

namespace PetHelper.Accounts.Application.AccountsManagement.Commands.RefreshTokens;

public record RefreshTokensCommand(string AccessToken, Guid RefreshToken) : ICommand;