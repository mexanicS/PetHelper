using PetHelper.Application.DTOs.Pet;
using PetHelper.Application.Volunteers.Commands.AddPetPhotos;

namespace PetHelper.API.Controllers.Volunteer.Requests;

public record AddPetPhotosRequest(
    IFormFileCollection Files)
{
    public AddPetPhotosCommand ToCommand(Guid volunteerId, Guid petId, List<UploadFileDto> uploadFileDto) =>
        new(volunteerId, petId, uploadFileDto);
}