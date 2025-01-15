using CSharpFunctionalExtensions;
using FluentAssertions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Moq;
using PetHelper.Application.DTOs;
using PetHelper.Application.DTOs.Pet;
using PetHelper.Application.Providers;
using PetHelper.Domain.Models;
using PetHelper.Domain.ValueObjects;
using PetHelper.Domain.ValueObjects.Pet;
using PetHelper.Application.Volunteers;
using PetHelper.Application.Species;
using PetHelper.Application.Volunteers.AddPet;
using PetHelper.Application.Volunteers.AddPetPhotos;
using PetHelper.Domain.Models.Species;
using PetHelper.Domain.Shared;
using Breed = PetHelper.Domain.Models.Breed.Breed;
using PetPhoto = PetHelper.Domain.ValueObjects.Pet.PetPhoto;

namespace PetHelper.UnitTests.PetHelher.Domain.UnitTest;

public class VolunteerTests
{
    
    private readonly Mock<IFileProvider> _fileProviderMock = new();
    private readonly Mock<IVolunteersRepository> _volunteerRepositoryMock = new();
    private readonly Mock<ISpeciesRepository> _speciesRepositoryMock = new();
    private readonly Mock<IValidator<AddPetCommand>> _validatorMock = new();
    private readonly Mock<ILogger<AddPetHandler>> _loggerMock = new();
    
    [Fact]
    public async void AddPetPhotosService_Should_Add_Photos_To_Pet()
    {
        // arrange
        /*var cancellationToken = new CancellationTokenSource().Token;
        var volunteer = GenerateVolunteer();
        var pet = GeneratePet();
        
        volunteer.AddPet(pet);
        
        var stream = new MemoryStream();
        var fileName = "test.jpg";
        
        var command = new AddPetPhotosCommand(volunteer.Id, pet.Id.Value,
        [
            new UploadFileDto(fileName, stream),
            new UploadFileDto(fileName, stream),
            new UploadFileDto(fileName, stream)
        ]);
        
        FilePath[] filePaths =
        [
            FilePath.Create(Guid.NewGuid(), ".jpg").Value,
            FilePath.Create(Guid.NewGuid(), ".jpg").Value,
            FilePath.Create(Guid.NewGuid(), ".jpg").Value
        ];
        
        _fileProviderMock.Setup(f => f.UploadFiles(It.IsAny<List<UploadingFileDto>>(), "",cancellationToken))
            .ReturnsAsync(Result.Success<IReadOnlyList<FilePath>, Error>).Should<>(filePaths);
        
        _volunteerRepositoryMock.Setup(v => v.GetByIdAsync(volunteer.Id, cancellationToken))
            .ReturnsAsync(volunteer);
        _validatorMock.Setup(v => v.ValidateAsync(command, cancellationToken))
            .ReturnsAsync(new ValidationResult());
        _unitOfWorkMock.Setup(u => u.SaveChangesAsync(cancellationToken))
            .Returns(Task.CompletedTask);
        var service = new AddPetPhotosService(
            _fileProviderMock.Object,
            _volunteerRepositoryMock.Object,
            _validatorMock.Object,
            _loggerMock.Object,
            _unitOfWorkMock.Object);
        // act
        var result = await service.ExecuteAsync(command, cancellationToken);
        var firstPhoto = filePaths[0];
        var secondPhoto = filePaths[1];
        var thirdPhoto = filePaths[2];
        // assert
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
        pet.PetPhotos.Values.Count.Should().Be(filePaths.Length);
        pet.PetPhotos.Values[0].Path.Should().Be(firstPhoto);
        pet.PetPhotos.Values[1].Path.Should().Be(secondPhoto);
        pet.PetPhotos.Values[2].Path.Should().Be(thirdPhoto);*/
    }
    
