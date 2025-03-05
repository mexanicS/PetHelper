using CSharpFunctionalExtensions;

namespace PetHelper.SharedKernel;

public abstract class SoftDeletableEntity : Entity, ISoftDeletable
{
    public bool IsDeleted { get;  set; }
    public DateTime DeletionDate { get;  set; }

    public virtual void SoftDelete()
    {
        if (!IsDeleted)
        {
            IsDeleted = true;
            DeletionDate = DateTime.UtcNow;
        }
    }

    public virtual void SoftRestore()
    {
        if (IsDeleted)
        {
            IsDeleted = false;
            DeletionDate = default;
        }
    }
}