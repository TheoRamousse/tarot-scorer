using System;
using System.Collections.Generic;
using TarotDB;

namespace TarotDB_UT
{
    public class TestData_Game
    {
        public static IEnumerable<object[]> Games
        {
            get
            {
                yield return new object[]
                {
                    1, new DateTime(2021, 1, 1), "FrenchTarotRules", 49, PetitResult.SavedAuBout, Poignée.Simple, true, false, Chelem.Unknown,
                    Tuple.Create(Bidding.GardeSans, (long)1),
                    Tuple.Create(Bidding.Opponent, (long)2),
                    Tuple.Create(Bidding.Opponent, (long)3),
                };
                yield return new object[]
                {
                    2, new DateTime(2021, 2, 2), "FrenchTarotRules", 45, PetitResult.LostAuBout, Poignée.None, true, true, Chelem.Unknown,
                    Tuple.Create(Bidding.Garde, (long)4),
                    Tuple.Create(Bidding.Opponent, (long)5),
                    Tuple.Create(Bidding.Opponent, (long)6),
                    Tuple.Create(Bidding.Opponent, (long)7),
                };
                yield return new object[]
                {
                    3, new DateTime(2021, 3, 3), "FrenchTarotRules", 44, PetitResult.SavedAuBout, Poignée.Simple, false, false, Chelem.Unknown,
                    Tuple.Create(Bidding.Petite, (long)8),
                    Tuple.Create(Bidding.KingCalled, (long)9),
                    Tuple.Create(Bidding.Opponent, (long)10),
                    Tuple.Create(Bidding.Opponent, (long)11),
                    Tuple.Create(Bidding.Opponent, (long)12),
                };
            }
        }

        public static IEnumerable<object[]> GamesToAdd
        {
            get
            {
                yield return new object[]
                {
                    DateTime.Now, "FrenchTarotRules", 42,
                    PetitResult.Unknown, Poignée.Unknown, true, false, Chelem.AnnouncedFail,
                    new Tuple<PlayerEntity, Bidding>(new PlayerEntity { FirstName = "Charlie", LastName = "Parker", NickName = "Bird", ImageName = null }, Bidding.Pousse),
                    new Tuple<PlayerEntity, Bidding>(new PlayerEntity { FirstName = "Dizzy", LastName = "Gillespie", NickName = "Dizz", ImageName = null }, Bidding.Opponent),
                    new Tuple<PlayerEntity, Bidding>(new PlayerEntity { FirstName = "Stan", LastName = "Getz", NickName = "", ImageName = null }, Bidding.Opponent),
                };

                yield return new object[]
                {
                    DateTime.Now, "FrenchTarotRules", 63,
                    PetitResult.Unknown, Poignée.Unknown, true, false, Chelem.AnnouncedFail,
                    new Tuple<PlayerEntity, Bidding>(new PlayerEntity { FirstName = "Charlie", LastName = "Parker", NickName = "Bird", ImageName = null }, Bidding.Pousse),
                    new Tuple<PlayerEntity, Bidding>(new PlayerEntity { FirstName = "Dizzy", LastName = "Gillespie", NickName = "Dizz", ImageName = null }, Bidding.Opponent),
                    new Tuple<PlayerEntity, Bidding>(new PlayerEntity { FirstName = "Stan", LastName = "Getz", NickName = "", ImageName = null }, Bidding.Opponent),
                    new Tuple<PlayerEntity, Bidding>(new PlayerEntity { FirstName = "Sonny", LastName = "Stitt", NickName = "", ImageName = null }, Bidding.Opponent),
                };

                yield return new object[]
                {
                    DateTime.Now, "FrenchTarotRules", 84,
                    PetitResult.Unknown, Poignée.Unknown, true, false, Chelem.NotAnnouncedSuccess,
                    new Tuple<PlayerEntity, Bidding>(new PlayerEntity { FirstName = "Charlie", LastName = "Parker", NickName = "Bird", ImageName = null }, Bidding.Pousse),
                    new Tuple<PlayerEntity, Bidding>(new PlayerEntity { FirstName = "Dizzy", LastName = "Gillespie", NickName = "Dizz", ImageName = null }, Bidding.KingCalled),
                    new Tuple<PlayerEntity, Bidding>(new PlayerEntity { FirstName = "Stan", LastName = "Getz", NickName = "", ImageName = null }, Bidding.Opponent),
                    new Tuple<PlayerEntity, Bidding>(new PlayerEntity { FirstName = "Sonny", LastName = "Stitt", NickName = "", ImageName = null }, Bidding.Opponent),
                    new Tuple<PlayerEntity, Bidding>(new PlayerEntity { FirstName = "Sonny", LastName = "Rollins", NickName = "", ImageName = null }, Bidding.Opponent),
                };

                yield return new object[]
                {
                    DateTime.Now, "FrenchTarotRules", 84,
                    PetitResult.Unknown, Poignée.Unknown, true, false, Chelem.NotAnnouncedSuccess,
                    new Tuple<PlayerEntity, Bidding>(new PlayerEntity { FirstName = "Charlie", LastName = "Parker", NickName = "Bird", ImageName = null }, Bidding.Pousse),
                    new Tuple<PlayerEntity, Bidding>(new PlayerEntity { Id = 2 }, Bidding.KingCalled),
                    new Tuple<PlayerEntity, Bidding>(new PlayerEntity { FirstName = "Stan", LastName = "Getz", NickName = "", ImageName = null }, Bidding.Opponent),
                    new Tuple<PlayerEntity, Bidding>(new PlayerEntity { Id = 4 }, Bidding.Opponent),
                    new Tuple<PlayerEntity, Bidding>(new PlayerEntity { FirstName = "Sonny", LastName = "Rollins", NickName = "", ImageName = null }, Bidding.Opponent),
                };

            }
        }
    }
}
