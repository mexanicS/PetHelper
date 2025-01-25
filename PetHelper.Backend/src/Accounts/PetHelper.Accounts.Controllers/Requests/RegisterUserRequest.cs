using PetHelper.Accounts.Application.AccountsManagement.Commands.Register;
using PetHelper.Core.DTOs;

namespace PetHelper.Accounts.Controllers.Requests;

public record RegisterUserRequest(string Email, string UserName, string Password, FullNameDto FullName)
{
    public RegisterUserCommand ToCommand() => 
        new RegisterUserCommand(Email, UserName, Password, FullName); 
}