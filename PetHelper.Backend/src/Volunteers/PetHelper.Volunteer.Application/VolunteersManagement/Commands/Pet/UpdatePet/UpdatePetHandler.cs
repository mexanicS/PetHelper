using System.Runtime.InteropServices.JavaScript;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PetHelper.Core;
using PetHelper.Core.Abstractions.Commands;
using PetHelper.Core.Extensions;
using PetHelper.SharedKernel;
using PetHelper.SharedKernel.ValueObjects;
using PetHelper.SharedKernel.ValueObjects.Common;
using PetHelper.SharedKernel.ValueObjects.ModelIds;
using PetHelper.SharedKernel.ValueObjects.Pet;
using PetHelper.Species.Contracts;
using PetHelper.Volunteer.Domain.Ids;

namespace PetHelper.Volunteer.Application.VolunteersManagement.Commands.Pet.UpdatePet;

public class UpdatePetHandler : ICommandHandler<Guid,UpdatePetCommand>
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly ILogger<UpdatePetHandler> _logger;
    private readonly IValidator<UpdatePetCommand> _validator;
    private readonly ISpeciesContract _speciesContract;
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePetHandler(
        IVolunteersRepository volunteersRepository,
        ILogger<UpdatePetHandler> logger,
        IValidator<UpdatePetCommand> validator,
        ISpeciesContract speciesContract,
        [FromKeyedServices(Constants.Context.VolunteerManagement)] IUnitOfWork unitOfWork)
    {
        _volunteersRepository = volunteersRepository;
        _logger = logger;
        _validator = validator;
        _speciesContract = speciesContract;
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
           await _speciesContract.IsExistingSpeciesAndBreed(speciesId, breedId, cancellationToken);

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
        
        var pet = new Domain.Pet(
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
}