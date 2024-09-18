namespace PetHelper.Domain.Models;

public record BreedId()
{
    private BreedId(Guid value) : this()
    {
        Value = value;
    }
    
    public Guid Value { get; }
    
    public static BreedId NewId() => new(Guid.NewGuid());
    
    public static BreedId Create(Guid id) => new(id);
}