using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Microsoft.Extensions.Logging;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TarotDB;
using TarotDB2Model;
using TarotDTO;

namespace RestApi.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/Session")]
    [ApiVersion("1.0")]
    public class SessionController : ControllerBase
    {
        private readonly IMapper _mapper = null;
        private readonly IDataManager _manager = null;
        private readonly ILogger<SessionController> _logger = null;
        public SessionController(ILogger<SessionController> logger, IMapper mapper, IDataManager manager)
        {
            _mapper = mapper;
            _logger = logger;
            _manager = manager;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<SessionDTO>))]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("RestApi: Call GetAll() Session(s) at {dateTime}", "Started", DateTime.UtcNow);
            var count = await _manager.GetNbSessions();
            var entity = await _manager.GetSessions(0, count);
            var dto = SessionFactory.ToDTO(entity);
            _logger.LogInformation("RestApi: Ended GetAll() method in Session(s) at {dateTime}", "Ended", DateTime.UtcNow);
            return Ok(dto);
        }



        [HttpGet("count")]
        [ProducesResponseType(200, Type = typeof(int))]
        public async Task<IActionResult> Count()
        {
            _logger.LogInformation("RestApi: Call Count() Session(s) at {dateTime}", "Started", DateTime.UtcNow);
            var res = await _manager.GetNbSessions();
            _logger.LogInformation("RestApi: Ended Count() method in Session(s) at {dateTime}", "Ended", DateTime.UtcNow);
            return Ok(res);
        }
    }
}
