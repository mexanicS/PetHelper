using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PetHelper.Accounts.Application.AccountsManagement.Commands.Register;
using PetHelper.Accounts.Application.Interfaces;
using PetHelper.Accounts.Contracts.Responses;
using PetHelper.Accounts.Domain;
using PetHelper.Core.Abstractions.Commands;
using PetHelper.SharedKernel;

namespace PetHelper.Accounts.Application.AccountsManagement.Commands.Login;

public class LoginHandler : ICommandHandler<LoginResponse, LoginCommand>
{
    private readonly UserManager<User> _userManager;
    private readonly ILogger<RegisterUserHandler> _logger;
    private readonly ITokenProvider _tokenProvider;

    public LoginHandler(
        UserManager<User> userManager, 
        ILogger<RegisterUserHandler> logger,
        ITokenProvider tokenProvider)
    {
        _userManager = userManager;
        _logger = logger;
        _tokenProvider = tokenProvider;
    }
    public async Task<Result<LoginResponse, ErrorList>> Handle(
        LoginCommand command, 
        CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(command.Email);
        if (user is null)
            return Errors.General.NotFound().ToErrorList();
        
        var passwordConfirmed = await _userManager.CheckPasswordAsync(user, command.Password);
        
        if (!passwordConfirmed)
            return Errors.User.InvalidCredentials().ToErrorList();
        
        var accessToken = await _tokenProvider.GetAccessToken(user, cancellationToken );
        //var refreshToken = await _tokenProvider.GenerateRefreshToken(user, accessToken.Jti, cancellationToken);
       
        _logger.LogInformation("User: {userName} logged in.", user.UserName);
        
        return new LoginResponse(accessToken.AccessToken, Guid.Empty);
    }
}