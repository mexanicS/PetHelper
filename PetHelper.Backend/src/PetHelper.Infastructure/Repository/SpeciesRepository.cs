using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetHelper.Application.Species;
using PetHelper.Domain.Models;
using PetHelper.Domain.Models.Breed;
using PetHelper.Domain.Models.Species;
using PetHelper.Domain.Shared;
using PetHelper.Domain.ValueObjects;
using PetHelper.Domain.ValueObjects.Common;
using PetHelper.Infastructure.DbContexts;
using Breed = PetHelper.Domain.Models.Breed.Breed;

namespace PetHelper.Infastructure.Repository;

public class SpeciesRepository : ISpeciesRepository
{
    private readonly WriteDbContext _dbContext;

    public SpeciesRepository(WriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Guid> AddAsync(
        Species species, 
        CancellationToken cancellationToken = default)
    {
        await _dbContext.Species.AddAsync(species, cancellationToken);
        
        return (Guid)species.Id;
    }
    
    public async Task<Result<Species, Error>> GetSpeciesByName(
        Name speciesName, 
        CancellationToken cancellationToken = default)
    { 
        var species= await _dbContext.Species
            .Include(s=>s.Breeds)
            .FirstOrDefaultAsync(species => species.Name == speciesName, cancellationToken);

        if (species is null)
            return Errors.General.NotFound();
        
        return species;
    }
    
    public async Task<Result<Species, Error>> GetSpeciesById(
        SpeciesId speciesId, 
        CancellationToken cancellationToken = default)
    { 
        var species= await _dbContext.Species
            .Include(s=>s.Breeds)
            .FirstOrDefaultAsync(species => species.Id == speciesId, cancellationToken);

        if (species is null)
            return Errors.General.NotFound();
        
        return species;
    }

    public Task<Guid> Update(
        Species species, 
        CancellationToken cancellationToken = default)
    {
        _dbContext.Species.Update(species);
        
        return Task.FromResult<Guid>(species.Id);
    }

    public Task<Guid> Delete(Species species, CancellationToken cancellationToken = default)
    {
        _dbContext.Species.Remove(species);
        
        return Task.FromResult<Guid>(species.Id);
    }
}