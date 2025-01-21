using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetHelper.Application.Abstractions.Commands;
using PetHelper.Application.Extensions;
using PetHelper.Application.FileProvider;
using PetHelper.Application.Messaging;
using PetHelper.Application.Volunteers.Commands.AddPetPhotos;
using PetHelper.Domain.Models.Pet;
using PetHelper.Domain.Models.Volunteer;
using PetHelper.Domain.Shared;
using PetHelper.Domain.ValueObjects;
using PetHelper.Domain.ValueObjects.Pet;
using FileInfo = System.IO.FileInfo;

namespace PetHelper.Application.Volunteers.Commands.ChangeStatusPet;

public class ChangeStatusPetHandler : ICommandHandler<Guid, ChangeStatusPetCommand>
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly ILogger<ChangeStatusPetHandler> _logger;
    private readonly IValidator<ChangeStatusPetCommand> _validator;

    public ChangeStatusPetHandler(
        IVolunteersRepository volunteersRepository,
        ILogger<ChangeStatusPetHandler> logger,
        IValidator<ChangeStatusPetCommand> validator)
    {
        _volunteersRepository = volunteersRepository;
        _logger = logger;
        _validator = validator;
    }

    public async Task<Result<Guid,ErrorList>> Handle(
        ChangeStatusPetCommand command,
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
        var oldStatus = pet.Status.ToString();
        
        var status = Enum.Parse<Constants.StatusPet>(command.Status);
        
        pet.ChangeStatus(status);
        
        await _volunteersRepository.Save(volunteerResult.Value, cancellationToken);
        
        _logger.LogInformation("Pet with id = {petId} status changed from {oldStatus} to {newStatus} fot volunteer with id {volunteerId}", 
            petId.Value, oldStatus, command.Status.ToString(), volunteerId.Value);
        
        return pet.Id.Value;
    }
}