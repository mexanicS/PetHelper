namespace PetHelper.Domain.Models;

public record SpeciesId()
{
    private SpeciesId(Guid value) : this()
    {
        Value = value;
    }
    
    public Guid Value { get; }
    
    public static SpeciesId NewId() => new(Guid.NewGuid());
    
    public static SpeciesId Create(Guid id) => new(id);
    
    public static implicit operator Guid(SpeciesId speciesId)
    {
        ArgumentNullException.ThrowIfNull(speciesId);
        
        return speciesId.Value;
    }
}