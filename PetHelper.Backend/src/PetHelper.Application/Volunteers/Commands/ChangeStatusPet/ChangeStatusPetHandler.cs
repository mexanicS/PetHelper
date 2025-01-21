using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetHelper.Application.Abstractions.Commands;
using PetHelper.Application.Database;
using PetHelper.Application.Extensions;
using PetHelper.Domain.Models.Pet;
using PetHelper.Domain.Models.Volunteer;
using PetHelper.Domain.Shared;

namespace PetHelper.Application.Volunteers.Commands.ChangeStatusPet;

public class ChangeStatusPetHandler : ICommandHandler<Guid, ChangeStatusPetCommand>
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly ILogger<ChangeStatusPetHandler> _logger;
    private readonly IValidator<ChangeStatusPetCommand> _validator;
    private readonly IUnitOfWork _unitOfWork;

    public ChangeStatusPetHandler(
        IVolunteersRepository volunteersRepository,
        ILogger<ChangeStatusPetHandler> logger,
        IValidator<ChangeStatusPetCommand> validator,
        IUnitOfWork unitOfWork)
    {
        _volunteersRepository = volunteersRepository;
        _logger = logger;
        _validator = validator;
        _unitOfWork = unitOfWork;
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
        
        await _volunteersRepository.Update(volunteerResult.Value, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        _logger.LogInformation("Pet with id = {petId} status changed from {oldStatus} to {newStatus} fot volunteer with id {volunteerId}", 
            petId.Value, oldStatus, command.Status.ToString(), volunteerId.Value);
        
        return pet.Id.Value;
    }
}