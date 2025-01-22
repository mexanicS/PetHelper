using PetHelper.Core.DTOs;
using PetHelper.Volunteer.Application.VolunteersManagement.Commands.Volunteer.UpdateSocialNetworkList;

namespace PetHelper.Volunteer.Controllers.Requests.Volunteer;

public record UpdateSocialNetworkListRequest(
    UpdateSocialNetworkListRequestDto UpdateSocialNetworkListCommandDto)
{
    public UpdateSocialNetworkListCommand ToCommand(Guid volunteerId) => 
        new(volunteerId, UpdateSocialNetworkListCommandDto.ToCommand());
}

public record UpdateSocialNetworkListRequestDto(SocialNetworkListDto SocialNetworkListDto)
{
    public UpdateSocialNetworkListCommandDto ToCommand() =>
        new(SocialNetworkListDto);
}