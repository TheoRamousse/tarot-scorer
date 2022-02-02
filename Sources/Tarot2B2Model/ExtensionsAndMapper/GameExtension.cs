using System;
using System.Collections.Generic;
using System.Linq;
using Model;
using TarotDB;
using static TarotDB2Model.Mapper;
using static TarotDB2Model.EnumsMapper;

namespace TarotDB2Model
{
    static class GameExtension
    {
        public static GameEntity ToEntity(this Game model)
        {
            var result = GamesMapper.GetEntity(model);

            if(result == null)
            {
                if (model.Rules != null)
                {
                    result = new GameEntity
                    {
                        Id = model.Id,
                        DateTime = model.Date,
                        Chelem = model.Chelem.ToEntity(),
                        Poignée = model.Poignée.ToEntity(),
                        Excuse = model.Excuse,
                        Petit = model.PetitResult.ToEntity(),
                        TakerPoints = model.TakerPoints,
                        TwentyOne = model.TwentyOne,
                        Rules = model.Rules.Name
                    };
                }
                else
                {
                    result = new GameEntity
                    {
                        Id = model.Id,
                        DateTime = model.Date,
                        Chelem = model.Chelem.ToEntity(),
                        Poignée = model.Poignée.ToEntity(),
                        Excuse = model.Excuse,
                        Petit = model.PetitResult.ToEntity(),
                        TakerPoints = model.TakerPoints,
                        TwentyOne = model.TwentyOne
                    };
                }
                foreach(var b in model.Players)
                {
                    result.Biddings.Add(new PlayerBiddingEntity
                    {
                        Bidding = b.Value.ToEntity(),
                        Player = b.Key.ToEntity(),
                        Game = result
                    });
                }
            }

            return result;
        }

        public static IEnumerable<GameEntity> ToEntities(this IEnumerable<Game> models)
            => models.Select(m => m.ToEntity());

        public static Game ToModel(this GameEntity entity)
        {
            var result = GamesMapper.GetModel(entity);

            if(result == null)
            {
                result = new Game(entity.Id,
                                  entity.DateTime,
                                  RulesFactory.Create(entity.Rules),
                                  entity.TakerPoints,
                                  entity.Petit.ToModel(),
                                  entity.Poignée.ToModel(),
                                  entity.Excuse,
                                  entity.TwentyOne,
                                  entity.Chelem.ToModel());
                result.AddPlayers(entity.Biddings.Select(b => Tuple.Create(b.Player.ToModel(), b.Bidding.ToModel())).ToArray());
            }

            return result;
        }

        public static IEnumerable<Game> ToModels(this IEnumerable<GameEntity> entities)
            => entities.Select(e => e.ToModel());
    }
}
