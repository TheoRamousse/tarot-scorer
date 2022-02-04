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
    [Route("api/v{version:apiVersion}/Game")]
    [ApiVersion("1.0")]
    public class GameController : ControllerBase
    {
        private readonly IMapper _mapper = null;
        private readonly IDataManager _manager = null;
        private readonly ILogger<GameController> _logger = null;

        public GameController(ILogger<GameController> logger, IMapper mapper, IDataManager manager)
        {
            _mapper = mapper;
            _logger = logger;
            _manager = manager;
        }



        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<GameDTO>))]
        public async Task<IActionResult> Get(int count, int index = 0)
        {
            _logger.LogInformation("RestApi: Call GetAll() Game(s) at {dateTime}", "Started");
            var entity = await _manager.GetGames(index, count);
            //Model to DTO with Mapper 
            //var dto = _mapper.Map<IEnumerable<GameDTO>>(entity);

            //Model to DTO with Factory
            var dto = GameFactory.ToDTO(entity);
            _logger.LogInformation("RestApi: Call GetAll() Game(s) at {dateTime}", "Ended");
            return Ok(dto);
        }



        [HttpGet("count")]
        [ProducesResponseType(200, Type = typeof(int))]
        public async Task<IActionResult> Count()
        {
            _logger.LogInformation("RestApi: Call Count() Game(s) at {dateTime}", "Started");
            var res = await _manager.GetNbGames();
            _logger.LogInformation("RestApi: Call Count() Game(s) at {dateTime}", "Ended");
            return Ok(res);
        }


    }
}
