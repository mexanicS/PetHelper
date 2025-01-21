using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetHelper.Application.Abstractions.Commands;
using PetHelper.Application.Database;
using PetHelper.Application.Extensions;
using PetHelper.Domain.Models.Pet;
using PetHelper.Domain.Models.Volunteer;
using PetHelper.Domain.Shared;

namespace PetHelper.Application.Volunteers.Commands.SoftDeletePet;

public class SoftDeletePetHandler  : ICommandHandler<Guid,SoftDeletePetCommand>
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IValidator<SoftDeletePetCommand> _validator;
    private readonly ILogger<SoftDeletePetHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public SoftDeletePetHandler(
        IVolunteersRepository volunteersRepository,
        IValidator<SoftDeletePetCommand> validator,
        ILogger<SoftDeletePetHandler> logger,
        IUnitOfWork unitOfWork)
    {
        _volunteersRepository = volunteersRepository;
        _validator = validator;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        SoftDeletePetCommand command, 
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
        
        pet.Delete();
        
        await _volunteersRepository.Update(volunteerResult.Value, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        _logger.LogInformation("Pet with id = {petId} soft deleted", command.PetId);
        
        return pet.Id.Value;
    }
}