using PetHelper.Core.DTOs;
using PetHelper.Volunteer.Application.VolunteersManagement.Commands.Volunteer.Create;

namespace PetHelper.Volunteer.Controllers.Requests.Volunteer;

public record CreateVolunteerRequest(
    string Email,
    string Description,
    int ExperienceInYears,
    string PhoneNumber,
    FullNameDto FullName,
    DetailsForAssistanceListDto DetailsForAssistances,
    SocialNetworkListDto SocialNetworks)
{
    public CreateVolunteerCommand ToCommand() =>
        new(Email, 
            Description, 
            ExperienceInYears, 
            PhoneNumber, 
            FullName, 
            DetailsForAssistances, 
            SocialNetworks);
}