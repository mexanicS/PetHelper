using PetHelper.Application.DTOs;
using PetHelper.Application.Volunteers.Commands.UpdateDetailsForAssistance;
using PetHelper.Application.Volunteers.Commands.UpdateSocialNetworkList;

namespace PetHelper.API.Controllers.Volunteer.Requests;

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