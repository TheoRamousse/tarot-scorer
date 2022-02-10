using AutoMapper;
using GraphQLApi.Inputs;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using TarotDB;
using TarotDTO;

namespace GraphQLApi.Profiles
{
    public class AutoMappingProfile : Profile
    {
        public AutoMappingProfile()
        {

            CreateMap<Game, GameDTO>().ReverseMap(); // means you want to map from Game to GameDTO
            CreateMap<KeyValuePair<Player, Model.Bidding>, PlayerAndBiddingDTO>().ConstructUsing(v => ComputePlayerAndBidding(v));
            CreateMap<PlayerAndBiddingDTO, KeyValuePair<Player, Model.Bidding>>().ConstructUsing(v => new KeyValuePair<Player, Model.Bidding>(new Player(v.Player.Id, v.Player.FirstName, v.Player.LastName, v.Player.NickName, ""), v.Bidding));

            CreateMap<Player, PlayerDTO>().ConstructUsing(v => new PlayerDTO()
            {
                Id = v.Id,
                FirstName = v.FirstName,
                LastName = v.LastName,
                NickName = v.NickName

            });

            CreateMap<PlayerDTO, Player>().ConstructUsing(v => new Player(v.Id, v.FirstName, v.LastName, v.NickName, ""));

            CreateMap<GameDTOInput, Game>(); // means you want to map from Game to GameDTO
            CreateMap<PlayerAndBiddingDTOInput, KeyValuePair<Player, Model.Bidding>>().ConstructUsing(v => new KeyValuePair<Player, Model.Bidding>(new Player(0, v.player.firstName, v.player.lastName, v.player.nickName, ""), v.bidding));
        }

        private static PlayerAndBiddingDTO ComputePlayerAndBidding(KeyValuePair<Player, Model.Bidding> v)
        {
            return new PlayerAndBiddingDTO()
            {
                Player = new PlayerDTO()
                {
                    Id = v.Key.Id,
                    LastName = v.Key.LastName,
                    FirstName = v.Key.FirstName,
                    NickName = v.Key.NickName
                },
                Bidding = v.Value
            };
        }
    }
}
