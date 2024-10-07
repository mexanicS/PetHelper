using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetHelper.Application.Species;
using PetHelper.Domain.Models;
using PetHelper.Domain.Models.Species;
using PetHelper.Domain.Shared;
using PetHelper.Domain.ValueObjects;
using Breed = PetHelper.Domain.Models.Breed.Breed;

namespace PetHelper.Infastructure.Repository;

public class SpeciesRepository : ISpeciesRepository
{
    private readonly ApplicationDbContext _dbContext;

    public SpeciesRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Guid> Add(
        Species species, 
        CancellationToken cancellationToken = default)
    {
        await _dbContext.Specieses.AddAsync(species, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return (Guid)species.Id;
    }
    
    public async Task<Result<Species, Error>> GetSpeciesByName(
        Name speciesName, 
        CancellationToken cancellationToken = default)
    { 
        var species= await _dbContext.Specieses
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
        var species= await _dbContext.Specieses
            .Include(s=>s.Breeds)
            .FirstOrDefaultAsync(species => species.Id == speciesId, cancellationToken);

        if (species is null)
            return Errors.General.NotFound();
        
        return species;
    }
    
    public async Task<Guid> Save(
        Species species, 
        CancellationToken cancellationToken = default)
    {
        _dbContext.Specieses.Attach(species);
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return species.Id;
    }
}