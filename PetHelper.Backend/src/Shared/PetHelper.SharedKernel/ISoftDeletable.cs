namespace PetHelper.SharedKernel;

public interface ISoftDeletable
{
    void Delete();
    
    void Restore();
}