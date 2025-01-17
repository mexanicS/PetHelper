using PetHelper.Application.DTOs;
using PetHelper.Application.Volunteers.Commands.Create;

namespace PetHelper.API.Controllers.Volunteer.Requests;

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
        new(
            Email, 
            Description, 
            ExperienceInYears, 
            PhoneNumber, 
            FullName, 
            DetailsForAssistances, 
            SocialNetworks);
}