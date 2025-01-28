using Microsoft.AspNetCore.Authorization;

namespace PetHelper.Framework.Authorization;

public class PermissionRequirementHandler : AuthorizationHandler<PermissionAttribute>
{
    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context, 
        PermissionAttribute permissionAttribute)
    {
        var userPermission = context.User.Claims.FirstOrDefault(claim => claim.Type == "Permission");
        
        if (userPermission is null)
            return;

        if (userPermission.Value == permissionAttribute.Code)
        {
            context.Succeed(permissionAttribute);
        }
    }
}