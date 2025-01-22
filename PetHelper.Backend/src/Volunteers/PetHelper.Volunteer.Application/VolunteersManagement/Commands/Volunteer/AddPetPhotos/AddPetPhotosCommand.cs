using PetHelper.Core.Abstractions.Commands;
using PetHelper.Core.DTOs.Pet;

namespace PetHelper.Volunteer.Application.VolunteersManagement.Commands.Volunteer.AddPetPhotos;

public record AddPetPhotosCommand(
    Guid VolunteerId,
    Guid PetId,
    IEnumerable<UploadFileDto> Files) : ICommand;