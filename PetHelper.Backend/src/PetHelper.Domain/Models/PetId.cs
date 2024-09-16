namespace PetHelper.Domain.Models;

public record PetId()
{
    private PetId(Guid value) : this()
    {
        Value = value;
    }
    
    public Guid Value { get; }
    
    public static PetId NewPetId(Guid value) => new(Guid.NewGuid());
    
    public static PetId Empty() => new(Guid.Empty);
}