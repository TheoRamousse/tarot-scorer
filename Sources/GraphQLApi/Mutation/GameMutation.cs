using AutoMapper;
using GraphQL;
using GraphQLApi.Inputs;
using GraphQLApi.Payloads;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using Microsoft.Extensions.Logging;
using Model;
using NLog;
using System.Threading.Tasks;
using TarotDTO;

namespace GraphQLApi.Mutation
{
    public class GameMutation
    {
        [GraphQLMetadata("addGame")]
        public async Task<GameDTOPayload> AddGameAsync(GameDTOInput gameToAdd, [Service] IDataManager context, [Service] IMapper mapper, [Service] ILogger<GameMutation> logger)
        {
            var result = await context.AddGame(mapper.Map<Game>(gameToAdd));

            return null;
        }

        [UseMutationConvention]
        // [Error(...)] possibilité de prendre en charge des exceptions personnalisées.
        public async Task<GameDTOPayload> UpdateGameAsync([ID] long gameId, GameDTOInput gameToUpdate, [Service] IDataManager context, [Service] IMapper mapper, [Service] ILogger<GameMutation> logger)
        {
            Game gToUp = mapper.Map<Game>(gameToUpdate);
            var resultat = await context.UpdateGame(gToUp.Id, gToUp);
            return mapper.Map<GameDTOPayload>(resultat);
        }
    }
}
