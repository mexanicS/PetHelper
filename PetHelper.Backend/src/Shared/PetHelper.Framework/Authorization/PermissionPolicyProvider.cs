using Microsoft.AspNetCore.Authorization;

namespace PetHelper.Framework.Authorization;

public class PermissionPolicyProvider : IAuthorizationPolicyProvider
{
    public Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        throw new NotImplementedException();
    }

    public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
    {
        throw new NotImplementedException();
    }

    public Task<AuthorizationPolicy?> GetFallbackPolicyAsync()
    {
        throw new NotImplementedException();
    }
}