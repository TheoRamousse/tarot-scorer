using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarotDTO;

namespace Shared
{
    public class PlayerFactory 
    {
        public static Player ToModel(PlayerDTO dto)
        {
            return new Player(
                dto.Id,
                dto.FirstName,
                dto.LastName,
                dto.NickName,"");
        }

        internal static Player[] ToModel(IEnumerable<PlayerDTO> players)
        {
            List<Player> list = new List<Player>();
            players.ToList().ForEach(player => list.Add(ToModel(player)));
            return list.ToArray();
        }

        public static PlayerDTO ToDTO(Player model)
        {
            return new PlayerDTO() { 
                Id = model.Id, 
                FirstName = model.FirstName, 
                LastName = model.LastName, };
        }

        public static PlayerAndBiddingDTO ToDTO(Player model,Bidding bidding)
        {
            return new PlayerAndBiddingDTO()
            {
                Player = PlayerFactory.ToDTO(model),
                Bidding = bidding
            };
        }

        public static Tuple<Player, Bidding>[] ToModel(IEnumerable<PlayerAndBiddingDTO> dto)
        {
            Collection<Tuple<Player, Bidding>> lTuple = new Collection<Tuple<Player, Bidding>>();
            dto.ToList().ForEach(playBind => lTuple.Add(new Tuple<Player, Bidding>(ToModel(playBind.Player),playBind.Bidding)));
            return lTuple.ToArray();
        }

        public static List<PlayerAndBiddingDTO> ToDTO(IDictionary<Player, Bidding> players)
        {
            List<PlayerAndBiddingDTO> list = new List<PlayerAndBiddingDTO>();
            players.ToList().ForEach(p => list.Add(ToDTO(p.Key, p.Value)));
            return list;
        }

        public static List<PlayerDTO> ToDTO(IEnumerable<Player> models)
        {
            List<PlayerDTO> list = new List<PlayerDTO>();
            models.ToList().ForEach(m => list.Add(ToDTO(m)));
            return list;
        }
    }


}
