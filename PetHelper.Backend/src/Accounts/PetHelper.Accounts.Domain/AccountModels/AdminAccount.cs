using PetHelper.SharedKernel;

namespace PetHelper.Accounts.Domain.AccountModels;

public class AdminAccount : SoftDeletableEntity
{
    public const string RoleName = "Admin";
    
    private AdminAccount() {}
    
    public AdminAccount(User user)
    {
        //Id = Guid.NewGuid();
        User = user;
        UserId = user.Id;
    }
    
    //public Guid Id { get; set; }
    
    public Guid UserId { get; set; }
    
    public User User { get; set; }
}