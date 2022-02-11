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
            _logger.LogInformation("RestApi: Call GetAll() Player(s) ", "Started", DateTime.UtcNow);
            var entity = await _manager.GetPlayers(index, count);
            /*
            ** Model to DTO with Mapper 
            ** var dto = _mapper.Map<IEnumerable<PlayerDTO>>(entity);
            **/

            /*
            ** Model to DTO with Factory
            */
            var dto = PlayerFactory.ToDTO(entity);
            _logger.LogInformation("RestApi: Ended GetAll() method in Player(s) ", "Ended", DateTime.UtcNow);
            return Ok(dto);
        }



        [HttpGet("count")]
        [ProducesResponseType(200, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Count()
        {
            _logger.LogInformation("RestApi: Call Count() Player(s) ", "Started", DateTime.UtcNow);
            var res = await _manager.GetNbPlayers();
            _logger.LogInformation("RestApi: Ended Count() method in Player(s) ", "Ended", DateTime.UtcNow);
            return Ok(res);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(PlayerDTO))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Insert(PlayerDTO dto)
        {
            _logger.LogInformation("RestApi: Call Insert() Player", "Started");
            if (dto == null)
            {
                _logger.LogError("Error invalid post request");
                return BadRequest();
            }

            var player = PlayerFactory.ToModel(dto);
            bool response = await _manager.AddPlayer(player);
            _logger.LogInformation("RestApi: Call Insert() Player", "Ended");
            if (!response)
                return UnprocessableEntity();
            return CreatedAtAction(nameof(Get), new { id = dto.Id }, dto);

        }


        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(PlayerDTO))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Update(int id, [FromBody] PlayerDTO dto)
        {
            _logger.LogInformation("RestApi: Call Update() Player", "Started");

            if (dto == null)
            {
                _logger.LogError("Error invalid put request");
                return BadRequest();
            }

            dto.Id = id;

            var player = PlayerFactory.ToModel(dto);
            bool response = await _manager.UpdatePlayer(player.Id, player);
            _logger.LogInformation("RestApi: Call Update() Player", "Ended");
            if (!response)
                return UnprocessableEntity();
            return Ok(dto);

        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Delete(long id)
        {
            _logger.LogInformation("RestApi: Call Delete() Player", "Started");

            bool response = await _manager.DeletePlayer(id);

            _logger.LogInformation("RestApi: Call Delete() Player", "Ended");
            if (!response)
                return UnprocessableEntity();
            return Ok(true);
        }
    }
}
