using PetHelper.SharedKernel;
using PetHelper.SharedKernel.ValueObjects.Volunteer;

namespace PetHelper.Accounts.Domain.AccountModels;

public class VolunteerAccount : SoftDeletableEntity
{
    public VolunteerAccount(){}
    public VolunteerAccount(User user, int experience)
    {
        User = user;
        UserId = user.Id;
        WorkingExperience = experience;
    }
    public const string RoleName = "Volunteer";
    public Guid UserId { get; set; }
    public User User { get; set; }
    public int WorkingExperience { get; private set; } = default!;
}