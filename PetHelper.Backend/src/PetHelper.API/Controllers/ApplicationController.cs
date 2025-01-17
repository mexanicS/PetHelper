using Microsoft.AspNetCore.Mvc;
using PetHelper.API.Response;
using PetHelper.Domain.Models;

namespace PetHelper.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApplicationController : ControllerBase
    {
        public override OkObjectResult Ok(object? value)
        {
            var envelope = Envelope.Ok(value);
            
            return base.Ok(envelope);
        }
    }
}
