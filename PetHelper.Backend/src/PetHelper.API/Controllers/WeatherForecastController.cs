using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using PetHelper.Domain.Models;

namespace PetHelper.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        [HttpGet(Name = "Get")]
        public IActionResult Get(string name, string typePet, string description)
        {
            Result<Pet> petResult = Pet.Create(name, typePet, description);

            if (petResult.IsFailure)
            {
                return BadRequest(petResult.Error);
            }

            Save(petResult.Value);
            return Ok();
        }

        public void Save(Pet pet) { }
    }
}
