using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetHelper.Application.Abstractions.Commands;
using PetHelper.Application.Database;
using PetHelper.Application.Extensions;
using PetHelper.Application.Species;
using PetHelper.Application.Volunteers.Commands.AddPet;
using PetHelper.Domain.Models.Breed;
using PetHelper.Domain.Models.Pet;
using PetHelper.Domain.Models.Species;
using PetHelper.Domain.Models.Volunteer;
using PetHelper.Domain.Shared;
using PetHelper.Domain.ValueObjects;
using PetHelper.Domain.ValueObjects.Common;
using PetHelper.Domain.ValueObjects.Pet;

namespace PetHelper.Application.Volunteers.Commands.UpdatePetCommand;

public class UpdatePetHandler : ICommandHandler<Guid,UpdatePetCommand>
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly ILogger<UpdatePetHandler> _logger;
    private readonly IValidator<UpdatePetCommand> _validator;
    private readonly IReadDbContext _readDbContext;
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePetHandler(
        IVolunteersRepository volunteersRepository,
        ILogger<UpdatePetHandler> logger,
        IValidator<UpdatePetCommand> validator,
        IReadDbContext readDbContext,
        IUnitOfWork unitOfWork)
    {
        _volunteersRepository = volunteersRepository;
        _logger = logger;
        _validator = validator;
        _readDbContext = readDbContext;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<Guid,ErrorList>> Handle(
        UpdatePetCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);

        if (validationResult.IsValid == false)
        {
            return validationResult.ToErrorList();
        }
        
        var volunteerId = VolunteerId.Create(command.VolunteerId);
        var volunteerResult = await  _volunteersRepository.GetVolunteerById(volunteerId, cancellationToken);

        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();
        
        var speciesId = SpeciesId.Create(command.SpeciesId);
        var breedId = BreedId.Create(command.BreedId);
        
        var isExistingSpeciesAndBreed =  
           await IsExistingSpeciesAndBreed(speciesId, breedId, cancellationToken);

        if (isExistingSpeciesAndBreed.IsFailure)
            return isExistingSpeciesAndBreed.Error.ToErrorList();
        
        var petToUpdate = volunteerResult.Value.Pets.FirstOrDefault(p=>p.Id == PetId.Create(command.PetId));

        if (petToUpdate == null)
            return Error.NotFound("pet.not.found",$"Pet with id = {command.PetId} not found" ).ToErrorList();
        
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
            new PetPhotoList([])
        );
        
        petToUpdate.Update(pet);
        
        await _volunteersRepository.Update(volunteerResult.Value, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        _logger.LogInformation("Updated pet fot volunteer with id {volunteerId}", volunteerId.Value);
        
        return pet.Id.Value;
    }
    
    private async Task<Result<bool, Error>> IsExistingSpeciesAndBreed(
        SpeciesId speciesId,
        BreedId breedId,
        CancellationToken cancellationToken = default)
    {
        var species = await _readDbContext.Species
            .FirstOrDefaultAsync(species => species.Id == speciesId, cancellationToken);
        
        if (species is null)
            return Errors.General.NotFound();

        var breed = await _readDbContext.Breeds
            .FirstOrDefaultAsync(breed => breed.Id == breedId.Value, cancellationToken);

        if (breed is null)
            return Errors.General.NotFound();

        return true;
    }
}