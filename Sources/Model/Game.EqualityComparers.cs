using System;
using System.Collections.Generic;

namespace Model
{
    public partial class Game
    {
        class FullEqComparer : EqualityComparer<Game>
        {
            public override bool Equals(Game x, Game y)
            {
                bool result = 
                        x.Id == y.Id
                        && x.Chelem == y.Chelem
                        && x.Date == y.Date
                        && x.Excuse == y.Excuse
                        && x.PetitResult == y.PetitResult
                        && x.Poignée == y.Poignée
                        && x.Rules.Equals(y.Rules)
                        && x.TakerPoints == y.TakerPoints
                        && x.TwentyOne == y.TwentyOne;
                if(!result) return false;
                foreach (var pb in y.Players)
                {
                    result = x.Players.TryGetValue(pb.Key, out Bidding value);
                    if(!result) break;
                    result = (value == pb.Value);
                    if(!result) break;
                }
                return result;
            }

            public override int GetHashCode(Game obj)
            {
                return obj.GetHashCode();
            }
        }

        /// <summary>
        /// a full game comparer: all properties are compared (including Id)
        /// </summary>
        public static EqualityComparer<Game> FullComparer { get; } = new FullEqComparer();
    }
}
