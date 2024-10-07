using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PetHelper.API.Extensions;
using PetHelper.Application.Species;
using PetHelper.Application.Species.AddBreed;
using PetHelper.Application.Species.Create;

namespace PetHelper.API.Controllers;

public class SpeciesController : ApplicationController
{
    [HttpPost]
    public async Task<ActionResult> Create(
        [FromServices] CreateSpeciesHandler handler,
        [FromBody] CreateSpeciesRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await handler.Handle(request, cancellationToken);
            
        if(result.IsFailure)
            return result.Error.ToResponse();
            
        return Ok(result.Value);
    }
    
    [HttpPost("{speciesId:guid}/breed")]
    public async Task<ActionResult<Guid>> AddBreed(
        [FromRoute] Guid speciesId,
        [FromBody] AddBreedRequestDto dto,
        [FromServices] AddBreedHandler handler,
        [FromServices] IValidator<AddBreedRequest> validator,
        CancellationToken cancellationToken = default)
    {
        var request = new AddBreedRequest(speciesId, dto);
        
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.IsValid == false)
            return validationResult.ToValidationErrorResponse();
        
        var result = await handler.Handle(request, cancellationToken);
        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
}