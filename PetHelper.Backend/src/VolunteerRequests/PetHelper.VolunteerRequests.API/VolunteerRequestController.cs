using Microsoft.AspNetCore.Mvc;
using PetHelper.Framework;
using PetHelper.Framework.Authorization;
using PetHelper.VolunteerRequests.API.Requests;
using PetHelper.VolunteerRequests.Application.Features.Write.SetStatus.SetVolunteerRequestApproved;
using PetHelper.VolunteerRequests.Application.Features.Write.SetStatus.SetVolunteerRequestSubmitted;

namespace PetHelper.VolunteerRequests.API
{
    public class VolunteerRequestController : ApplicationController
    {
        [Permission("set.volunteer.request.submitted")]
        [HttpPost("{volunteerRequestId:guid}/volunteerRequest/{adminId:guid}/setSubmitted")]
        public async Task<ActionResult> SetVolunteerRequestSubmitted(
            [FromRoute] Guid volunteerRequestId, 
            [FromRoute] Guid adminId, 
            [FromServices] SetVolunteerRequestSubmittedHandler handler,
            CancellationToken cancellationToken = default)
        {
            var result = await handler
                .Handle(new SetVolunteerRequestSubmittedRequest(volunteerRequestId, adminId), cancellationToken);
            
            if(result.IsFailure)
                return result.Error.ToResponse();
            
            return Ok(result);
        }
        
        [Permission("set.volunteer.request.approved")]
        [HttpPost("{volunteerRequestId:guid}/volunteerRequest/{adminId:guid}/setApproved")]
        public async Task<ActionResult> SetVolunteerRequestApproved(
            [FromRoute] Guid volunteerRequestId, 
            [FromRoute] Guid adminId, 
            [FromServices] SetVolunteerRequestApprovedHandler handler,
            CancellationToken cancellationToken = default)
        {
            var result = await handler
                .Handle(new SetVolunteerRequestApprovedRequest(volunteerRequestId, adminId), cancellationToken);
            
            if(result.IsFailure)
                return result.Error.ToResponse();
            
            return Ok(result);
        }
    }
}
