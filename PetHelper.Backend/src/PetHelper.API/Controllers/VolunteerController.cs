using Microsoft.AspNetCore.Mvc;
using PetHelper.Application.Volunteers.CreateVolunteers;

namespace PetHelper.API.Controllers
{
    public class VolunteerController : ApplicationController
    {
        public async Task<ActionResult<Guid>> Create(
            [FromServices] CreateVolunteerHandler handler,
            [FromBody] CreateVolunteerRequest request,
            CancellationToken cancellationToken = default)
        {
            var result = await handler.Handle(request, cancellationToken);
            
            if(result.IsSuccess)
                return BadRequest(result.Error);
            
            return Ok(result.Value);
        }
    }
}
