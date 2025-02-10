using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using PetHelper.Accounts.Contracts;
using PetHelper.Accounts.Domain;

namespace PetHelper.Framework.Authorization;

public class PermissionRequirementHandler : AuthorizationHandler<PermissionAttribute>
{
    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context, 
        PermissionAttribute permissionAttribute)
    {
        var permissions = context.User.Claims
            .Where(claim => claim.Type == CustomClaims.Permission)
            .Select(claim => claim.Value)
            .ToList();
            
        if (permissions.Contains(permissionAttribute.Code))
        {
            context.Succeed(permissionAttribute);
            return;
        }
        context.Fail();
    }
}