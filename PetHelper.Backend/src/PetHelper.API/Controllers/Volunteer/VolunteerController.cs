using Microsoft.AspNetCore.Mvc;
using PetHelper.API.Controllers.Volunteer.Requests;
using PetHelper.API.Extensions;
using PetHelper.API.Processors;
using PetHelper.API.Response;
using PetHelper.Application.Volunteers.Commands.AddPet;
using PetHelper.Application.Volunteers.Commands.AddPetPhotos;
using PetHelper.Application.Volunteers.Commands.Create;
using PetHelper.Application.Volunteers.Commands.Delete;
using PetHelper.Application.Volunteers.Commands.UpdateDetailsForAssistance;
using PetHelper.Application.Volunteers.Commands.UpdateMainInfo;
using PetHelper.Application.Volunteers.Commands.UpdateSocialNetworkList;
using PetHelper.Application.Volunteers.Queries.GetVolunteers;

namespace PetHelper.API.Controllers.Volunteer
{
    public class VolunteerController : ApplicationController
    {
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
            
            return Ok(Envelope.Ok(result.Value));
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
    }
}
