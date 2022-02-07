using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TarotDB
{
    public class SessionEntity : IEntity
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public DateTime? StartingTime { get; set; }

        public DateTime? EndingTime { get; set; }

        public ICollection<PlayerSessionEntity> Players { get; set; } = new List<PlayerSessionEntity>();
    }
}
