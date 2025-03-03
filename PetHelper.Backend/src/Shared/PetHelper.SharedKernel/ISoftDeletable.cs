namespace PetHelper.SharedKernel;

public interface ISoftDeletable
{
    bool IsDeleted { get;}
    
    DateTime DeletionDate { get;}
    
    public  void SoftDelete();
    
    public  void SoftRestore();
}