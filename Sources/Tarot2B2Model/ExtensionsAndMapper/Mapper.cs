using System;
using System.Collections.Generic;
using System.Linq;
using Model;
using TarotDB;

namespace TarotDB2Model
{
    class Mapper<TModel, TEntity> where TModel : class
                                  where TEntity : class
    {
        HashSet<Tuple<TModel, TEntity>> mapper = new HashSet<Tuple<TModel, TEntity>>();

        public void Reset()
        {
            mapper.Clear();
        }

        public TModel GetModel(TEntity entity)
        {
            var result = mapper.Where(tuple => ReferenceEquals(tuple.Item2, entity));
            if(result.Count() != 1)
            {
                return null;
            }
            return result.First().Item1;
        }

        public TEntity GetEntity(TModel model)
        {
            var result = mapper.Where(tuple => ReferenceEquals(tuple.Item2, model));
            if(result.Count() != 1)
            {
                return null;
            }
            return result.First().Item2;
        }

        public bool AddMapping(TModel model, TEntity entity)
        {
            var mapping = new Tuple<TModel, TEntity>(model, entity);
            if(mapper.Contains(mapping)) return false;
            mapper.Add(mapping);
            return true;
        }
    }

    static class Mapper
    {
        internal static Mapper<Player, PlayerEntity> PlayersMapper { get; } = new Mapper<Player, PlayerEntity>();
        internal static Mapper<Session, SessionEntity> SessionsMapper { get; } = new Mapper<Session, SessionEntity>();
        internal static Mapper<Game, GameEntity> GamesMapper { get; } = new Mapper<Game, GameEntity>();

        internal static void Reset()
        {
            PlayersMapper.Reset();
            SessionsMapper.Reset();
            GamesMapper.Reset();
        }
    }
}
