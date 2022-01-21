using Microsoft.AspNetCore.Mvc;
using NLog;
using System.Collections.Generic;
using TarotDB.
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
        public IActionResult GetAll()
        {
            return Ok(manager.);
        }
    }
}
