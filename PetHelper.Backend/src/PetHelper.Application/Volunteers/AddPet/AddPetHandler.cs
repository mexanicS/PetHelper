using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetHelper.Application.Providers;
using PetHelper.Application.Species;
using PetHelper.Application.Volunteers.CreateVolunteers;
using PetHelper.Domain.Models;
using PetHelper.Domain.Shared;
using PetHelper.Domain.ValueObjects;
using PetHelper.Domain.ValueObjects.Pet;

namespace PetHelper.Application.Volunteers.AddPet;

public class AddPetHandler
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly ISpeciesRepository _speciesRepository;
    private readonly ILogger<AddPetHandler> _logger;

    public AddPetHandler(
        IVolunteersRepository volunteersRepository,
        ISpeciesRepository speciesRepository,
        ILogger<AddPetHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _speciesRepository = speciesRepository;
        _logger = logger;
    }
    
    public async Task<Result<Guid,Error>> Handle(
        AddPetCommand command,
        CancellationToken cancellationToken = default)
    {
        var volunteerId = VolunteerId.Create(command.VolunteerId);
        
        var volunteerResult = await  _volunteersRepository.GetVolunteerById(volunteerId, cancellationToken);

        if (volunteerResult.IsFailure)
            return volunteerResult.Error;
        
        var speciesId = SpeciesId.Create(command.SpeciesId);
        var breedId = BreedId.Create(command.BreedId);
        
        var isExistingSpeciesAndBreed =  
           await _speciesRepository.IsExistingSpeciesAndBreed(speciesId, breedId, cancellationToken);

        if (isExistingSpeciesAndBreed.IsFailure)
            return isExistingSpeciesAndBreed.Error;
        
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

        var speciesBreed = SpeciesBreed
            .Create(speciesId, breedId.Value).Value;
        
        var detailsForAssistance = new PetDetails(
            command.DetailsForAssistances.DetailsForAssistances
                .Select(c => DetailsForAssistance.Create(c.Name, c.Description).Value)
        );

        var photoEmptyList = new PetPhotoList(null);
        
        var pet = new Pet(
            petId,
            name,
            typePet,
            description,
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
            speciesBreed,
            detailsForAssistance,
            photoEmptyList);

        volunteerResult.Value.AddPet(pet);
        
        await _volunteersRepository.Save(volunteerResult.Value, cancellationToken);
        
        _logger.LogInformation("Created pet added fot volunteer with id {volunteerId}", volunteerId);
        
        return pet.Id.Value;
    }
}