using CSharpFunctionalExtensions;
using PetHelper.Domain.Models;
using PetHelper.Domain.Shared;

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
    
    public static Result<SpeciesBreed, Error> Create(SpeciesId speciesId, Guid breedId)
    {
        if (speciesId.Value == Guid.Empty)
            return Errors.General.ValueIsInvalid(nameof(speciesId));

        if (breedId == Guid.Empty)
            return Errors.General.ValueIsInvalid(nameof(breedId));

        return new SpeciesBreed(speciesId, breedId);
    }
}
