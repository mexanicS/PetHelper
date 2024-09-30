using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetHelper.Domain.Models;
using PetHelper.Domain.Shared;
using PetHelper.Domain.ValueObjects;

namespace PetHelper.Application.Volunteers.UpdateDetailsForAssistance;

public class UpdateDetailsForAssistanceHandler
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly ILogger<UpdateDetailsForAssistanceHandler> _logger;

    public UpdateDetailsForAssistanceHandler(
        IVolunteersRepository volunteersRepository,
        ILogger<UpdateDetailsForAssistanceHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _logger = logger;
    }
    
    public async Task<Result<Guid,Error>> Handle(
        UpdateDetailsForAssistanceRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var volunteerResult = await _volunteersRepository.
            GetVolunteerById(VolunteerId.Create(request.VolunteerId), cancellationToken);
        
        if (volunteerResult.IsFailure)
            return volunteerResult.Error;
        
        var detailsForAssistance = new DetailsForAssistanceList(
            request.UpdateDetailsForAssistanceRequestDto.DetailsForAssistanceListDto.DetailsForAssistances
                .Select(c => DetailsForAssistance.Create(c.Name, c.Description).Value)
        );
        
        volunteerResult.Value.UpdateDetailsForAssistance(detailsForAssistance);
        
        await _volunteersRepository.Save(volunteerResult.Value, cancellationToken);
            
        _logger.LogInformation("The list of details for assistance for user {volunteerId} has been updated", request.VolunteerId);
        
        return volunteerResult.Value.Id.Value;
    }

}