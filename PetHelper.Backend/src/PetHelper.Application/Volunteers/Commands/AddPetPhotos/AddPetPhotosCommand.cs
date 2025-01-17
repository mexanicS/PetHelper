using PetHelper.Application.Abstractions;
using PetHelper.Application.Abstractions.Commands;
using PetHelper.Application.DTOs.Pet;

namespace PetHelper.Application.Volunteers.Commands.AddPetPhotos;

public record AddPetPhotosCommand(
    Guid VolunteerId,
    Guid PetId,
    IEnumerable<UploadFileDto> Files) : ICommand;