using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PetHelper.Accounts.Application.Extensions;
using PetHelper.Accounts.Domain;
using PetHelper.Core.Abstractions.Commands;
using PetHelper.SharedKernel;
using PetHelper.SharedKernel.ValueObjects;

namespace PetHelper.Accounts.Application.AccountsManagement.Commands.Register;

public class RegisterUserHandler : ICommandHandler<RegisterUserCommand>
{
    private readonly UserManager<User> _userManager;
    private readonly ILogger<RegisterUserHandler> _logger;

    public RegisterUserHandler(
        UserManager<User> userManager, 
        ILogger<RegisterUserHandler> logger)
    {
        _userManager = userManager;
        _logger = logger;
    }
    public async Task<UnitResult<ErrorList>> Handle(
        RegisterUserCommand command, 
        CancellationToken cancellationToken)
    {
        var existingUser = _userManager.FindByEmailAsync(command.Email).Result;
        if (existingUser != null)
            return Errors.General.AlreadyExist().ToErrorList();
        
        var fullNameResult = 
            FullName.Create(command.FullName.FirstName, command.FullName.LastName, command.FullName.MiddleName);
        if (fullNameResult.IsFailure)
            return fullNameResult.Error.ToErrorList();
            
        var user = User.CreateParticipant(command.Email, command.UserName, fullNameResult.Value);
        var result = await _userManager.CreateAsync(user, command.Password);
        
        if (!result.Succeeded)
        {
            return result.Errors.ToErrorList();
        }
        
        _logger.LogInformation("User {username} was registered", user.UserName);
        return UnitResult.Success<ErrorList>();
    }
}