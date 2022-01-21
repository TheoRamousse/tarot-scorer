using AutoMapper;
using GraphQL.Types;
using HotChocolate;
using Model;
using StubLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TarotDB;
using TarotDB2Model;
using TarotDTO;

namespace GraphQLApi.Queries
{
    public class GameQuery
    {
        public async Task<IEnumerable<GameDTO>> GetGame([Service] IDataManager context, [Service] IMapper mapper)
        {
            List<Game> l = new List<Game>(await context.GetGames(0, 10));
            List<GameDTO> result = new List<GameDTO>();
            l.ForEach(x => result.Add(mapper.Map<GameDTO>(x)));
            return result;
        }

        public String HelloWorld() => "Hello World !";
    }
}
