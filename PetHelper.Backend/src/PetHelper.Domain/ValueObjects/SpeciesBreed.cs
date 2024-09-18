using PetHelper.Domain.Models;

namespace PetHelper.Domain.ValueObjects;
public record SpeciesBreed()
{
    public SpeciesBreed(SpeciesId speciesId, Guid breedId) : this()
    {
        SpeciesId = speciesId;
        BreedId = breedId;
    }
    
    public SpeciesId SpeciesId { get; }
    
    public Guid BreedId { get; }
}
