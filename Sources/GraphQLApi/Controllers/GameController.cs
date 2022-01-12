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

        private readonly ILogger<GameController> _logger;
        private readonly IMapper _mapper;
        private readonly IDataManager _dataManager;

        public GameController(ILogger<GameController> logger, IMapper mapper, IDataManager dataManager)
        {
            _logger = logger;
            _dataManager = dataManager;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<GameDTO>> Get()
        {
            List<GameDTO> result = new List<GameDTO>();
            Game g = await _dataManager.GetGameById(1);
            result.Add(_mapper.Map<GameDTO>(g));
            return result;
        }
    }
}
