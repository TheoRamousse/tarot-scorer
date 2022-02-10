﻿using AutoMapper;
using GraphQL;
using GraphQLApi.Inputs;
using GraphQLApi.Payloads;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using Microsoft.Extensions.Logging;
using Model;
using System.Threading.Tasks;
using TarotDB;
using TarotDB2Model;
using TarotDTO;

namespace GraphQLApi.Mutation
{
    public class GameMutation
    {
        [GraphQLMetadata("addGame")]
        public async Task<GameDTOPayload> AddGameAsync(GameDTO gameToAdd, [Service] IDataManager context, [Service] IMapper mapper, [Service] ILogger<GameMutation> logger, [Service] IUnitOfWork unit)
        {
            var game = mapper.Map<Game>(gameToAdd);
            game.Rules = new FakeTarotRuleForApi();
            var result = await context.AddGame(game);

            return null;
        }


        [GraphQLMetadata("addPlayer")]
        public async Task<PlayerDTOPayload> AddPlayerAsync(PlayerDTO playerToAdd, [Service] IDataManager context, [Service] IMapper mapper, [Service] ILogger<GameMutation> logger)
        {
            var player = mapper.Map<Player>(playerToAdd);
            player.Image = "";
            var result = await context.AddPlayer(player);

            return null;
        }

        /*[UseMutationConvention]
        // [Error(...)] possibilité de prendre en charge des exceptions personnalisées.
        public async Task<GameDTOPayload> UpdateGameAsync([ID] long gameId, GameDTOInput gameToUpdate, [Service] IDataManager context, [Service] IMapper mapper, [Service] ILogger<GameMutation> logger)
        {
            Game gToUp = mapper.Map<Game>(gameToUpdate);
            var resultat = await context.UpdateGame(gToUp.Id, gToUp);
            return mapper.Map<GameDTOPayload>(resultat);
        }*/
    }
}
