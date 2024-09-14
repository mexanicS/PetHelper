namespace PetHelper.Domain.Models;

public record VolunteerId()
{
    private VolunteerId(Guid value) : this()
    {
        Value = value;
    }
    
    public Guid Value { get; }
    
    public static VolunteerId NewPetId() => new(Guid.NewGuid());
    
    public static VolunteerId Empty() => new(Guid.Empty);
}