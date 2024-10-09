using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetHelper.Application.DTOs.Pet;
using PetHelper.Application.Providers;
using PetHelper.Domain.Models;
using PetHelper.Domain.Shared;
using PetHelper.Domain.ValueObjects;
using PetHelper.Domain.ValueObjects.Pet;
using PetPhoto = PetHelper.Domain.ValueObjects.Pet.PetPhoto;

namespace PetHelper.Application.Volunteers.AddPetPhotos;

public class AddPetPhotoHandler
{
    private readonly string BUCKET_NAME = "photos";
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IFileProvider _fileProvider;
    private readonly ILogger<AddPetPhotoHandler> _logger;
    private readonly IValidator<AddPetPhotosCommand> _validator;

    public AddPetPhotoHandler(
        IVolunteersRepository volunteersRepository,
        IFileProvider fileProvider,
        ILogger<AddPetPhotoHandler> logger,
        IValidator<AddPetPhotosCommand> validator)
    {
        _volunteersRepository = volunteersRepository;
        _fileProvider = fileProvider;
        _logger = logger;
        _validator = validator;
    }

    public async Task<UnitResult<Error>> Handle(
        AddPetPhotosCommand command,
        CancellationToken cancellationToken)
    {
        var volunteerId = VolunteerId.Create(command.VolunteerId);
        var volunteerResult = await _volunteersRepository
            .GetVolunteerById(volunteerId, cancellationToken);
        
        if(volunteerResult.IsFailure)
            return volunteerResult.Error;
        
        var petId = PetId.Create(command.PetId);
        var petResult = volunteerResult.Value.GetPetById(petId);
        
        if(petResult.IsFailure)
            return petResult.Error;
        
        List<UploadingFileDto> files = [];

        try
        {
            foreach (var file in command.Files)
            {
                var extension = Path.GetExtension(file.FileName);
                var filePathResult = FilePath.Create(Guid.NewGuid(), extension);
                if(filePathResult.IsFailure)
                    return filePathResult.Error;

                var fileToUpload = new UploadingFileDto(
                    filePathResult.Value,
                    file.Content);
                
                files.Add(fileToUpload);
            }
            
            var uploadResult = await _fileProvider
                .UploadFiles(files,BUCKET_NAME ,cancellationToken);

            if (uploadResult.IsFailure)
                return uploadResult.Error;

            var petPhotos = PetPhotoList.Create(files.Select(x => PetPhoto.Create(x.FilePath).Value));
            
            petResult.Value.UpdatePhotos(petPhotos);
            
            await _volunteersRepository.Save(volunteerResult.Value, cancellationToken);

            return Result.Success<Error>();
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Error occured while uploading pet photos to volunteer with id {id}",
                command.VolunteerId);
            
            return Error.Failure("volunteer.pet.photos.failure",
                "Error occured while uploading pet photos");
        }
    }
    
    
}