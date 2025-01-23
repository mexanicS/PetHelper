using CSharpFunctionalExtensions;
using PetHelper.SharedKernel;
using PetHelper.SharedKernel.ValueObjects.Common;
using PetHelper.SharedKernel.ValueObjects.ModelIds;

namespace PetHelper.Species.Application.Interfaces;

public interface ISpeciesRepository
{
    Task<Guid> AddAsync(Domain.Models.Species species, 
        CancellationToken cancellationToken = default);

    Task<Result<Domain.Models.Species, Error>> GetSpeciesByName(Name speciesName, 
        CancellationToken cancellationToken = default);

    Task<Result<Domain.Models.Species, Error>> GetSpeciesById(SpeciesId speciesId,
        CancellationToken cancellationToken = default);

    Task<Guid> Update(Domain.Models.Species species,
        CancellationToken cancellationToken = default);
    
    Task<Guid> Delete(Domain.Models.Species species, 
        CancellationToken cancellationToken = default);
}