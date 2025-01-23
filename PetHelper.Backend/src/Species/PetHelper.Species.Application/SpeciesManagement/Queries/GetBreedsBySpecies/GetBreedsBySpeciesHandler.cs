using Microsoft.EntityFrameworkCore;
using PetHelper.Core.Abstractions.Queries;
using PetHelper.Core.DTOs.ReadDtos;
using PetHelper.Species.Application.Database;

namespace PetHelper.Species.Application.SpeciesManagement.Queries.GetBreedsBySpecies;

public class GetBreedsBySpeciesHandler 
    : IQueryHandler<List<BreedDto>, GetBreedsBySpeciesQuery>
{
    private readonly IReadDbContext _readDbContext;
    
    public GetBreedsBySpeciesHandler(IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }
    
    public async Task<List<BreedDto>> Handle(
        GetBreedsBySpeciesQuery query, 
        CancellationToken cancellationToken)
    {
        var breeds =  _readDbContext.Breeds
            .Where(b => b.SpeciesId == query.SpeciesId);
        
        return await breeds.ToListAsync(cancellationToken);
    }
}