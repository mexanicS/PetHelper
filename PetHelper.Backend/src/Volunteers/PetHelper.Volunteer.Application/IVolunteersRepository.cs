using CSharpFunctionalExtensions;
using PetHelper.SharedKernel;
using PetHelper.Volunteer.Domain.Ids;

namespace PetHelper.Volunteer.Application;

public interface IVolunteersRepository
{
    Task<Guid> AddAsync(Domain.Volunteer volunteer, CancellationToken cancellationToken = default);
    
    Task<Guid> Update(Domain.Volunteer volunteer, CancellationToken cancellationToken = default);
    
    Task<Guid> Delete(Domain.Volunteer volunteer, CancellationToken cancellationToken = default);
    
    Task<Result<Domain.Volunteer, Error>> GetVolunteerById(VolunteerId volunteerId,
        CancellationToken cancellationToken = default);
}