using AutoMapper;
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
            CreateMap<Game, GameDTO>(); // means you want to map from Game to GameDTO
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
        }
    }
}
