using PetHelper.Domain.Models;

namespace PetHelper.Domain.ValueObjects;
public record SpeciesBreed()
{
    public SpeciesBreed(SpeciesId speciesId, BreedId breedId) : this()
    {
        SpeciesId = speciesId;
        BreedId = breedId;
    }
    
    public SpeciesId SpeciesId { get; } = null!;
    
    public BreedId BreedId { get; } = null!;
}
