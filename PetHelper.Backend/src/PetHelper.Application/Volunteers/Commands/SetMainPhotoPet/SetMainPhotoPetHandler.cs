using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetHelper.Application.Abstractions.Commands;
using PetHelper.Application.Extensions;
using PetHelper.Domain.Models.Pet;
using PetHelper.Domain.Models.Volunteer;
using PetHelper.Domain.Shared;
using PetHelper.Domain.ValueObjects;
using PetHelper.Domain.ValueObjects.Pet;

namespace PetHelper.Application.Volunteers.Commands.SetMainPhotoPet;

public class SetMainPhotoPetHandler : ICommandHandler<string,SetMainPhotoPetCommand>
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IValidator<SetMainPhotoPetCommand> _validator;
    private readonly ILogger<SetMainPhotoPetHandler> _logger;

    public SetMainPhotoPetHandler(
        IVolunteersRepository volunteersRepository,
        IValidator<SetMainPhotoPetCommand> validator,
        ILogger<SetMainPhotoPetHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _validator = validator;
        _logger = logger;
    }
    
    public async Task<Result<string, ErrorList>> Handle(
        SetMainPhotoPetCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);

        if (validationResult.IsValid == false)
        {
            return validationResult.ToErrorList();
        }
        
        var volunteerId = VolunteerId.Create(command.VolunteerId);
        var volunteerResult = await _volunteersRepository
            .GetVolunteerById(volunteerId, cancellationToken);
        
        if(volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();
        
        var filePathResult = FilePath.Create(command.PathPhoto);
        if(filePathResult.IsFailure)
            return filePathResult.Error.ToErrorList();
        
        var petPhotoResult = PetPhoto.Create(filePathResult.Value);
        if (petPhotoResult.IsFailure)
            return petPhotoResult.Error;
        
        var setMainPhotoResult = volunteerResult.Value.SetMainPetPhoto(PetId.Create(command.PetId), petPhotoResult.Value);
        if(setMainPhotoResult.IsFailure)
            return setMainPhotoResult.Error;
        
        await _volunteersRepository.Save(volunteerResult.Value, cancellationToken);
        
        return command.PathPhoto;
    }
}