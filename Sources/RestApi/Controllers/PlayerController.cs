using Microsoft.AspNetCore.Mvc;
using NLog;
using System.Collections.Generic;
using System.Threading.Tasks;
using TarotDB2Model;

namespace RestApi.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/")]
    [ApiVersion("1.0")]
    public class PlayerController
    {
        private readonly TarotDBManager manager = null;
        private readonly ILogger _logger = null;
        public PlayerController(ILogger logger)
        {
            _logger = logger;
        }


        [HttpGet]
        public Task<IActionResult> GetAll()
        {
            //return Ok(manager.);
            return null; // A MODIFIER BIEN ENTENDU
        }
    }
}
