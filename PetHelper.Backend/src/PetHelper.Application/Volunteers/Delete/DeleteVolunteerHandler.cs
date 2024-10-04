using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetHelper.Domain.Models;
using PetHelper.Domain.Shared;

namespace PetHelper.Application.Volunteers.DeleteVolunteer;

public class DeleteVolunteerHandler
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly ILogger<DeleteVolunteerHandler> _logger;

    public DeleteVolunteerHandler(
        IVolunteersRepository volunteersRepository, 
        ILogger<DeleteVolunteerHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _logger = logger;
    }
    
    public async Task<Result<Guid,Error>> Handle(
        DeleteVolunteerRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var volunteerResult = await _volunteersRepository.
            GetVolunteerById(VolunteerId.Create(request.VolunteerId), cancellationToken);
        
        if (volunteerResult.IsFailure)
            return volunteerResult.Error;
        
        await _volunteersRepository.Delete(volunteerResult.Value, cancellationToken);
            
        _logger.LogInformation("Volunteer with id = {volunteerId} deleted", request.VolunteerId);
        
        return volunteerResult.Value.Id.Value;
    }
}