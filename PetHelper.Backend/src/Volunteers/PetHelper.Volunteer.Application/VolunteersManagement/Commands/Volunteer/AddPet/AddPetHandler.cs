using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PetHelper.Core;
using PetHelper.Core.Abstractions.Commands;
using PetHelper.SharedKernel;
using PetHelper.SharedKernel.ValueObjects;
using PetHelper.SharedKernel.ValueObjects.Common;
using PetHelper.SharedKernel.ValueObjects.ModelIds;
using PetHelper.SharedKernel.ValueObjects.Pet;
using PetHelper.Species.Contracts;
using PetHelper.Volunteer.Domain.Ids;

namespace PetHelper.Volunteer.Application.VolunteersManagement.Commands.Volunteer.AddPet;

public class AddPetHandler : ICommandHandler<Guid,AddPetCommand>
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly ISpeciesContract _speciesContract;
    private readonly ILogger<AddPetHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IReadDbContext _readDbContext;

    public AddPetHandler(
        IVolunteersRepository volunteersRepository,
        ISpeciesContract speciesContract,
        ILogger<AddPetHandler> logger,
        [FromKeyedServices(Constants.Context.VolunteerManagement)] IUnitOfWork unitOfWork,
        IReadDbContext readDbContext)
    {
        _volunteersRepository = volunteersRepository;
        _speciesContract = speciesContract;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _readDbContext = readDbContext;
    }
    
    public async Task<Result<Guid,ErrorList>> Handle(
        AddPetCommand command,
        CancellationToken cancellationToken = default)
    {
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
            photoEmptyList);

        volunteerResult.Value.AddPet(pet);
        
        await _volunteersRepository.Update(volunteerResult.Value, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        _logger.LogInformation("Created pet added for volunteer with id {volunteerId}", volunteerId);
    
        return pet.Id.Value;
    }
    
    
}