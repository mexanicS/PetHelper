using PetHelper.Domain.ValueObjects;

namespace PetHelper.Application.DTOs.Pet;

public record UploadingFileDto(
    FilePath FilePath,
    Stream Content);