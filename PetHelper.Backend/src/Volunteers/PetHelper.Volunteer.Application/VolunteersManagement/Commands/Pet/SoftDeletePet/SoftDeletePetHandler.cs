using System.Runtime.InteropServices.JavaScript;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PetHelper.Core;
using PetHelper.Core.Abstractions.Commands;
using PetHelper.Core.Extensions;
using PetHelper.SharedKernel;
using PetHelper.Volunteer.Domain.Ids;

namespace PetHelper.Volunteer.Application.VolunteersManagement.Commands.Pet.SoftDeletePet;

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
        [FromKeyedServices(Constants.Context.VolunteerManagement)] IUnitOfWork unitOfWork)
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