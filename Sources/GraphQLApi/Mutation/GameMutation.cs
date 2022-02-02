﻿using AutoMapper;
using GraphQL;
using GraphQLApi.Inputs;
using GraphQLApi.Payloads;
using HotChocolate;
using Microsoft.Extensions.Logging;
using Model;
using System.Threading.Tasks;
using TarotDTO;

namespace GraphQLApi.Mutation
{
    public class GameMutation
    {
        [GraphQLMetadata("addGame")]
        public async Task<GameDTOPayload> AddGameAsync(GameDTO gameToAdd, [Service] IDataManager context, [Service] IMapper mapper, [Service] ILogger<GameMutation> logger)
        {
            var game = mapper.Map<Game>(gameToAdd);
            game.Rules = new FakeTarotRuleForApi();
            var result = await context.AddGame(game);

            return null;
        }
    }
}