    [Fact]
    public async void AddPet_Should_AddPet_When_ValidPetProvided()
    {
        // arrange
        var cancellationToken = new CancellationTokenSource().Token;
        var volunteer = GenerateVolunteer();
        var species = GenerateSpecies();
        var breed = GenerateBreed(species);
        var command = GenerateCommand(volunteer.Id, species.Id, breed.Id.Value);
        
        var volunteerId = VolunteerId.Create(command.VolunteerId);
        
        _volunteerRepositoryMock
            .Setup(v=>v.GetVolunteerById(volunteerId, cancellationToken))
            .ReturnsAsync(volunteer);
        
        var speciesId = SpeciesId.Create(command.SpeciesId);
        
        _speciesRepositoryMock
            .Setup(s => s.GetSpeciesById(speciesId, cancellationToken))
            .ReturnsAsync(species);
        
        var handler = new AddPetHandler(
            _volunteerRepositoryMock.Object,
            _speciesRepositoryMock.Object,
            _loggerMock.Object);
        
        // act
        var result = await handler.Handle(command, cancellationToken);
        var petResult = volunteer.GetPetById(PetId.Create(result.Value));

        // assert
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
        volunteer.Pets.Count.Should().NotBe(0);
        petResult.IsSuccess.Should().BeTrue();
        petResult.IsFailure.Should().BeFalse();
        petResult.Value.Id.Value.Should().Be(result.Value);
        petResult.Value.SpeciesBreed.SpeciesId.Should().Be(species.Id);
        petResult.Value.SpeciesBreed.BreedId.Should().Be(breed.Id.Value);
    }   
    
    
    [Fact]
    public void MovePet_Should_Move_Pet_To_Forward()
    {
        // arrange
        var volunteer = GenerateVolunteer();

        var pets = Enumerable.Range(1, 5).Select(_ =>
            GeneratePet()).ToList();

        foreach (var pet in pets)
            volunteer.AddPet(pet);

        var firstPet = pets[0];
        var secondPet = pets[1];
        var thirdPet = pets[2];
        var fourthPet = pets[3];
        var fifthPet = pets[4];

        var fourthPosition = Position.Create(4).Value;
        
        // act
        var result = volunteer.MovePet(secondPet, fourthPosition);

        // assert
        result.IsSuccess.Should().BeTrue();
        firstPet.Position.Value.Should().Be(1);
        secondPet.Position.Value.Should().Be(4);
        thirdPet.Position.Value.Should().Be(2);
        fourthPet.Position.Value.Should().Be(3);
        fifthPet.Position.Value.Should().Be(5);
    }
    
    [Fact]
    public void MovePet_Should_Move_Pet_To_Back()
    {
        // arrange
        var volunteer = GenerateVolunteer();

        var pets = Enumerable.Range(1, 5).Select(_ =>
            GeneratePet()).ToList();

        foreach (var pet in pets)
            volunteer.AddPet(pet);

        var firstPet = pets[0];
        var secondPet = pets[1];
        var thirdPet = pets[2];
        var fourthPet = pets[3];
        var fifthPet = pets[4];

        var secondPosition = Position.Create(2).Value;
        // act
        var result = volunteer.MovePet(fourthPet, secondPosition);

        // assert
        result.IsSuccess.Should().BeTrue();
        firstPet.Position.Value.Should().Be(1);
        secondPet.Position.Value.Should().Be(3);
        thirdPet.Position.Value.Should().Be(4);
        fourthPet.Position.Value.Should().Be(2);
        fifthPet.Position.Value.Should().Be(5);
    }
    
    [Fact]
    public void MovePet_Should_Move_Pet_To_First()
    {
        // arrange
        var volunteer = GenerateVolunteer();

        var pets = Enumerable.Range(1, 5).Select(_ =>
            GeneratePet()).ToList();

        foreach (var pet in pets)
            volunteer.AddPet(pet);

        var firstPet = pets[0];
        var secondPet = pets[1];
        var thirdPet = pets[2];
        var fourthPet = pets[3];
        var fifthPet = pets[4];

        var firstPosition = Position.Create(1).Value;
        // act
        var result = volunteer.MovePet(fourthPet, firstPosition);

        // assert
        result.IsSuccess.Should().BeTrue();
        firstPet.Position.Value.Should().Be(2);
        secondPet.Position.Value.Should().Be(3);
        thirdPet.Position.Value.Should().Be(4);
        fourthPet.Position.Value.Should().Be(1);
        fifthPet.Position.Value.Should().Be(5);
    }
    
