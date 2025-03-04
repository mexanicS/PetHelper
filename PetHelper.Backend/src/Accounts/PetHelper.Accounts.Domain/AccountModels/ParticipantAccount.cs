using PetHelper.SharedKernel;

namespace PetHelper.Accounts.Domain.AccountModels;

public class ParticipantAccount : SoftDeletableEntity
{
    private ParticipantAccount(){}
    public ParticipantAccount(User user)
    {
        User = user;
        UserId = user.Id;
    }
    
    public const string RoleName = "Participant";
    public Guid UserId { get; set; }
    public User User { get; set; }
    
    public DateTime? BannedForRequestsUntil { get; set; }

    public void BanForRequestsForWeek(DateTime date)
    {
        BannedForRequestsUntil = date;
    }
}