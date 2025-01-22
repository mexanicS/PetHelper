namespace PetHelper.Core.DTOs.Pet;

public record UploadFileDto(
    string FileName,
    Stream Content);