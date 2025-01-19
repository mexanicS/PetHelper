using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PetHelper.API.Controllers.Species.Requests;
using PetHelper.API.Extensions;
using PetHelper.Application.Species.Command.AddBreed;
using PetHelper.Application.Species.Command.Create;
using PetHelper.Application.Species.Command.Delete;
using PetHelper.Application.Species.Command.DeleteBreed;
using PetHelper.Application.Species.Queries.GetSpecieses;

namespace PetHelper.API.Controllers.Species;

public class SpeciesController : ApplicationController
{
    [HttpPost]
    public async Task<ActionResult> Create(
        [FromServices] CreateSpeciesHandler handler,
        [FromBody] CreateSpeciesRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await handler.Handle(request.ToCommand(), cancellationToken);
            
        if(result.IsFailure)
            return result.Error.ToResponse();
            
        return Ok(result.Value);
    }
    
    [HttpPost("{speciesId:guid}/breed")]
    public async Task<ActionResult<Guid>> AddBreed(
        [FromRoute] Guid speciesId,
        [FromBody] AddBreedRequest request,
        [FromServices] AddBreedHandler handler,
        CancellationToken cancellationToken = default)
    {
        var command = request.ToCommand(speciesId);

        var result = await handler.Handle(command, cancellationToken);
        
        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }

    [HttpDelete("{speciesId:guid}")]
    public async Task<ActionResult> Delete(
        [FromRoute] Guid speciesId,
        [FromServices] DeleteSpeciesHandler handler,
        CancellationToken cancellationToken = default)
    {
        var command = new DeleteSpeciesCommand(speciesId);
        
        var result = await handler.Handle(command, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }

    [HttpDelete("{speciesId:guid}/breed")]
    public async Task<ActionResult> DeleteBreed(
        [FromRoute] Guid speciesId, 
        [FromBody] DeleteBreedRequest request,
        [FromServices] DeleteBreedHandler handler,
        CancellationToken cancellationToken = default)
    {
        var command = request.ToCommand(speciesId);
        var result = await handler.Handle(command, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
    
    [HttpGet]
    public async Task<ActionResult> Get(
        [FromQuery] GetSpeciesRequest request,
        [FromServices] GetSpeciesesHandler handler, 
        CancellationToken cancellationToken = default)
    {
        var response = await handler.Handle(request.ToQuery(), cancellationToken);
        
        return Ok(response);
    }
}