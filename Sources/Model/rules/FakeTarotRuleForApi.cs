using System;
using System.Collections.Generic;
using System.Linq;
using static Model.IRules;

namespace Model
{
    public class FakeTarotRuleForApi : IRules
    {
        public bool CheckValid(Game game, out Validity validity)
        {
            CheckNbPlayers(game, out validity);
            return true;
        }

        public int MinNbPlayers { get; } = 0;

        public int MaxNbPlayers { get; } = 999;

        public int MinNbPlayersForKingCalled { get; } = 0;

        public int MaxNbKingsCalled { get; } = 999;

        public bool Equals(IRules other)
        {
            return other.GetType().Equals(GetType());
        }

        public override bool Equals(object obj)
        {
            if(ReferenceEquals(obj, null)) return false;
            if(ReferenceEquals(this, obj)) return true;
            if(GetType() != obj.GetType()) return false;
            return Equals(obj as FrenchTarotRules);
        }

        public override int GetHashCode()
        {
            return GetType().Name.GetHashCode();
        }

        private bool CheckNbPlayers(Game game, out Validity validity)
        {
            if(game.Players.Count < MinNbPlayers)
            {
                validity = Validity.NotEnoughPlayers;
                return false;
            }
            if(game.Players.Count > MaxNbPlayers)
            {
                validity = Validity.TooManyPlayers;
                return false;
            }
            validity = Validity.Valid;
            return true;
        }

        private bool CheckTaker(Game game, out Validity validity)
        {
            int nbTakers = game.Players.Where(r => (r.Value & Bidding.Prise) == Bidding.Prise).Count();
            validity = Validity.Valid;
            return true;
        }

        private bool CheckKingCalled(Game game, out Validity validity)
        {
            int nbKingsCalled = game.Players.Where(r => (r.Value & Bidding.KingCalled) == Bidding.KingCalled).Count();
            validity = Validity.Valid;
            return true;
        }

        private bool CheckBiddings(Game game, out Validity validity)
        {
            CheckTaker(game, out validity);
            validity = Validity.Valid;
            return true;
        }

        //private bool CheckPoints(Game game, out Validity validity)
        //{
        //    //checks sum is 0
        //    if(game.Players.Sum(r => r.Value.Points) != 0)
        //    {
        //        validity = Validity.PointsAreNotValid;
        //        return false;
        //    }

        //    //taker points
        //    var takerResult = game.Players.SingleOrDefault(r => (r.Value.Bidding & Bidding.Prise) == Bidding.Prise);
        //    int takerPoints = takerResult.Value.Points;

        //    //nbPlayers
        //    int nbPlayers = game.Players.Count;

        //    foreach(var result in game.Players)
        //    {
        //        int sign=0;
        //        switch(result.Value.Bidding)
        //        {
        //            case Bidding.KingCalled: sign=1; break;
        //            case Bidding.Opponent: sign=-1; break;
        //            default: continue;
        //        }

        //        if(result.Value.Points * (nbPlayers-1) != sign*takerPoints)
        //        {
        //            validity = Validity.PointsAreNotValid;
        //            return false;
        //        }
        //    }
        //    validity = Validity.Valid;
        //    return true;
        //}


        
        public IReadOnlyDictionary<Player, int> GetScores(Game game)
        {
            if (game.Players.Count != 0)
            {
                var players = game.Players.OrderBy(r => r.Value);
                Bidding takerBidding = players.First().Value;
                int takerPoints = game.TakerPoints;
                int takerPointsBidding = takerPoints - GetOudlersPoints(game);
                int sign = Math.Sign(takerPointsBidding);

                int takerScore = (sign * GetPrime(takerBidding) + takerPointsBidding + GetPetitBonus(game.PetitResult))
                                    * multiplicators[takerBidding]
                                 + sign * GetBonusPoignée(game.Poignée)
                                 + sign * GetBonusChelem(game.Chelem);

                (int nbOpponents, int nbKingsCalled) = GetNbPlayers(game);

                return game.Players
                           .ToDictionary(r => r.Key,
                                         r => GetTotalPlayerPoints(r.Value, takerScore, nbOpponents, nbKingsCalled));
            }
            return null;
        }

