using CSharpFunctionalExtensions;
using PetHelper.Application.Providers;
using PetHelper.Domain.Models;
using PetHelper.Domain.Shared;
using PetHelper.Domain.ValueObjects;

namespace PetHelper.Application.Volunteers.AddPet;

public class AddPetHandler
{
    private readonly IFileProvider _fileProvider;
    private readonly IVolunteersRepository _volunteersRepository;

    public AddPetHandler(
        IFileProvider fileProvider,
        IVolunteersRepository volunteersRepository)
    {
        _fileProvider = fileProvider;
        _volunteersRepository = volunteersRepository;
    }
    
    public async Task<Result<Guid,Error>> Handle(
        AddPetCommand command,
        CancellationToken cancellationToken = default)
    {
        var volunteerId = VolunteerId.Create(command.VolunteerId);
        
        var volunteerResult = await  _volunteersRepository.GetVolunteerById(volunteerId, cancellationToken);

        if (volunteerResult.IsFailure)
            return volunteerResult.Error;
        
        var petId = PetId.NewId();
        var name = Name.Create(command.Name).Value;
        var typePet = TypePet.Create(command.TypePet).Value;
        var description = Description.Create(command.Description).Value;
        var color = Color.Create(command.Color).Value;
        var healthInformation = HealthInformation.Create(command.HealthInformation).Value;
        var weight = Weight.Create(command.Weight).Value;
        var height = Height.Create(command.Height).Value;
        var phoneNumber = PhoneNumber.Create(command.PhoneNumber).Value;
        var address = Address.Create(
            command.City,
            command.Street,
            command.HouseNumber,
            command.ZipCode).Value;
        
        var breed = Breed.Create("CAT").Value;
        var speciesBreed = SpeciesBreed
            .Create(SpeciesId.Create(command.SpeciesId), command.BreedId).Value;
        
        
        var pet = new Pet(
            petId,
            name,
            typePet,
            description,
            breed,
            color,
            healthInformation,
            weight,
            height,
            phoneNumber,
            command.IsNeutered,
            command.BirthDate,
            command.IsVaccinated,
           DateTime.UtcNow, 
            address,
            speciesBreed);

        volunteerResult.Value.AddPet(pet);
        
        await _volunteersRepository.Save(volunteerResult.Value, cancellationToken);
        
        return pet.Id.Value;
    }
}