/*using System.Runtime.InteropServices.JavaScript;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Moq;
using PetHelper.Application.Species;
using PetHelper.Application.Volunteers;
using PetHelper.Application.Volunteers.Commands.AddPet;
using PetHelper.Application.Volunteers.Commands.AddPetPhotos;
using PetHelper.Core.DTOs.Pet;
using PetHelper.Core.FileProvider;
using PetHelper.Core.Shared;
using PetHelper.Core.ValueObjects;
using IFileProvider = PetHelper.Core.Providers.IFileProvider;

namespace PetHelper.UnitTests;

public class UploadPhotosToPetTests
{
    private readonly Mock<IFileProvider> _fileProviderMock = new();
    private readonly Mock<IVolunteersRepository> _volunteerRepositoryMock = new();
    private readonly Mock<ISpeciesRepository> _speciesRepositoryMock = new();
    private readonly Mock<IValidator<AddPetCommand>> _validatorMock = new();
    private readonly Mock<ILogger<AddPetHandler>> _loggerMock = new();
    [Fact]
    public void Handle_Should_Upload_Photos_To_Pet()
    {
        //arrange
        
        var cancellationToken = new CancellationTokenSource().Token;
        var volunteerTests = new VolunteerTests();
        var volunteer = volunteerTests.GenerateVolunteer();
        var pet = volunteerTests.GeneratePet();
        
        volunteer.AddPet(pet);
        
        var stream = new MemoryStream();
        
        var uploadFileDto = new UploadFileDto("test", stream);
        List<UploadFileDto> uploadFiles = [uploadFileDto, uploadFileDto, uploadFileDto];
            
        var command = new AddPetPhotosCommand(volunteer.Id.Value, pet.Id.Value, uploadFiles);

        FilePath[] filePaths =
        [
            FilePath.Create(Guid.NewGuid(), ".jpg").Value,
            FilePath.Create(Guid.NewGuid(), ".jpg").Value,
            FilePath.Create(Guid.NewGuid(), ".jpg").Value
        ]; 
        
        _fileProviderMock
            .Setup(f => f.UploadFiles(It.IsAny<List<FileData>>(),
            "testBucketName", 
            cancellationToken))
            .ReturnsAsync(Result.Success<IReadOnlyList<FilePath>, JSType.Error>(filePaths));
        
        _volunteerRepositoryMock.Setup(v => v.GetVolunteerById(volunteer.Id, cancellationToken))
            .ReturnsAsync(volunteer);
        
        
        /*var handler = new AddPetPhotoHandler(_volunteerRepositoryMock.Object, _fileProviderMock.Object,);
        //act
        var result = await handle.Handle(command,cancellationToken);
        #1#


        //assert
    }

}*/