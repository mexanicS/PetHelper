using PetHelper.Core.Abstractions.Commands;
using PetHelper.Core.DTOs;

namespace PetHelper.Accounts.Application.AccountsManagement.Commands.Register;

public record RegisterUserCommand(string Email, 
    string UserName, 
    string Password,
    FullNameDto FullName) : ICommand;