    [Fact]
    public void MovePet_Should_Move_Pet_To_Last()
    {
        // arrange
        var volunteer = GenerateVolunteer();

        var pets = Enumerable.Range(1, 5).Select(_ =>
            GeneratePet()).ToList();

        foreach (var pet in pets)
            volunteer.AddPet(pet);

        var firstPet = pets[0];
        var secondPet = pets[1];
        var thirdPet = pets[2];
        var fourthPet = pets[3];
        var fifthPet = pets[4];

        var fivePosition = Position.Create(5).Value;

        // act
        var result = volunteer.MovePet(secondPet, fivePosition);

        // assert
        result.IsSuccess.Should().BeTrue();
        firstPet.Position.Value.Should().Be(1);
        secondPet.Position.Value.Should().Be(5);
        thirdPet.Position.Value.Should().Be(2);
        fourthPet.Position.Value.Should().Be(3);
        fifthPet.Position.Value.Should().Be(4);
    }

    private Volunteer GenerateVolunteer()
    {
        var fullName = FullName.Create("test", "test", "test").Value;
        var email = Email.Create("test@sail.ru").Value;
        var description = Description.Create("test").Value;
        var yearsExperience = ExperienceInYears.Create(1).Value;
        var phoneNumber = PhoneNumber.Create("+79222222222").Value;
        var volunteer = new Volunteer(
            VolunteerId.NewId(),
            fullName,
            email,
            description,
            yearsExperience,
            phoneNumber,
            new SocialNetworkList([]),
            new DetailsForAssistanceList([]));

        return volunteer;
    }

    private Pet GeneratePet()
    {
        var petId = PetId.NewId();
        var name = Name.Create("test").Value;
        var description = Description.Create("test").Value;
        var type = TypePet.Create("test").Value;
        var color = Color.Create("test").Value;
        var informationHealth = HealthInformation.Create("test").Value;
        var address = Address.Create("test", "test", "test", "test").Value;
        var weight = Weight.Create(1).Value;
        var height = Height.Create(1).Value;
        var phoneNumber = PhoneNumber.Create("+79222222222").Value;
        var dateOfBirth = DateOnly.FromDateTime(DateTime.UtcNow);
        var speciesBreed = SpeciesBreed.Create(SpeciesId.NewId(), BreedId.NewId().Value);

        var pet = new Pet(
            petId,
            name,
            type,
            description,
            color,
            informationHealth,
            weight,
            height,
            phoneNumber,
            false,
            dateOfBirth,
            false,
            DateTime.UtcNow,
            address,
            speciesBreed.Value,
            new PetDetails([]),
            new PetPhotoList([])
        );

        return pet;
    }
    private Species GenerateSpecies()
    {
        var id = SpeciesId.NewId();
        var name = Name.Create("Test").Value;
        
        return new Species(id, name);
    }
    
    private Breed GenerateBreed(Species species)
    {
        var id = BreedId.NewId();
        var name = Name.Create("Test").Value;
        var breed = new Breed(id, name);
        
        species.AddBreed(breed);
        
        return breed;
    }
    
    private AddPetCommand GenerateCommand(Guid volunteerId, Guid speciesId, Guid breedId)
    {
        var name = "Test";
        var description = "Test";
        var color = "Test";
        var informationHealth = "Test";
        var weight = 1d;
        var height = 2d;
        var phoneNumber = "+79333333333";
        var isNeutered = false;
        var dateOfBirth = DateOnly.FromDateTime(DateTime.UtcNow);
        var isVaccinated = true;
        var typePet = "Test";
        var city = "Test";
        var street = "Test";
        var houseNumber = "Test";
        var zipCode = "Test";
        var assistanceDto1 = new DetailsForAssistanceDto("Test", "Test");
        
        DetailsForAssistanceListDto detailsForAssistance = 
            new DetailsForAssistanceListDto(new List<DetailsForAssistanceDto>(){assistanceDto1});
        
        return new(volunteerId,
            speciesId,
            breedId,
            name,
            typePet,
            description,
            color,
            informationHealth,
            weight,
            height,
            phoneNumber,
            isNeutered,
            dateOfBirth,
            isVaccinated,
            city,
            street,
            houseNumber,
            zipCode,
            detailsForAssistance);
    }
}
