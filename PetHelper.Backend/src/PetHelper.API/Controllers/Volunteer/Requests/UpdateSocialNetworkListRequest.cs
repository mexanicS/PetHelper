using PetHelper.Application.DTOs;
using PetHelper.Application.Volunteers.Commands.UpdateSocialNetworkList;

namespace PetHelper.API.Controllers.Volunteer.Requests;

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