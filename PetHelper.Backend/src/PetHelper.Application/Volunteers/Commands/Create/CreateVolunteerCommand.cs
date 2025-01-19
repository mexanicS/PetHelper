using PetHelper.Application.Abstractions;
using PetHelper.Application.Abstractions.Commands;
using PetHelper.Application.DTOs;

namespace PetHelper.Application.Volunteers.Commands.Create;

public record CreateVolunteerCommand(
    string Email,
    string Description,
    int ExperienceInYears,
    string PhoneNumber,
    FullNameDto FullName,
    DetailsForAssistanceListDto DetailsForAssistances,
    SocialNetworkListDto SocialNetworks) : ICommand;

