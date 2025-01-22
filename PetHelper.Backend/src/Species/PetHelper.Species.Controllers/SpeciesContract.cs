using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetHelper.SharedKernel;
using PetHelper.SharedKernel.ValueObjects.ModelIds;
using PetHelper.Species.Application.Database;
using PetHelper.Species.Contracts;

namespace PetHelper.Species.Controllers;

public class SpeciesContracts : ISpeciesContract
{
    private readonly IReadDbContext _readDbContext;

    public SpeciesContracts( 
        IReadDbContext readDbContext,
        ILogger<SpeciesContracts> logger)
    {
        _readDbContext = readDbContext;
    }
    public async Task<Result<bool, Error>> IsExistingSpeciesAndBreed(
        SpeciesId speciesId,
        BreedId breedId,
        CancellationToken cancellationToken = default)
    {
        var species = await _readDbContext.Species
            .FirstOrDefaultAsync(species => species.Id == speciesId, cancellationToken);
        
        if (species is null)
            return Errors.General.NotFound();

        var breed = await _readDbContext.Breeds
            .FirstOrDefaultAsync(breed => breed.Id == breedId.Value, cancellationToken);

        if (breed is null)
            return Errors.General.NotFound();

        return true;
    }
}