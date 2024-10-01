using PetHelper.Application.DTOs;

namespace PetHelper.Application.Volunteers.UpdateSocialNetworkList;

public record UpdateSocialNetworkListRequest(Guid VolunteerId, 
    UpdateSocialNetworkListRequestDto UpdateSocialNetworkListRequestDto);

public record UpdateSocialNetworkListRequestDto(SocialNetworkListDto SocialNetworkListDto);