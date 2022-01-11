using System;
using System.Collections.Generic;

namespace TarotDB
{
    class PlayerBiddingEntity
    {
        public PlayerEntity Player { get; set; }

        public GameEntity Game { get; set; }

        public Bidding Bidding { get; set; }

        public static EqualityComparer<PlayerBiddingEntity> Comparer { get; } = new PlayerBiddingEntityComparer();
    }

    class PlayerBiddingEntityComparer : EqualityComparer<PlayerBiddingEntity>
    {
        public override bool Equals(PlayerBiddingEntity x, PlayerBiddingEntity y)
        {
            if(x.Player.Id == 0 || y.Player.Id == 0 || x.Game.Id == 0 || y.Game.Id == 0)
            {
                return false;
            }
            return x.Player.Id == y.Player.Id && x.Game.Id == y.Game.Id && x.Bidding == y.Bidding;
        }

        public override int GetHashCode(PlayerBiddingEntity obj)
        {
            return obj.Game.GetHashCode();
        }
    }
}
