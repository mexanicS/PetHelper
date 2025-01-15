using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Moq;
using PetHelper.Application.DTOs.Pet;
using PetHelper.Application.FileProvider;
using PetHelper.Application.Species;
using PetHelper.Application.Volunteers;
using PetHelper.Application.Volunteers.AddPet;
using PetHelper.Application.Volunteers.AddPetPhotos;
using PetHelper.Domain.Models;
using PetHelper.Domain.Shared;
using PetHelper.Domain.ValueObjects;
using IFileProvider = PetHelper.Application.Providers.IFileProvider;

namespace PetHelper.UnitTests.PetHelher.Domain.UnitTest;

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
            .ReturnsAsync(Result.Success<IReadOnlyList<FilePath>, Error>(filePaths));
        
        _volunteerRepositoryMock.Setup(v => v.GetVolunteerById(volunteer.Id, cancellationToken))
            .ReturnsAsync(volunteer);
        
        
        /*var handler = new AddPetPhotoHandler(_volunteerRepositoryMock.Object, _fileProviderMock.Object,);
        //act
        var result = await handle.Handle(command,cancellationToken);
        */


        //assert
    }

}