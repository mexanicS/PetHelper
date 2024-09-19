using PetHelper.Domain.Models;

namespace PetHelper.Application.Volunteers;

public interface IVolunteersRepository
{
    Task<Guid> Add(Volunteer volunteer, CancellationToken cancellationToken = default);
}