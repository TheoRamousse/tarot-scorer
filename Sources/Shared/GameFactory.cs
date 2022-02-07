using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarotDTO;

namespace Shared
{
    public class GameFactory
    {
        public static Game ToModel(GameDTO dto)
        {
            return new Game(
                dto.Id, 
                dto.Date, 
                null, 
                dto.TakerPoints, 
                PetitResult.Unknown,
                Poignée.Unknown,
                dto.Excuse,
                dto.TwentyOne, 
                Chelem.Unknown, 
                PlayerFactory.ToModel(dto.Players));
        }

        public static GameDTO ToDTO(Game model)
        {
            //Manque des trucs frere
            return new GameDTO()
            {
                Id = model.Id,
                Date = model.Date,
                TakerPoints = model.TakerPoints,
                Excuse = model.Excuse,
                TwentyOne = model.TwentyOne,
                Players = PlayerFactory.ToDTO(model.Players)
            };
        }

        public static List<GameDTO> ToDTO(IEnumerable<Game> model)
        {
            List<GameDTO> list = new List<GameDTO>();
            model.ToList().ForEach(m => list.Add(ToDTO(m)));
            return list;
        }
    }
}
