namespace PetHelper.Application.DTOs.Pet;

public record UploadFileDto(
    string FileName,
    Stream Content);