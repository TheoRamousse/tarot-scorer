using AutoMapper;
using GraphQL;
using GraphQLApi.Inputs;
using GraphQLApi.Payloads;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using Microsoft.Extensions.Logging;
using Model;
using System;
using System.Threading.Tasks;
using TarotDB;
using TarotDB2Model;
using TarotDTO;

namespace GraphQLApi.Mutation
{
    public class GameMutation
    {
        [GraphQLMetadata("addGame")]
        public async Task<bool> AddGameAsync(GameDTO gameToAdd, [Service] IDataManager context, [Service] IMapper mapper, [Service] ILogger<GameMutation> logger, [Service] IUnitOfWork unit)
        {
            bool result = true;
            logger.LogInformation("Entered in the method AddGameAsync with parameters : "+gameToAdd);
            try
            {
                var game = mapper.Map<Game>(gameToAdd);
                game.Rules = new FakeTarotRuleForApi();
                await context.AddGame(game);
            }catch(Exception ex)
            {
                result = false;
            }
            logger.LogInformation("Exited in the method AddGameAsync");
            return result;
        }


        [GraphQLMetadata("addPlayer")]
        public async Task<bool> AddPlayerAsync(PlayerDTO playerToAdd, [Service] IDataManager context, [Service] IMapper mapper, [Service] ILogger<GameMutation> logger)
        {
            bool result = true;
            logger.LogInformation("Entered in the method AddPlayerAsync with parameters : " + playerToAdd);
            try
            {
                var player = mapper.Map<Player>(playerToAdd);
                player.Image = "";
                return await context.AddPlayer(player);
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

    }
}
