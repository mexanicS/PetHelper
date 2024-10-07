namespace PetHelper.Application.DTOs.Pet;

public record UploadFileDto(
    string FilePath,
    Stream Content);