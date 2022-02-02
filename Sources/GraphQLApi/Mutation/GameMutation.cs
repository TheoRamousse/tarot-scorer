using AutoMapper;
using GraphQL;
using GraphQLApi.Inputs;
using GraphQLApi.Payloads;
using HotChocolate;
using Model;
using NLog;
using System.Threading.Tasks;
using TarotDTO;

namespace GraphQLApi.Mutation
{
    public class GameMutation
    {
        [GraphQLMetadata("addGame")]
        public async Task<GameDTOPayload> AddGameAsync(GameDTOInput gameToAdd, [Service] IDataManager context, [Service] IMapper mapper, [Service] ILogger logger)
        {
            var result = await context.AddGame(mapper.Map<Game>(gameToAdd));

            return null;
        }
    }
}
