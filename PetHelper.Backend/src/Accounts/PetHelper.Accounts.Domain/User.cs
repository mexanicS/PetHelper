using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using PetHelper.Accounts.Domain.AccountModels;
using PetHelper.Core.DTOs;
using PetHelper.SharedKernel;
using PetHelper.SharedKernel.ValueObjects;
using PetHelper.SharedKernel.ValueObjects.Common;
using PetHelper.SharedKernel.ValueObjects.Pet;

namespace PetHelper.Accounts.Domain;

public class User : IdentityUser<Guid>, ISoftDeletable
{
    private User() {}
    private List<Role> _roles = [];
    private List<SocialNetwork> _socialNetworks = [];
    private List<PetPhoto> _photos = [];
    private List<DetailsForAssistance> _detailsForAssistance = [];
    
    public FullName FullName { get; set; } = null!;
    
    public AdminAccount? AdminAccount { get; set; }
    
    public ParticipantAccount? ParticipantAccount { get; set; }
    
    public VolunteerAccount? VolunteerAccount { get; set; }
    
    public IReadOnlyList<Role> Roles => _roles.AsReadOnly();
    
    public IReadOnlyList<SocialNetwork> SocialNetworks => _socialNetworks;
    
    public IReadOnlyList<DetailsForAssistance> DetailsForAssistance => _detailsForAssistance;
    
    public IReadOnlyList<PetPhoto> Photos => _photos;

    private User(
        Email email,
        Name userName,
        Role role)
    {
        Id = Guid.NewGuid();
        Email = email;
        UserName = userName;
        _roles = [role];
    }
    public static Result<User, Error> Create(
        Email email,
        Name userName,
        Role role)
    {
        if (role != null && email != null && role != null)
            return new User(email, userName, role);

        return Errors.Validation("User");
    }
    
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
    
    public static User CreateParticipant(string email, 
        string userName,
        FullName fullName)
    {
        return new User
        {
            Email = email,
            UserName = userName,
            FullName = fullName,
        };
    }

    public bool IsDeleted { get; set; }
    
    public DateTime DeletionDate { get; set; }

    public void SoftDelete()
    {
        throw new NotImplementedException();
    }

    public void SoftRestore()
    {
        throw new NotImplementedException();
    }
}