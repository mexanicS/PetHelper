using PetHelper.Application.Abstractions;
using PetHelper.Application.Abstractions.Commands;
using PetHelper.Application.DTOs;

namespace PetHelper.Application.Volunteers.Commands.UpdateSocialNetworkList;

public record UpdateSocialNetworkListCommand(Guid VolunteerId, 
    UpdateSocialNetworkListCommandDto UpdateSocialNetworkListCommandDto) : ICommand;

public record UpdateSocialNetworkListCommandDto(SocialNetworkListDto SocialNetworkListDto);