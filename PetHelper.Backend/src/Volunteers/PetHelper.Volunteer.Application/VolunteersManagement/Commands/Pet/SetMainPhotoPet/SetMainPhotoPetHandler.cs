using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PetHelper.Core;
using PetHelper.Core.Abstractions.Commands;
using PetHelper.Core.Extensions;
using PetHelper.SharedKernel;
using PetHelper.SharedKernel.ValueObjects;
using PetHelper.SharedKernel.ValueObjects.Pet;
using PetHelper.Volunteer.Domain.Ids;

namespace PetHelper.Volunteer.Application.VolunteersManagement.Commands.Pet.SetMainPhotoPet;

public class SetMainPhotoPetHandler : ICommandHandler<string,SetMainPhotoPetCommand>
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IValidator<SetMainPhotoPetCommand> _validator;
    private readonly ILogger<SetMainPhotoPetHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public SetMainPhotoPetHandler(
        IVolunteersRepository volunteersRepository,
        IValidator<SetMainPhotoPetCommand> validator,
        ILogger<SetMainPhotoPetHandler> logger,
        [FromKeyedServices(Constants.Context.VolunteerManagement)] IUnitOfWork unitOfWork)
    {
        _volunteersRepository = volunteersRepository;
        _validator = validator;
        _logger = logger;
        _unitOfWork = unitOfWork;
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
        
        var petPhotoResult = PetPhoto.Create(filePathResult.Value.Value);
        if (petPhotoResult.IsFailure)
            return petPhotoResult.Error;
        
        var setMainPhotoResult = volunteerResult.Value.SetMainPetPhoto(PetId.Create(command.PetId), petPhotoResult.Value);
        if(setMainPhotoResult.IsFailure)
            return setMainPhotoResult.Error;
        
        await _volunteersRepository.Update(volunteerResult.Value, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return command.PathPhoto;
    }
}