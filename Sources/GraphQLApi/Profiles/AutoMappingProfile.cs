using AutoMapper;
using Model;
using TarotDTO;

namespace GraphQLApi.Profiles
{
    public class AutoMappingProfile : Profile
    {
        public AutoMappingProfile()
        {
            CreateMap<Game, GameDTO>(); // means you want to map from User to UserDTO
            CreateMap<Player, PlayerDTO>();
        }
    }
}
