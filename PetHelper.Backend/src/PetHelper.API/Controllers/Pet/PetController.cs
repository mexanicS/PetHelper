using Microsoft.AspNetCore.Mvc;
using PetHelper.API.Controllers.Pet.Requests;
using PetHelper.Application.Volunteers.Queries.GetPets;

namespace PetHelper.API.Controllers.Pet;

public class PetController : ApplicationController
{
    [HttpGet]
    public async Task<ActionResult> Get(
        [FromQuery] GetPetWithPaginationRequest request,
        [FromServices] GetPetsWithPaginationHandler handler, 
        CancellationToken cancellationToken = default)
    {
        var query = request.ToQuery();
        
        var response = await handler.Handle(query, cancellationToken);
        
        return Ok(response);
    }

}