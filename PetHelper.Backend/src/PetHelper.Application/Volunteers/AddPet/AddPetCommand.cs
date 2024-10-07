namespace PetHelper.Application.Volunteers.AddPet;

public record AddPetCommand(
    Guid VolunteerId,
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
    string? ZipCode);