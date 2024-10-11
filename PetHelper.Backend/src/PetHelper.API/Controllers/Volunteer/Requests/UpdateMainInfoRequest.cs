using PetHelper.Application.DTOs;

namespace PetHelper.API.Controllers.Volunteer.Requests;

public record UpdateMainInfoRequest(string Email,
    string Description,
    int ExperienceInYears,
    string PhoneNumber,
    FullNameDto FullName);