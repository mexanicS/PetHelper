using Microsoft.AspNetCore.Identity;
using PetHelper.Accounts.Domain.AccountModels;
using PetHelper.SharedKernel.ValueObjects;
using PetHelper.SharedKernel.ValueObjects.Pet;

namespace PetHelper.Accounts.Domain;

public class User : IdentityUser<Guid>
{
    private User() {}
    private List<Role> _roles = [];
    private List<SocialNetwork> _socialNetworks = [];
    private List<PetPhoto> _photos = [];
    public FullName FullName { get; set; } = null!;
    
    public AdminAccount? AdminAccount { get; set; }
    
    public IReadOnlyList<Role> Roles => _roles.AsReadOnly();
    
    public IReadOnlyList<SocialNetwork> SocialNetworks => _socialNetworks;
    
    public IReadOnlyList<PetPhoto> Photos => _photos;
    
    public static User CreateAdmin(string email, string userName, FullName fullName, Role role)
    {
        return new User
        {
            Email = email,
            UserName = userName,
            _roles = [role],
            FullName = fullName
        };
    }
}