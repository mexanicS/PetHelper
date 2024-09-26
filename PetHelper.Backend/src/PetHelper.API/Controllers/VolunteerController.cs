using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PetHelper.API.Extensions;
using PetHelper.Application.Volunteers.CreateVolunteers;
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
    }
}
