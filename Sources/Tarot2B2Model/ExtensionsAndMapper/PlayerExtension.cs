using System;
using System.Collections.Generic;
using System.Linq;
using Model;
using TarotDB;
using static TarotDB2Model.Mapper;

namespace TarotDB2Model
{
    static class PlayerExtension
    {
        public static PlayerEntity ToEntity(this Player player)
        {
            var result = PlayersMapper.GetEntity(player);

            if(result == null)
            {
                result = new PlayerEntity
                {
                    Id = player.Id,
                    FirstName = player.FirstName,
                    LastName = player.LastName,
                    NickName = player.NickName,
                    ImageName = player.Image
                };
            }
            return result;
        }

        public static Player ToModel(this PlayerEntity entity)
        {
            var result = PlayersMapper.GetModel(entity);

            if(result == null)
            {
                result = new Player(entity.Id, entity.FirstName, entity.LastName, entity.NickName, entity.ImageName);
            }

            return result;
        }

        public static IEnumerable<PlayerEntity> ToEntities(this IEnumerable<Player> players)
        {
            return players.Select(p => p.ToEntity());
        }

        public static IEnumerable<Player> ToModels(this IEnumerable<PlayerEntity> entities)
        {
            return entities.Select(e => e.ToModel());
        }
    }
}
