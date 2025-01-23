using PetHelper.Core.Abstractions.Commands;
using PetHelper.Core.DTOs;

namespace PetHelper.Volunteer.Application.VolunteersManagement.Commands.Pet.UpdatePet;

public record UpdatePetCommand(
    Guid VolunteerId,
    Guid PetId,
    Guid SpeciesId,
    Guid BreedId,
    string Name,
    string TypePet,
    string Description,
    string Color,
    string HealthInformation,
    double Weight,
    double Height,
    string PhoneNumber,
    bool IsNeutered,
    DateOnly? BirthDate,
    bool IsVaccinated,
    string City, 
    string Street,
    string HouseNumber,
    string? ZipCode,
    DetailsForAssistanceListDto DetailsForAssistances) : ICommand;