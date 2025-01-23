using PetHelper.SharedKernel.ValueObjects;

namespace PetHelper.Core.DTOs.Pet;

public record UploadingFileDto(
    FilePath FilePath,
    Stream Content);