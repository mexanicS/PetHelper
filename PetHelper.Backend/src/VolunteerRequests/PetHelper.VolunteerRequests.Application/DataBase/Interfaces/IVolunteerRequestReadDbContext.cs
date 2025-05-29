using PetHelper.VolunteerRequests.Application.DataBase.Dto;

namespace PetHelper.VolunteerRequests.Application.DataBase.Interfaces;

public interface IVolunteerRequestReadDbContext
{
    public IQueryable<VolunteerRequestDto> VolunteerRequests { get; }
}