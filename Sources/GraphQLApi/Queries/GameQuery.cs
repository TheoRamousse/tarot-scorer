using AutoMapper;
using GraphQL;
using GraphQL.Types;
using HotChocolate;
using Microsoft.Extensions.Logging;
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

    //Utiliser projets MSTest pour tester
    //Should_ExpectedBehavior_When_StateUnderTest
    //Utiliser les commentaires pour Arrange, Act, Assert
    //Utiliser la bibliothèque fluent assertion
    //SQLLIte in memory pour les tests
    //MOQ Framework pour faire des MOCKS-> on utilise ça pour tester une seule chose à la fois dans les tests
    public class GameQuery
    {
        [GraphQLMetadata("games")]
        public async Task<IEnumerable<GameDTO>> GetGames(int numberOfElementsPerPage, int pageNumber, [Service] IDataManager context, [Service] IMapper mapper, [Service] ILogger<GameQuery> logger)
        {
            List<Game> l = new List<Game>(await context.GetGames(pageNumber, numberOfElementsPerPage));
            List<GameDTO> result = new List<GameDTO>();
            l.ForEach(x => result.Add(mapper.Map<GameDTO>(x)));
            return result;
        }

        [GraphQLMetadata("game")]
        public async Task<GameDTO> GetGame(long id, [Service] IDataManager context, [Service] IMapper mapper, [Service] ILogger<GameQuery> logger)
        {
            return mapper.Map<GameDTO>(await context.GetGameById(id));
        }

        [GraphQLMetadata("gamesByPlayerId")]
        public async Task<IEnumerable<GameDTO>> GetGamesByPlayerId(int numberOfElementsPerPage, int pageNumber, long playerId, [Service] IDataManager context, [Service] IMapper mapper, [Service] ILogger<GameQuery> logger)
        {
            var resultNotConverted = await context.GetGamesByPlayer(await context.GetPlayerById(playerId), pageNumber, numberOfElementsPerPage);
            List<GameDTO> result = new List<GameDTO>();
            new List<Game>(resultNotConverted).ForEach(x => result.Add(mapper.Map<GameDTO>(x)));
            return result;
        }

        [GraphQLMetadata("gamesByPlayerName")]
        public async Task<GameDTO> GetGameByPlayerName(int numberOfElementsPerPage, int pageNumber, string name, [Service] IDataManager context, [Service] IMapper mapper, [Service] ILogger<GameQuery> logger)
        {
            List<Game> result = new List<Game>();
            foreach(Player p in await context.GetPlayersByName(name, 0, int.MaxValue))
            {
                result.AddRange(await context.GetGamesByPlayer(p, pageNumber, numberOfElementsPerPage));
            }
            return mapper.Map<GameDTO>(result);
        }

    }
}
