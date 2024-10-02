using FluentValidation;
using PetHelper.Application.DTOs;
using PetHelper.Domain.ValueObjects;

namespace PetHelper.Application.Volunteers.CreateVolunteers;

public record CreateVolunteerRequest(
    string Email,
    string Description,
    int ExperienceInYears,
    string PhoneNumber,
    FullNameDto FullName,
    DetailsForAssistanceListDto DetailsForAssistances,
    SocialNetworkListDto SocialNetworks);

