using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using TarotDB;
using TarotDB2Model;
using System;
using Shared;
using TarotDTO;

namespace RestApi.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/Player")]
    [ApiVersion("1.0")]
    public class PlayerController : ControllerBase
    {
        private readonly IMapper _mapper = null;
        private readonly IDataManager _manager = null;
        private readonly ILogger<PlayerController> _logger = null;
        public PlayerController(ILogger<PlayerController> logger, IMapper mapper, IDataManager manager)
        {
            _mapper = mapper;
            _logger = logger;
            _manager = manager;
        }


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PlayerDTO>))]
        public async Task<IActionResult> Get(int count, int index = 0)
        {
            _logger.LogInformation("RestApi: Call GetAll() Player(s) at {dateTime}", "Started", DateTime.UtcNow);
            var entity = await _manager.GetPlayers(index, count);
            //Model to DTO with Mapper 
            //var dto = _mapper.Map<IEnumerable<PlayerDTO>>(entity);

            //Model to DTO with Factory
            var dto = PlayerFactory.ToDTO(entity);
            _logger.LogInformation("RestApi: Ended GetAll() method in Player(s) at {dateTime}", "Ended", DateTime.UtcNow);
            return Ok(dto);
        }



        [HttpGet("count")]
        [ProducesResponseType(200, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Count()
        {
            _logger.LogInformation("RestApi: Call Count() Player(s) at {dateTime}", "Started", DateTime.UtcNow);
            var res = await _manager.GetNbPlayers();
            _logger.LogInformation("RestApi: Ended Count() method in Player(s) at {dateTime}", "Ended", DateTime.UtcNow);
            return Ok(res);
        }
    }
}
