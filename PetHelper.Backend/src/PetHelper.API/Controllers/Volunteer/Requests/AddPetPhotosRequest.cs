namespace PetHelper.API.Contracts;

public record AddPetPhotosRequest(
    IFormFileCollection Files);