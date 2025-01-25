using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetHelper.Framework;
using PetHelper.Volunteer.Application.VolunteersManagement.Commands.Pet.ChangeStatusPet;
using PetHelper.Volunteer.Application.VolunteersManagement.Commands.Pet.SetMainPhotoPet;
using PetHelper.Volunteer.Application.VolunteersManagement.Commands.Pet.SoftDeletePet;
using PetHelper.Volunteer.Application.VolunteersManagement.Commands.Pet.UpdatePet;
using PetHelper.Volunteer.Application.VolunteersManagement.Commands.Volunteer.AddPet;
using PetHelper.Volunteer.Application.VolunteersManagement.Commands.Volunteer.AddPetPhotos;
using PetHelper.Volunteer.Application.VolunteersManagement.Commands.Volunteer.Create;
using PetHelper.Volunteer.Application.VolunteersManagement.Commands.Volunteer.Delete;
using PetHelper.Volunteer.Application.VolunteersManagement.Commands.Volunteer.HardDeletePet;
using PetHelper.Volunteer.Application.VolunteersManagement.Commands.Volunteer.UpdateDetailsForAssistance;
using PetHelper.Volunteer.Application.VolunteersManagement.Commands.Volunteer.UpdateMainInfo;
using PetHelper.Volunteer.Application.VolunteersManagement.Commands.Volunteer.UpdateSocialNetworkList;
using PetHelper.Volunteer.Application.VolunteersManagement.Queries.GetVolunteers;
using PetHelper.Volunteer.Controllers.Processors;
using PetHelper.Volunteer.Controllers.Requests.Volunteer;

namespace PetHelper.Volunteer.Controllers.Controllers.Volunteer
{
    public class VolunteerController : ApplicationController
    {
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Create(
            [FromServices] CreateVolunteerHandler handler,
            [FromBody] CreateVolunteerRequest request,
            CancellationToken cancellationToken = default)
        {
            var result = await handler.Handle(request.ToCommand(), cancellationToken);
            
            if(result.IsFailure)
                return BadRequest(result.Error.ToResponse());
            
            return Ok(result.Value);
        }
        
