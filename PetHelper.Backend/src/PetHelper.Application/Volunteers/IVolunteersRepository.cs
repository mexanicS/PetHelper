using CSharpFunctionalExtensions;
using PetHelper.Domain.Models;
using PetHelper.Domain.Shared;

namespace PetHelper.Application.Volunteers;

public interface IVolunteersRepository
{
    Task<Guid> Add(Volunteer volunteer, CancellationToken cancellationToken = default);
    
    Task<Guid> Update(Volunteer volunteer, CancellationToken cancellationToken = default);

    Task<Result<Volunteer, Error>> GetVolunteerById(VolunteerId volunteerId,
        CancellationToken cancellationToken = default);
}