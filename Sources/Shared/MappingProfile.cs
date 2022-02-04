using AutoMapper;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarotDB;
using TarotDTO;

namespace Shared
{
    public class MappingProfile : Profile
    {
        protected void Configure()
        {
            // means you want to map from Game to GameDTO
            CreateMap<PlayerDTO, Player>().ReverseMap();
            CreateMap<KeyValuePair<Player, Model.Bidding>, PlayerAndBiddingDTO>().ConstructUsing(v => new PlayerAndBiddingDTO()
            {
                Player = new PlayerDTO()
                {
                    Id = v.Key.Id,
                    LastName = v.Key.LastName,
                    FirstName = v.Key.FirstName,
                    NickName = v.Key.NickName
                },
                Bidding = v.Value
            });
            CreateMap<PlayerAndBiddingDTO, KeyValuePair<Player, Model.Bidding>>().ConstructUsing(v => new KeyValuePair<Player, Model.Bidding>
            (
                new Player
                (
                    v.Player.Id, 
                    v.Player.LastName, 
                    v.Player.FirstName, 
                    v.Player.NickName, 
                    ""
                ), 
                v.Bidding
            ));
            CreateMap<GameDTO, Game>().ReverseMap();
            CreateMap<SessionDTO, Session>().ReverseMap();
        }
    }
}
