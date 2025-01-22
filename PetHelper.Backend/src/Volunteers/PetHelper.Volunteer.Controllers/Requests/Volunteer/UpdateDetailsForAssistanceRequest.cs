using PetHelper.Core.DTOs;
using PetHelper.Volunteer.Application.VolunteersManagement.Commands.Volunteer.UpdateDetailsForAssistance;

namespace PetHelper.Volunteer.Controllers.Requests.Volunteer;

public record UpdateDetailsForAssistanceRequest(
    UpdateDetailsForAssistanceRequestDto UpdateDetailsForAssistanceCommandDto)
{
    public UpdateDetailsForAssistanceCommand ToCommand(Guid volunteerId) => 
        new(volunteerId, UpdateDetailsForAssistanceCommandDto.ToCommand());
}

public record UpdateDetailsForAssistanceRequestDto(DetailsForAssistanceListDto DetailsForAssistanceDto)
{
    public UpdateDetailsForAssistanceCommandDto ToCommand() =>
        new(DetailsForAssistanceDto);
}