        [HttpPut("{volunteerId:guid}/main-info")]
        public async Task<ActionResult> Update(
            [FromRoute] Guid volunteerId,
            [FromServices] UpdateMainInfoHandler handler,
            [FromBody] UpdateMainInfoRequest request,
            CancellationToken cancellationToken = default)
        {
            var command = request.ToCommand(volunteerId);
            
            var result = await handler.Handle(command, cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();
            
            return Ok(result.Value);
        }
        
        [HttpPut("{volunteerId:guid}/social-network")]
        public async Task<ActionResult> UpdateSocialNetworkList(
            [FromRoute] Guid volunteerId,
            [FromServices] UpdateSocialNetworkListHandler handler,
            [FromBody] UpdateSocialNetworkListRequest request,
            CancellationToken cancellationToken = default)
        {
            var command = request.ToCommand(volunteerId);
            
            var result = await handler.Handle(command, cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();
            
            return Ok(result.Value);
        }
        
        [HttpPut("{volunteerId:guid}/details-for-assistance")]
        public async Task<ActionResult> UpdateDetailsForAssistanceList(
            [FromRoute] Guid volunteerId,
            [FromServices] UpdateDetailsForAssistanceHandler handler,
            [FromBody] UpdateDetailsForAssistanceRequest request,
            CancellationToken cancellationToken = default)
        {
            var command = request.ToCommand(volunteerId);
            
            var result = await handler.Handle(command, cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();
            
            return Ok(result.Value);
        }
        
        [HttpDelete("{volunteerId:guid}/volunteer")]
        public async Task<ActionResult> Delete(
            [FromRoute] Guid volunteerId,
            [FromServices] DeleteVolunteerHandler handler,
            CancellationToken cancellationToken = default)
        {
            var command = new DeleteVolunteerCommand(volunteerId);
            
            var result = await handler.Handle(command, cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();
            
            return Ok(result.Value);
        }

        [HttpPost("{volunteerId:guid}/pet")]
        public async Task<ActionResult> AddPet(
            [FromRoute] Guid volunteerId, 
            [FromBody] AddPetRequest request,
            [FromServices] AddPetHandler handler,
            CancellationToken cancellationToken = default)
        {
            var command = request.ToCommand(volunteerId);
            
            var result = await handler.Handle(command, cancellationToken);
            
            if(result.IsFailure)
                return result.Error.ToResponse();
            
            return Ok(result.Value);
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

            var command = request.ToCommand(volunteerId, petId, fileList);

            var handleResult = await handler
                .Handle(command, cancellationToken);
            
            if(handleResult.IsFailure)
                return handleResult.Error.ToResponse();

            return Ok();
        }
        
        [HttpGet]
        public async Task<ActionResult> Get(
            [FromQuery] GetVolunteersWithPaginationRequest request,
            [FromServices] GetVolunteersWithPaginationHandler withPaginationHandler, 
            CancellationToken cancellationToken = default)
        {
            var query = request.ToQuery();
        
            var response = await withPaginationHandler.Handle(query, cancellationToken);
        
            return Ok(response);
        }
        
        [HttpPost("{volunteerId:guid}/pets/{petId:guid}/update")]
        public async Task<ActionResult> UpdatePet(
            [FromRoute] Guid volunteerId, 
            [FromRoute] Guid petId, 
            [FromBody] UpdatePetRequest request,
            [FromServices] UpdatePetHandler handler,
            CancellationToken cancellationToken = default)
        {
            var command = request.ToCommand(volunteerId, petId);
            
            var result = await handler.Handle(command, cancellationToken);
            
            if(result.IsFailure)
                return result.Error.ToResponse();
            
            return Ok(result.Value);
        }
        
        [HttpPost("{volunteerId:guid}/pets/{petId:guid}/change-status")]
        public async Task<ActionResult> ChangeStatusPet(
            [FromRoute] Guid volunteerId,
            [FromRoute] Guid petId, 
            [FromBody] ChangeStatusPetRequest request,
            [FromServices] ChangeStatusPetHandler handler,
            CancellationToken cancellationToken = default)
        {
            var command = request.ToCommand(volunteerId, petId);
            
            var result = await handler.Handle(command, cancellationToken);
            
            if(result.IsFailure)
                return result.Error.ToResponse();
            
            return Ok(result.Value);
        }
        
        [HttpDelete("{volunteerId:guid}/pets/{petId:guid}/soft-delete")]
        public async Task<ActionResult> SoftDeletePet(
            [FromRoute] Guid volunteerId,
            [FromRoute] Guid petId,
            [FromServices] SoftDeletePetHandler handler,
            CancellationToken cancellationToken = default)
        {
            var command = new SoftDeletePetCommand(volunteerId, petId);
            
            var result = await handler.Handle(command, cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();
            
            return Ok(result.Value);
        }
        
        [HttpDelete("{volunteerId:guid}/pets/{petId:guid}/hard-delete")]
        public async Task<ActionResult> HardDeletePet(
            [FromRoute] Guid volunteerId,
            [FromRoute] Guid petId,
            [FromServices] HardDeletePetHandler handler,
            CancellationToken cancellationToken = default)
        {
            var command = new HardDeletePetCommand(volunteerId, petId);
            
            var result = await handler.Handle(command, cancellationToken);

            if (result.IsFailure)
                return result.Error.ToResponse();
            
            return Ok(result.Value);
        }
        
        [HttpPost("{volunteerId:guid}/pets/{petId:guid}/set-main-photo")]
        public async Task<ActionResult> SetMainPhoto(
            [FromRoute] Guid volunteerId, 
            [FromRoute] Guid petId, 
            [FromQuery] string photoPath,
            [FromServices] SetMainPhotoPetHandler handler,
            CancellationToken cancellationToken = default)
        {
            var command = new SetMainPhotoPetCommand(volunteerId, petId, photoPath);
            
            var result = await handler.Handle(command, cancellationToken);
            
            if(result.IsFailure)
                return result.Error.ToResponse();
            
            return Ok(result.Value);
        }
    }
}
