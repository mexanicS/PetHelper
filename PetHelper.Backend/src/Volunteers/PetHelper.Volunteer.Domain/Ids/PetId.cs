namespace PetHelper.Volunteer.Domain.Ids;

public record PetId()
{
    private PetId(Guid value) : this()
    {
        Value = value;
    }
    
    public Guid Value { get; }
    
    public static PetId NewId() => new(Guid.NewGuid());
    
    public static PetId Create(Guid id) => new(id);
    
    public static implicit operator Guid(PetId petId)
    {
        ArgumentNullException.ThrowIfNull(petId);
        
        return petId.Value;
    }
}