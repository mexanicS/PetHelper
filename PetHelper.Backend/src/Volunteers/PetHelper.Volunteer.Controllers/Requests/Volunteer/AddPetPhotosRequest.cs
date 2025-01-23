using Microsoft.AspNetCore.Http;
using PetHelper.Core.DTOs.Pet;
using PetHelper.Volunteer.Application.VolunteersManagement.Commands.Volunteer.AddPetPhotos;

namespace PetHelper.Volunteer.Controllers.Requests.Volunteer;

public record AddPetPhotosRequest(
    IFormFileCollection Files)
{
    public AddPetPhotosCommand ToCommand(Guid volunteerId, Guid petId, List<UploadFileDto> uploadFileDto) =>
        new(volunteerId, petId, uploadFileDto);
}