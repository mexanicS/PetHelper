namespace PetHelper.Domain.Models;

public record VolunteerId()
{
    private VolunteerId(Guid value) : this()
    {
        Value = value;
    }
    
    public Guid Value { get; }
    
    public static VolunteerId NewId() => new(Guid.NewGuid());
    
    public static VolunteerId Create(Guid id) => new(id);
    
    public static implicit operator Guid(VolunteerId volunteerId)
    {
        ArgumentNullException.ThrowIfNull(volunteerId);
        
        return volunteerId.Value;
    }
}