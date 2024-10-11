using PetHelper.Application.DTOs.Pet;

namespace PetHelper.Application.Volunteers.AddPetPhotos;

public record AddPetPhotosCommand(
    Guid VolunteerId,
    Guid PetId,
    IEnumerable<UploadFileDto> Files);