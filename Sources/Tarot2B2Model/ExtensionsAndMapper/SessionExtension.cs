using System;
using System.Collections.Generic;
using System.Linq;
using Model;
using TarotDB;
using static TarotDB2Model.Mapper;

namespace TarotDB2Model
{
    static class SessionExtension
    {
        public static Session ToModel(this SessionEntity entity)
        {
            var result = SessionsMapper.GetModel(entity);

            if(result == null)
            {
                result = new Session(entity.Id, entity.Name, entity.StartingTime, entity.EndingTime,
                    entity.Players.Select(pse => pse.Player).ToModels().ToArray());
            }

            return result;
        }

        public static IEnumerable<Session> ToModels(this IEnumerable<SessionEntity> entities)
            => entities.Select(entity => entity.ToModel());

        public static SessionEntity ToEntity(this Session model)
        {
            var result = SessionsMapper.GetEntity(model);

            if(result == null)
            {
                result = new SessionEntity
                {
                    Id = model.Id,
                    Name = model.Name,
                    StartingTime = model.StartingTime,
                    EndingTime = model.EndingTime
                };
                foreach(var p in model.Players)
                {
                    result.Players.Add(new PlayerSessionEntity
                                        {
                                            Player = p.ToEntity(),
                                            Session = result
                                        });
                }
            }
            return result;
        }

        public static IEnumerable<SessionEntity> ToEntities(this IEnumerable<Session> models)
            => models.Select(m => m.ToEntity());
    }
}
