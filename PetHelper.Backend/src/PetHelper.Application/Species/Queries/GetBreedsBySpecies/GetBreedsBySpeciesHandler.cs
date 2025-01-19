using Microsoft.EntityFrameworkCore;
using PetHelper.Application.Abstractions.Queries;
using PetHelper.Application.Database;
using PetHelper.Application.DTOs.ReadDtos;
using PetHelper.Application.Species.Queries.GetSpecieses;

namespace PetHelper.Application.Species.Queries.GetBreedsBySpecies;

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