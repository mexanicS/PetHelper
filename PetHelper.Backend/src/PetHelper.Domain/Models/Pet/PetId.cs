namespace PetHelper.Domain.Models.Pet;

public record PetId()
{
    private PetId(Guid value) : this()
    {
        Value = value;
    }
    
    public Guid Value { get; }
    
    public static PetId NewId() => new(Guid.NewGuid());
    
    public static PetId Create(Guid id) => new(id);
}