using Microsoft.AspNetCore.Mvc;
using PetHelper.Accounts.Application.AccountsManagement.Commands.Login;
using PetHelper.Accounts.Application.AccountsManagement.Commands.RefreshTokens;
using PetHelper.Accounts.Application.AccountsManagement.Commands.Register;
using PetHelper.Accounts.Application.AccountsManagement.Queries.GetUserInformation;
using PetHelper.Accounts.Contracts.Responses;
using PetHelper.Accounts.Controllers.Requests;
using PetHelper.Framework;
using PetHelper.Framework.Authorization;

namespace PetHelper.Accounts.Controllers;

public class AccountController : ApplicationController
{
    [HttpPost("registration")]
    public async Task<ActionResult> Register(
        [FromBody] RegisterUserRequest request,
        [FromServices] RegisterUserHandler handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(request.ToCommand(), cancellationToken);
        
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok();
    }
    
    [HttpPost("login")]
    public async Task<ActionResult> Login(
        [FromBody] LoginRequest request,
        [FromServices] LoginHandler handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(request.ToCommand(), cancellationToken);
        
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }
    
    [HttpPost("refresh")]
    public async Task<ActionResult> RefreshTokens(
        [FromBody] RefreshTokensRequest request,
        [FromServices] RefreshTokensHandler handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(
            new RefreshTokensCommand(request.AccessToken, request.RefreshToken), 
            cancellationToken);
        
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }
    
    [HttpGet("user-information/{id:guid}")]
    public async Task<IActionResult> GetUserInformation(
        [FromServices] GetUserInformationHandler handler,
        Guid id,
        CancellationToken cancellationToken)
    {
        var command = new GetUserInformationCommand(id);
        var result = await handler.Handle(command, cancellationToken);
        
        if(result.IsFailure)
            return result.Error.ToResponse();
            
        return Ok(result.Value);
    }
}