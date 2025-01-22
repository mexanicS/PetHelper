using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetHelper.SharedKernel;
using PetHelper.SharedKernel.ValueObjects.Common;
using PetHelper.SharedKernel.ValueObjects.ModelIds;
using PetHelper.Species.Application.Interfaces;
using PetHelper.Species.Infastructure.DbContexts;

namespace PetHelper.Species.Infastructure;

public class SpeciesRepository : ISpeciesRepository
{
    private readonly SpeciesWriteDbContext _dbContext;

    public SpeciesRepository(SpeciesWriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Guid> AddAsync(
        Domain.Models.Species species, 
        CancellationToken cancellationToken = default)
    {
        await _dbContext.Species.AddAsync(species, cancellationToken);
        
        return (Guid)species.Id;
    }
    
    public async Task<Result<Domain.Models.Species, Error>> GetSpeciesByName(
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
    
    public async Task<Result<Domain.Models.Species, Error>> GetSpeciesById(
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
        Domain.Models.Species species, 
        CancellationToken cancellationToken = default)
    {
        _dbContext.Species.Update(species);
        
        return Task.FromResult<Guid>(species.Id);
    }

    public Task<Guid> Delete(Domain.Models.Species species, CancellationToken cancellationToken = default)
    {
        _dbContext.Species.Remove(species);
        
        return Task.FromResult<Guid>(species.Id);
    }
}