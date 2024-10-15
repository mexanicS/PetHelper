using FluentAssertions;
using PetHelper.Domain.Models;
using PetHelper.Domain.ValueObjects;
using PetHelper.Domain.ValueObjects.Pet;

namespace PetHelper.UnitTests.PetHelher.Domain.UnitTest;

public class VolunteerTests
{
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
}
