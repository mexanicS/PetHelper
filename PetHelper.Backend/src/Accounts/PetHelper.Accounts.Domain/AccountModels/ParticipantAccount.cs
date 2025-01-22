namespace PetHelper.Accounts.Domain.AccountModels;

public class ParticipantAccount
{
    private ParticipantAccount(){}
    public ParticipantAccount(User user)
    {
        Id = Guid.NewGuid();
        User = user;
        UserId = user.Id;
    }
    
    public const string RoleName = "Participant";
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    
    public DateTime? BannedForRequestsUntil { get; set; }

    public void BanForRequestsForWeek(DateTime date)
    {
        BannedForRequestsUntil = date;
    }
}