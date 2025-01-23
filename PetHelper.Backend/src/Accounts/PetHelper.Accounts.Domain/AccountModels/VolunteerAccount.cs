using PetHelper.SharedKernel.ValueObjects.Volunteer;

namespace PetHelper.Accounts.Domain.AccountModels;

public class VolunteerAccount
{
    public VolunteerAccount(){}
    public VolunteerAccount(User user, int experience)
    {
        Id = Guid.NewGuid();
        User = user;
        UserId = user.Id;
        WorkingExperience = experience;
    }
    public const string RoleName = "Volunteer";
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public int WorkingExperience { get; private set; } = default!;
}