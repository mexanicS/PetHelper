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
            [FromServices] IValidator<CreateVolunteerRequest> validator,
            [FromBody] CreateVolunteerRequest request,
            CancellationToken cancellationToken = default)
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if(validationResult.IsValid == false)
            {
                var validationErrors = validationResult.Errors;
                
                var errors = from validationError in validationErrors
                    let error = Error.Validation(validationError.ErrorCode, validationError.ErrorMessage)
                        select new ResponseError(error.Code, error.Message,validationError.PropertyName);
                
                var envelope = Envelope.Error(errors);
                return BadRequest(envelope);
            }

            var result = await handler.Handle(request, cancellationToken);
            
            if(result.IsFailure)
                return BadRequest(result.Error.ToResponse());
            
            return Ok(Envelope.Ok(result.Value));
        }
    }
}
