using PetHelper.Core.Abstractions.Commands;
using PetHelper.Core.DTOs;

namespace PetHelper.Volunteer.Application.VolunteersManagement.Commands.Volunteer.UpdateSocialNetworkList;

public record UpdateSocialNetworkListCommand(Guid VolunteerId, 
    UpdateSocialNetworkListCommandDto UpdateSocialNetworkListCommandDto) : ICommand;

public record UpdateSocialNetworkListCommandDto(SocialNetworkListDto SocialNetworkListDto);