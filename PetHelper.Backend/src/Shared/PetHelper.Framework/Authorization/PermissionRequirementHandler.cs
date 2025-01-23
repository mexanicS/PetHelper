using Microsoft.AspNetCore.Authorization;

namespace PetHelper.Framework.Authorization;

public class PermissionRequirementHandler : AuthorizationHandler<PermissionAttribute>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context, 
        PermissionAttribute requirement)
    {
        throw new NotImplementedException();
    }
}