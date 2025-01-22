using PetHelper.Core.Abstractions.Commands;
using PetHelper.Core.DTOs;

namespace PetHelper.Volunteer.Application.VolunteersManagement.Commands.Volunteer.UpdateMainInfo;

public record UpdateMainInfoCommand(
    Guid Id,
    string Email,
    string Description,
    int ExperienceInYears,
    string PhoneNumber,
    FullNameDto FullName) : ICommand;