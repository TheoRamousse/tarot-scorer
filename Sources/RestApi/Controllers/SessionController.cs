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
        public async Task<IActionResult> Get(int count, int index = 0)
        {
            _logger.LogInformation("RestApi: Call GetAll() Session(s)", "Started", DateTime.UtcNow);
            var entity = await _manager.GetSessions(index, count);
            var dto = SessionFactory.ToDTO(entity);
            _logger.LogInformation("RestApi: Ended GetAll() method in Session(s)", "Ended", DateTime.UtcNow);
            return Ok(dto);
        }



        [HttpGet("count")]
        [ProducesResponseType(200, Type = typeof(int))]
        public async Task<IActionResult> Count()
        {
            _logger.LogInformation("RestApi: Call Count() Session(s)", "Started", DateTime.UtcNow);
            var res = await _manager.GetNbSessions();
            _logger.LogInformation("RestApi: Ended Count() method in Session(s)", "Ended", DateTime.UtcNow);
            return Ok(res);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(SessionDTO))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Insert(SessionDTO dto)
        {
            _logger.LogInformation("RestApi: Call Insert() Session", "Started");
            if (dto == null)
            {
                _logger.LogError("Error invalid post request");
                return BadRequest();
            }

            var session = SessionFactory.ToModel(dto);
            bool response = await _manager.AddSession(session);
            _logger.LogInformation("RestApi: Call Insert() Session", "Ended");
            if (!response)
                return UnprocessableEntity();
            return CreatedAtAction(nameof(Get), new { id = dto.Id }, dto);

        }


        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(SessionDTO))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Update(int id, [FromBody] SessionDTO dto)
        {
            _logger.LogInformation("RestApi: Call Update() Session", "Started");

            if (dto == null)
            {
                _logger.LogError("Error invalid put request");
                return BadRequest();
            }

            dto.Id = id;

            var session = SessionFactory.ToModel(dto);
            bool response = await _manager.UpdateSession(session.Id, session);
            _logger.LogInformation("RestApi: Call Update() Session", "Ended");
            if (!response)
                return UnprocessableEntity();
            return Ok(dto);

        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Delete(long id)
        {
            _logger.LogInformation("RestApi: Call Delete() Session", "Started");

            bool response = await _manager.DeleteSession(id);

            _logger.LogInformation("RestApi: Call Delete() Session", "Ended");
            if (!response)
                return UnprocessableEntity();
            return Ok(true);
        }
    }
}
