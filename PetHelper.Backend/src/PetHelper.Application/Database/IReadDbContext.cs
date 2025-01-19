using Microsoft.EntityFrameworkCore;
using PetHelper.Application.DTOs.ReadDtos;

namespace PetHelper.Application.Database;

public interface IReadDbContext
{
    IQueryable<PetDto> Pets { get; }
    
    IQueryable<VolunteerDto> Volunteers { get; }
    
    IQueryable<BreedDto> Breeds { get; }
    
    IQueryable<SpeciesDto> Species { get; }
}