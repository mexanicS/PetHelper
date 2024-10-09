using PetHelper.Application.DTOs;

namespace PetHelper.Application.Volunteers.UpdateMainInfo;

public record UpdateMainInfoCommand(
    Guid Id,
    string Email,
    string Description,
    int ExperienceInYears,
    string PhoneNumber,
    FullNameDto FullName);