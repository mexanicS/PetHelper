using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetHelper.Application.DTOs.Pet;
using PetHelper.Application.FileProvider;
using PetHelper.Application.Messaging;
using PetHelper.Application.Providers;
using PetHelper.Domain.Models;
using PetHelper.Domain.Shared;
using PetHelper.Domain.ValueObjects;
using PetHelper.Domain.ValueObjects.Pet;
using FileInfo = PetHelper.Application.FileProvider.FileInfo;
using PetPhoto = PetHelper.Domain.ValueObjects.Pet.PetPhoto;

namespace PetHelper.Application.Volunteers.AddPetPhotos;

public class AddPetPhotoHandler
{
    private readonly string BUCKET_NAME = "photos";
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IFileProvider _fileProvider;
    private readonly ILogger<AddPetPhotoHandler> _logger;
    private readonly IValidator<AddPetPhotosCommand> _validator;
    private readonly IMessageQueue<IEnumerable<FileInfo>> _messageQueue;

    public AddPetPhotoHandler(
        IVolunteersRepository volunteersRepository,
        IFileProvider fileProvider,
        ILogger<AddPetPhotoHandler> logger,
        IValidator<AddPetPhotosCommand> validator,
        IMessageQueue<IEnumerable<FileInfo>> messageQueue)
    {
        _volunteersRepository = volunteersRepository;
        _fileProvider = fileProvider;
        _logger = logger;
        _validator = validator;
        _messageQueue = messageQueue;
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
        
        List<FileData> files = [];

        try
        {
            foreach (var file in command.Files)
            {
                var extension = Path.GetExtension(file.FileName);
                var filePathResult = FilePath.Create(Guid.NewGuid(), extension);
                if(filePathResult.IsFailure)
                    return filePathResult.Error;

                var fileToUpload = new FileData(
                    file.Content, new FileInfo(filePathResult.Value, BUCKET_NAME));
                
                files.Add(fileToUpload);
            }
            
            var uploadResult = await _fileProvider
                .UploadFiles(files, BUCKET_NAME, cancellationToken);

            if (uploadResult.IsFailure)
            {
                await _messageQueue.WriteAsync(files.Select(f=>f.FileInfo), cancellationToken);
                return uploadResult.Error;
            }
                
            
            var petPhotos = new PetPhotoList(files.Select(x => PetPhoto.Create(x.FileInfo.FilePath).Value));
            
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