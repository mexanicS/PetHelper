using CSharpFunctionalExtensions;

namespace PetHelper.SharedKernel;

public abstract class SoftDeletableEntity<TId> : Entity<TId> where TId : notnull
{
    protected SoftDeletableEntity(TId id) : base(id) { }

    public bool IsDeleted { get; protected set; }
    public DateTime? DeletionDate { get; protected set; }

    public virtual void Delete()
    {
        if (!IsDeleted)
        {
            IsDeleted = true;
            DeletionDate = DateTime.UtcNow;
        }
    }

    public virtual void Restore()
    {
        if (IsDeleted)
        {
            IsDeleted = false;
            DeletionDate = null;
        }
    }
}