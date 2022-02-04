using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarotDTO;

namespace Shared
{
    public class SessionFactory
    {
        public static Session ToModel(SessionDTO dto)
        {
            return new Session(
                dto.Id,
                dto.Name, 
                dto.StartingTime, 
                dto.EndingTime, 
                PlayerFactory.ToModel(dto.Players));
        }

        public static SessionDTO ToDTO(Session model)
        {
            return new SessionDTO
            {
                Id = model.Id,
                Name = model.Name,
                StartingTime = model.StartingTime,
                EndingTime = model.EndingTime,
                Players = PlayerFactory.ToDTO(model.Players)
            };
        }

        public static SessionDTO[] ToDTO(IEnumerable<Session> model)
        {
            List<SessionDTO> list = new List<SessionDTO>();
            model.ToList().ForEach(s => list.Add(SessionFactory.ToDTO(s)));
            return list.ToArray();
        }
    }
}
