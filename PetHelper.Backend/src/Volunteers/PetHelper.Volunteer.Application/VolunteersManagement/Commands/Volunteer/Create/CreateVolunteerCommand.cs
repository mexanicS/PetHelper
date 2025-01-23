using PetHelper.Core.Abstractions.Commands;
using PetHelper.Core.DTOs;

namespace PetHelper.Volunteer.Application.VolunteersManagement.Commands.Volunteer.Create;

public record CreateVolunteerCommand(
    string Email,
    string Description,
    int ExperienceInYears,
    string PhoneNumber,
    FullNameDto FullName,
    DetailsForAssistanceListDto DetailsForAssistances,
    SocialNetworkListDto SocialNetworks) : ICommand;

