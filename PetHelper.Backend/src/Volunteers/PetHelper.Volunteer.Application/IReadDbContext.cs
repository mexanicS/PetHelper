using PetHelper.Core.DTOs.ReadDtos;

namespace PetHelper.Volunteer.Application;

public interface IReadDbContext
{
    IQueryable<PetDto> Pets { get; }
    
    IQueryable<VolunteerDto> Volunteers { get; }
}