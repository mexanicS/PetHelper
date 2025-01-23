using Microsoft.AspNetCore.Identity;

namespace PetHelper.Accounts.Domain;

public class Role : IdentityRole<Guid>
{
    public List<RolePermission> RolePermissions { get; set; } = [];
}