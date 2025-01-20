using CSharpFunctionalExtensions;
using PetHelper.Domain.Models;
using PetHelper.Domain.Models.Breed;
using PetHelper.Domain.Models.Species;
using PetHelper.Domain.Shared;
using PetHelper.Domain.ValueObjects;
using PetHelper.Domain.ValueObjects.Common;
using Breed = PetHelper.Domain.Models.Breed.Breed;

namespace PetHelper.Application.Species;

public interface ISpeciesRepository
{
    Task<Guid> Add(Domain.Models.Species.Species volunteer, 
        CancellationToken cancellationToken = default);

    Task<Result<Domain.Models.Species.Species, Error>> GetSpeciesByName(Name speciesName, 
        CancellationToken cancellationToken = default);

    Task<Result<Domain.Models.Species.Species, Error>> GetSpeciesById(SpeciesId speciesId,
        CancellationToken cancellationToken = default);

    Task<Guid> Save(Domain.Models.Species.Species species,
        CancellationToken cancellationToken = default);
    
    Task<Guid> Delete(Domain.Models.Species.Species species, 
        CancellationToken cancellationToken = default);
}