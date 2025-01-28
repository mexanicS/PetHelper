using Microsoft.AspNetCore.Mvc;
using PetHelper.Accounts.Application.AccountsManagement.Commands.Login;
using PetHelper.Accounts.Application.AccountsManagement.Commands.Register;
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
    
    [Permission("login")]
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
}