        private static int GetOudlersPoints(Game game)
        {
            int nbOudlers = 0;
            nbOudlers += game.Excuse.GetValueOrDefault() == true ? 1 : 0;
            nbOudlers += game.TwentyOne.GetValueOrDefault() == true ? 1 : 0;
            nbOudlers += (game.PetitResult & PetitResult.Owned) == PetitResult.Owned ? 1 : 0;
            if(!oudlersPoints.TryGetValue(nbOudlers, out int points))
            {
                points = oudlersPoints[0];
            }
            return points;
        }

        private static Dictionary<int, int> oudlersPoints {get; } = new Dictionary<int, int>()
        {
            [0] = 56,
            [1] = 51,
            [2] = 41,
            [3] = 36
        };

        public string Name => GetType().Name;

        private static int GetPetitBonus (PetitResult petitResult)
        {
            if((petitResult & PetitResult.AuBout) == PetitResult.AuBout)
            {
                if((petitResult & PetitResult.Owned) != PetitResult.Owned)
                    return -10;
                return 10;
                
            }
            else
            {
                return 0;
            }
        }

        private static Dictionary<Bidding, int> primes = new Dictionary<Bidding, int>()
        {
            [Bidding.Petite] = 25,
            [Bidding.Pousse] = 25,
            [Bidding.Garde] = 25,
            [Bidding.GardeSans] = 25,
            [Bidding.GardeContre] = 25,
        };

        private static Dictionary<Bidding, int> multiplicators = new Dictionary<Bidding, int>()
        {
            [Bidding.Petite] = 1,
            [Bidding.Pousse] = 1,
            [Bidding.Garde] = 2,
            [Bidding.GardeSans] = 4,
            [Bidding.GardeContre] = 6,
        };

        private static int GetPrime(Bidding bidding)
        {
            if(!primes.TryGetValue(bidding, out int prime))
            {
                prime = 0;
            }
            return prime;
        }

        private static Dictionary<Poignée, int> primesPoignée = new Dictionary<Poignée, int>()
        {
            [Poignée.Unknown] = 0,
            [Poignée.None] = 0,
            [Poignée.Simple] = 20,
            [Poignée.Double] = 30,
            [Poignée.Triple] = 40,
            [Poignée.SimpleDefense] = 20,
            [Poignée.DoubleDefense] = 30,
            [Poignée.TripleDefense] = 40,
        };

        private static int GetBonusPoignée(Poignée poignée)
            => primesPoignée[poignée];

        private static Dictionary<Chelem ,int> primesChelem = new Dictionary<Chelem, int>()
        {
            [Chelem.AnnouncedFail] = -200,
            [Chelem.AnnouncedSuccess] = 400,
            [Chelem.NotAnnouncedSuccess] = 200
        };

        private static int GetBonusChelem(Chelem chelem)
        {
            int bonusChelem = 0;
            if(primesChelem.ContainsKey(chelem))
                bonusChelem = primesChelem[chelem];
            return bonusChelem;
        }

        private static (int nbOpponents, int nbKingsCalled) GetNbPlayers(Game game)
        {
            int nbOpponents = game.Players.Count(r => r.Value == Bidding.Opponent);
            int nbKingsCalled = game.Players.Count(r => r.Value == Bidding.KingCalled);
            return (nbOpponents, nbKingsCalled);
        }

        private static int GetTotalPlayerPoints(Bidding bidding, int takerPoints, int nbOpponents, int nbKingsCalled)
        {
            switch(bidding)
            {
                case Bidding bid when (bid & Bidding.Prise) == Bidding.Prise:
                    return takerPoints*(nbOpponents - nbKingsCalled);
                case Bidding.Opponent:
                    return -takerPoints;
                case Bidding.KingCalled:
                    return takerPoints;
                default:
                    return 0;
            }
        }
    }
}
