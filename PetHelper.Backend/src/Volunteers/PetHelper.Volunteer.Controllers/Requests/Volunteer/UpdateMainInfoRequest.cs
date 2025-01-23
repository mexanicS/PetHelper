using PetHelper.Core.DTOs;
using PetHelper.Volunteer.Application.VolunteersManagement.Commands.Volunteer.UpdateMainInfo;

namespace PetHelper.Volunteer.Controllers.Requests.Volunteer;

public record UpdateMainInfoRequest(
    string Email,
    string Description,
    int ExperienceInYears,
    string PhoneNumber,
    FullNameDto FullName)
{
    public UpdateMainInfoCommand ToCommand(Guid volunteerId) =>
        new(volunteerId, Email, Description, ExperienceInYears, PhoneNumber, FullName);
}