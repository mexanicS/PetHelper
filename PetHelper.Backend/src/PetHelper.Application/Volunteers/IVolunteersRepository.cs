using CSharpFunctionalExtensions;
using PetHelper.Domain.Models;
using PetHelper.Domain.Models.Volunteer;
using PetHelper.Domain.Shared;
using PetHelper.Domain.ValueObjects;

namespace PetHelper.Application.Volunteers;

public interface IVolunteersRepository
{
    Task<Guid> Add(Volunteer volunteer, CancellationToken cancellationToken = default);
    
    Task<Guid> Save(Volunteer volunteer, CancellationToken cancellationToken = default);
    
    Task<Guid> Delete(Volunteer volunteer, CancellationToken cancellationToken = default);
    
    Task<Result<Volunteer, Error>> GetVolunteerById(VolunteerId volunteerId,
        CancellationToken cancellationToken = default);
}