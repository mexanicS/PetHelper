using PetHelper.Accounts.Application.AccountsManagement.Commands.Login;

namespace PetHelper.Accounts.Controllers.Requests;

public record LoginRequest(string Email, string Password)
{
    public LoginCommand ToCommand() 
        => new LoginCommand(Email, Password);
}