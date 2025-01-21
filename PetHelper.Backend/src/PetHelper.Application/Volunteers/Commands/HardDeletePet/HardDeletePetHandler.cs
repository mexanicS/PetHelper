using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetHelper.Application.Abstractions.Commands;
using PetHelper.Application.Extensions;
using PetHelper.Application.Volunteers.Commands.SoftDeletePet;
using PetHelper.Domain.Models.Pet;
using PetHelper.Domain.Models.Volunteer;
using PetHelper.Domain.Shared;

namespace PetHelper.Application.Volunteers.Commands.HardDeletePet;

public class HardDeletePetHandler  : ICommandHandler<Guid,HardDeletePetCommand>
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IValidator<HardDeletePetCommand> _validator;
    private readonly ILogger<HardDeletePetHandler> _logger;

    public HardDeletePetHandler(
        IVolunteersRepository volunteersRepository,
        IValidator<HardDeletePetCommand> validator,
        ILogger<HardDeletePetHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _validator = validator;
        _logger = logger;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        HardDeletePetCommand command, 
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);

        if (validationResult.IsValid == false)
        {
            return validationResult.ToErrorList();
        }
        
        var volunteerId = VolunteerId.Create(command.VolunteerId);
        var volunteerResult = await _volunteersRepository.GetVolunteerById(volunteerId, cancellationToken);

        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();
        
        var petId = PetId.Create(command.PetId);
        var pet = volunteerResult.Value.Pets.FirstOrDefault(p => p.Id == petId);
        
        if(pet == null)
            return Error.NotFound("pet.not.found",
                $"Pet with id = {command.PetId} not found" ).ToErrorList();

        volunteerResult.Value.RemovePet(pet);
        
        await _volunteersRepository.Save(volunteerResult.Value, cancellationToken);
        
        _logger.LogInformation("Pet with id = {petId} hard deleted", command.PetId);
        
        return pet.Id.Value;
    }
}