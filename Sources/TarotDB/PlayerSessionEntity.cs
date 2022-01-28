using System;
using System.Collections.Generic;

namespace TarotDB
{
    public class PlayerSessionEntity
    {
        public PlayerEntity Player { get; set; }

        public SessionEntity Session { get; set; }

        public static PlayerSessionEntityEqualityComparer EqualityComparer { get; } = new PlayerSessionEntityEqualityComparer();
    }

    class PlayerSessionEntityEqualityComparer : EqualityComparer<PlayerSessionEntity>
    {
        public override bool Equals(PlayerSessionEntity x, PlayerSessionEntity y)
        {
            if(x.Player.Id == 0 || x.Session.Id == 0 || y.Player.Id == 0 || y.Session.Id == 0)
                return false;
            return x.Player.Id == y.Player.Id && x.Session.Id == y.Session.Id;
        }

        public override int GetHashCode(PlayerSessionEntity obj)
        {
            return (int)(obj.Player.Id % 31 + obj.Session.Id);
        }
    }
}
