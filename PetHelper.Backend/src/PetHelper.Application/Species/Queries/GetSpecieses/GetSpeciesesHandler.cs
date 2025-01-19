using Microsoft.EntityFrameworkCore;
using PetHelper.Application.Abstractions.Queries;
using PetHelper.Application.Database;
using PetHelper.Application.DTOs.ReadDtos;
using PetHelper.Application.Extensions;
using PetHelper.Application.Models;
using PetHelper.Application.Volunteers.Queries.GetVolunteers;

namespace PetHelper.Application.Species.Queries.GetSpecieses;

public class GetSpeciesesHandler 
    : IQueryHandler<List<SpeciesDto>, GetSpeciesesQuery>
{
    private readonly IReadDbContext _readDbContext;
    
    public GetSpeciesesHandler(IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }
    public async Task<List<SpeciesDto>> Handle(
        GetSpeciesesQuery query, 
        CancellationToken cancellationToken)
    {
        return await _readDbContext.Species.ToListAsync(cancellationToken);
    }
}