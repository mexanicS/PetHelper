using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PetHelper.API.Contracts;
using PetHelper.API.Controllers.Volunteer.Requests;
using PetHelper.API.Extensions;
using PetHelper.API.Processors;
using PetHelper.Application.Volunteers.AddPet;
using PetHelper.Application.Volunteers.AddPetPhotos;
using PetHelper.Application.Volunteers.CreateVolunteers;
using PetHelper.Application.Volunteers.DeleteVolunteer;
using PetHelper.Application.Volunteers.UpdateDetailsForAssistance;
using PetHelper.Application.Volunteers.UpdateMainInfo;
using PetHelper.Application.Volunteers.UpdateSocialNetworkList;
using PetHelper.Domain.Shared;

namespace PetHelper.API.Controllers
{
    public class VolunteerController : ApplicationController
    {
        [HttpPost]
        public async Task<ActionResult> Create(
            [FromServices] CreateVolunteerHandler handler,
            [FromBody] CreateVolunteerRequest request,
            CancellationToken cancellationToken = default)
        {
            var result = await handler.Handle(request, cancellationToken);
            
            if(result.IsFailure)
                return BadRequest(result.Error.ToResponse());
            
            return Ok(Envelope.Ok(result.Value));
        }
        
        [HttpPut("{id:guid}/main-info")]
        public async Task<ActionResult> Update(
            [FromRoute] Guid id,
            [FromServices] UpdateMainInfoHandler handler,
            [FromBody] UpdateMainInfoRequest request,
            [FromServices] IValidator<UpdateMainInfoRequest> validator,
            CancellationToken cancellationToken = default)
        {
            
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (validationResult.IsValid == false)
            {
                return validationResult.ToValidationErrorResponse();
            }

            var command = new UpdateMainInfoCommand(
                id,
                request.Email,
                request.Description,
                request.ExperienceInYears,
                request.PhoneNumber,
                request.FullName
                );
            
            var result = await handler.Handle(command, cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();
            
            return Ok(Envelope.Ok(result.Value));
        }
        
        [HttpPut("{id:guid}/social-network")]
        public async Task<ActionResult> UpdateSocialNetworkList(
            [FromRoute] Guid id,
            [FromServices] UpdateSocialNetworkListHandler handler,
            [FromBody] UpdateSocialNetworkListRequestDto dto,
            [FromServices] IValidator<UpdateSocialNetworkListRequest> validator,
            CancellationToken cancellationToken = default)
        {
            var request = new UpdateSocialNetworkListRequest(id, dto);
            
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (validationResult.IsValid == false)
            {
                return validationResult.ToValidationErrorResponse();
            }
            
            var result = await handler.Handle(request, cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();
            
            return Ok(Envelope.Ok(result.Value));
        }
        
        [HttpPut("{id:guid}/details-for-assistance")]
        public async Task<ActionResult> UpdateDetailsForAssistanceList(
            [FromRoute] Guid id,
            [FromServices] UpdateDetailsForAssistanceHandler handler,
            [FromBody] UpdateDetailsForAssistanceRequestDto dto,
            [FromServices] IValidator<UpdateDetailsForAssistanceRequest> validator,
            CancellationToken cancellationToken = default)
        {
            var request = new UpdateDetailsForAssistanceRequest(id, dto);
            
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (validationResult.IsValid == false)
            {
                return validationResult.ToValidationErrorResponse();
            }
            
            var result = await handler.Handle(request, cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();
            
            return Ok(Envelope.Ok(result.Value));
        }
        
        [HttpDelete("{id:guid}/volunteer")]
        public async Task<ActionResult> Delete(
            [FromRoute] Guid id,
            [FromServices] DeleteVolunteerHandler handler,
            [FromServices] IValidator<DeleteVolunteerRequest> validator,
            CancellationToken cancellationToken = default)
        {
            var request = new DeleteVolunteerRequest(id);
            
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (validationResult.IsValid == false)
            {
                return validationResult.ToValidationErrorResponse();
            }
            
            var result = await handler.Handle(request, cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();
            
            return Ok(Envelope.Ok(result.Value));
        }

        [HttpPost("{id:guid}/pet")]
        public async Task<ActionResult> AddPet(
            [FromRoute] Guid id, 
            [FromBody] AddPetRequest request,
            [FromServices] AddPetHandler handler,
            CancellationToken cancellationToken = default)
        {
            var command = new AddPetCommand(
                id,
                request.SpeciesId,
                request.BreedId,
                request.Name,
                request.TypePet,
                request.Description,
                request.Color,
                request.HealthInformation,
                request.Weight,
                request.Height,
                request.PhoneNumber,
                request.IsNeutered,
                request.BirthDate,
                request.IsVaccinated,
                request.City,
                request.Street,
                request.HouseNumber,
                request.ZipCode,
                request.DetailsForAssistances
            );
            
            var result = await handler.Handle(command, cancellationToken);
            
            if(result.IsFailure)
                return result.Error.ToResponse();
            
            return Ok(Envelope.Ok(result.Value));
        }

        [HttpPost("{volunteerId:guid}/pets/{petId:guid}/photos")]
        public async Task<IActionResult> AddPetPhoto(
            [FromRoute] Guid volunteerId,
            [FromRoute] Guid petId,
            [FromForm] AddPetPhotosRequest request,
            [FromServices] AddPetPhotoHandler handler,
            CancellationToken cancellationToken = default)
        {
            await using var fileProcessor = new FormFileProcessor();

            var fileList = fileProcessor.Process(request.Files);

            var command = new AddPetPhotosCommand(
                volunteerId,
                petId,
                fileList);

            var handleResult = await handler
                .Handle(command, cancellationToken);
            
            if(handleResult.IsFailure)
                return handleResult.Error.ToResponse();

            return Ok();
        }
    }
}
