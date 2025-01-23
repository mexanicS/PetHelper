using Microsoft.AspNetCore.Mvc;
using PetHelper.Core.Models;

namespace PetHelper.Framework
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
