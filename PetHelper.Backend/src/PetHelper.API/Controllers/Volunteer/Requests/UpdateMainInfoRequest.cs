using PetHelper.Application.DTOs;
using PetHelper.Application.Volunteers.Commands.UpdateMainInfo;

namespace PetHelper.API.Controllers.Volunteer.Requests;

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