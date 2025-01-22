using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PetHelper.Core;
using PetHelper.Core.Abstractions.Commands;
using PetHelper.Core.FileProvider;
using PetHelper.Core.Messaging;
using PetHelper.Core.Providers;
using PetHelper.SharedKernel;
using PetHelper.SharedKernel.ValueObjects;
using PetHelper.SharedKernel.ValueObjects.Pet;
using PetHelper.Volunteer.Domain.Ids;
using FileInfo = PetHelper.Core.FileProvider.FileInfo;

namespace PetHelper.Volunteer.Application.VolunteersManagement.Commands.Volunteer.AddPetPhotos;

public class AddPetPhotoHandler : ICommandHandler<Guid, AddPetPhotosCommand>
{
    private readonly string BUCKET_NAME = "photos";
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IFileProvider _fileProvider;
    private readonly ILogger<AddPetPhotoHandler> _logger;
    private readonly IValidator<AddPetPhotosCommand> _validator;
    private readonly IMessageQueue<IEnumerable<FileInfo>> _messageQueue;
    private readonly IUnitOfWork _unitOfWork;

    public AddPetPhotoHandler(
        IVolunteersRepository volunteersRepository,
        IFileProvider fileProvider,
        ILogger<AddPetPhotoHandler> logger,
        IValidator<AddPetPhotosCommand> validator,
        IMessageQueue<IEnumerable<FileInfo>> messageQueue,
        [FromKeyedServices(Constants.Context.VolunteerManagement)] IUnitOfWork unitOfWork)
    {
        _volunteersRepository = volunteersRepository;
        _fileProvider = fileProvider;
        _logger = logger;
        _validator = validator;
        _messageQueue = messageQueue;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        AddPetPhotosCommand command,
        CancellationToken cancellationToken)
    {
        var volunteerId = VolunteerId.Create(command.VolunteerId);
        var volunteerResult = await _volunteersRepository
            .GetVolunteerById(volunteerId, cancellationToken);
        
        if(volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();
        
        var petId = PetId.Create(command.PetId);
        var petResult = volunteerResult.Value.GetPetById(petId);
        
        if(petResult.IsFailure)
            return petResult.Error.ToErrorList();
        
        List<FileData> files = [];
        var transaction = await _unitOfWork.BeginTransaction(cancellationToken);
        try
        {
            foreach (var file in command.Files)
            {
                var extension = Path.GetExtension(file.FileName);
                var filePathResult = FilePath.Create(Guid.NewGuid(), extension);
                if(filePathResult.IsFailure)
                    return filePathResult.Error.ToErrorList();

                var fileToUpload = new FileData(
                    file.Content, new FileInfo(filePathResult.Value, BUCKET_NAME));
                
                files.Add(fileToUpload);
            }
            
            var uploadResult = await _fileProvider
                .UploadFiles(files, BUCKET_NAME, cancellationToken);

            if (uploadResult.IsFailure)
            {
                await _messageQueue.WriteAsync(files.Select(f=>f.FileInfo), cancellationToken);
                return uploadResult.Error.ToErrorList();
            }
                
            var petPhotos = new PetPhotoList(files.Select(x => PetPhoto.Create(x.FileInfo.FilePath.Value).Value));
            
            petResult.Value.UpdatePhotos(petPhotos);
            
            await _volunteersRepository.Update(volunteerResult.Value, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            transaction.Commit();
            
            return petResult.Value.Id.Value;
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            
            _logger.LogError(
                ex,
                "Error occured while uploading pet photos to volunteer with id {id}",
                command.VolunteerId);
            
            return Error.Failure("volunteer.pet.photos.failure",
                "Error occured while uploading pet photos").ToErrorList();
        }
    }
    
    
}