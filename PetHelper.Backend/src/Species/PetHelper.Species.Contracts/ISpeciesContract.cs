using CSharpFunctionalExtensions;
using PetHelper.SharedKernel;
using PetHelper.SharedKernel.ValueObjects.ModelIds;

namespace PetHelper.Species.Contracts;

public interface ISpeciesContract
{
    Task<Result<bool, Error>> IsExistingSpeciesAndBreed(
        SpeciesId speciesId,
        BreedId breedId,
        CancellationToken cancellationToken = default);
}