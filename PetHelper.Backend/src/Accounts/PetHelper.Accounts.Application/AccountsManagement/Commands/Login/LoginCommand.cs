using PetHelper.Core.Abstractions.Commands;

namespace PetHelper.Accounts.Application.AccountsManagement.Commands.Login;

public record LoginCommand(string Email, string Password) : ICommand;