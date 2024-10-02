using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PetHelper.API.Extensions;
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
            [FromBody] UpdateMainInfoDto dto,
            [FromServices] IValidator<UpdateMainInfoRequest> validator,
            CancellationToken cancellationToken = default)
        {
            var request = new UpdateMainInfoRequest(id, dto);
            
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
    }
}
