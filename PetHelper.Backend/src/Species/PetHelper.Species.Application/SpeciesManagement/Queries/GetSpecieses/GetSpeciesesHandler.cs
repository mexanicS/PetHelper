using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetHelper.Core.Abstractions.Queries;
using PetHelper.Core.DTOs.ReadDtos;
using PetHelper.SharedKernel;
using PetHelper.Species.Application.Database;

namespace PetHelper.Species.Application.SpeciesManagement.Queries.GetSpecieses;

public class GetSpeciesesHandler 
    : IQueryHandler<List<SpeciesDto>, GetSpeciesesQuery>
{
    private readonly IReadDbContext _readDbContext;
    
    public GetSpeciesesHandler(IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }
    public async Task<Result<List<SpeciesDto>,ErrorList>> Handle(
        GetSpeciesesQuery query, 
        CancellationToken cancellationToken)
    {
        return await _readDbContext.Species.ToListAsync(cancellationToken);
    }
}