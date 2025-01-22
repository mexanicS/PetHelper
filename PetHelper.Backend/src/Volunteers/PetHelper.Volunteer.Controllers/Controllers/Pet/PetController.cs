using Microsoft.AspNetCore.Mvc;
using PetHelper.Framework;
using PetHelper.Volunteer.Application.VolunteersManagement.Queries.GetPets;
using PetHelper.Volunteer.Controllers.Requests.Pet;

namespace PetHelper.Volunteer.Controllers.Controllers.Pet;

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