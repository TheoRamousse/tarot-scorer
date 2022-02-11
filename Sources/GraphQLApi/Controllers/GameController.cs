using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TarotDTO;

namespace GraphQLApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {

        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IDataManager _dataManager;

        public GameController(ILogger logger, IMapper mapper, IDataManager dataManager)
        {
            _logger.LogDebug("Call of GameController constructor");
            _logger = logger;
            _dataManager = dataManager;
            _mapper = mapper;
            _logger.LogDebug("End of GameController constructor");
        }

        [HttpGet]
        public async Task<IEnumerable<GameDTO>> Get()
        {
            _logger.LogDebug("Call of get method to get Games");
            List<GameDTO> result = new List<GameDTO>();
            Game g = await _dataManager.GetGameById(1);
            result.Add(_mapper.Map<GameDTO>(g));
            _logger.LogInformation("End of get method to get Games");
            return result;
        }
    }
}
