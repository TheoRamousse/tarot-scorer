using System;
using System.Collections.Generic;
using Model;
using Xunit;

namespace Model_UT
{
    public class TestData_Rules
    {
        public static IEnumerable<object[]> Games
        {
            get
            {
                //valid 3 players game
                yield return new object[]
                {
                    true,
                    Validity.Valid,
                    new Game(DateTime.Now, new FrenchTarotRules(), 21, PetitResult.Unknown, Poignée.Unknown, true, false, Chelem.Unknown,
                        Tuple.Create(new Player("Charlie", "Parker", "Bird", null), Bidding.Pousse),
                        Tuple.Create(new Player("Dizzy", "Gillespie", "Dizz", null), Bidding.Opponent),
                        Tuple.Create(new Player("Stan", "Getz", "", null), Bidding.Opponent)),
                };
                //valid 4 players game
                yield return new object[]
                {
                    true,
                    Validity.Valid,
                    new Game(DateTime.Now, new FrenchTarotRules(), 21, PetitResult.Unknown, Poignée.Unknown, true, false, Chelem.Unknown,
                        Tuple.Create(new Player("Charlie", "Parker", "Bird", null), Bidding.Pousse),
                        Tuple.Create(new Player("Dizzy", "Gillespie", "Dizz", null), Bidding.Opponent),
                        Tuple.Create(new Player("Sonny", "Stitt", "", null), Bidding.Opponent),
                        Tuple.Create(new Player("Stan", "Getz", "", null), Bidding.Opponent)),
                };
                //valid 5 players game (2 vs 3)
                yield return new object[]
                {
                    true,
                    Validity.Valid,
                    new Game(DateTime.Now, new FrenchTarotRules(), 21, PetitResult.Unknown, Poignée.Unknown, true, false, Chelem.Unknown,
                        Tuple.Create(new Player("Charlie", "Parker", "Bird", null), Bidding.Pousse),
                        Tuple.Create(new Player("Dizzy", "Gillespie", "Dizz", null), Bidding.KingCalled),
                        Tuple.Create(new Player("Sonny", "Stitt", "", null), Bidding.Opponent),
                        Tuple.Create(new Player("Sonny", "Rollins", "", null), Bidding.Opponent),
                        Tuple.Create(new Player("Stan", "Getz", "", null), Bidding.Opponent)),
                };
                //valid 5 players game (1 vs 4)
                yield return new object[]
                {
                    true,
                    Validity.Valid,
                    new Game(DateTime.Now, new FrenchTarotRules(), 21, PetitResult.Unknown, Poignée.Unknown, true, false, Chelem.Unknown,
                        Tuple.Create(new Player("Charlie", "Parker", "Bird", null), Bidding.Pousse),
                        Tuple.Create(new Player("Dizzy", "Gillespie", "Dizz", null), Bidding.Opponent),
                        Tuple.Create(new Player("Sonny", "Stitt", "", null), Bidding.Opponent),
                        Tuple.Create(new Player("Sonny", "Rollins", "", null), Bidding.Opponent),
                        Tuple.Create(new Player("Stan", "Getz", "", null), Bidding.Opponent)),
                };
                //not valid game (NotEnoughPlayers)
                yield return new object[]
                {
                    false,
                    Validity.NotEnoughPlayers,
                    new Game(DateTime.Now, new FrenchTarotRules(), 21, PetitResult.Unknown, Poignée.Unknown, true, false, Chelem.Unknown,
                        Tuple.Create(new Player("Charlie", "Parker", "Bird", null), Bidding.Pousse),
                        Tuple.Create(new Player("Stan", "Getz", "", null), Bidding.Opponent)),
                };
                //not valid game (NotEnoughPlayers)
                yield return new object[]
                {
                    false,
                    Validity.TooManyPlayers,
                    new Game(DateTime.Now, new FrenchTarotRules(), 21, PetitResult.Unknown, Poignée.Unknown, true, false, Chelem.Unknown,
                        Tuple.Create(new Player("Charlie", "Parker", "Bird", null), Bidding.Pousse),
                        Tuple.Create(new Player("Dizzy", "Gillespie", "Dizz", null), Bidding.Opponent),
                        Tuple.Create(new Player("Sonny", "Stitt", "", null), Bidding.Opponent),
                        Tuple.Create(new Player("Sonny", "Rollins", "", null), Bidding.Opponent),
                        Tuple.Create(new Player("Chick", "Corea", "", null), Bidding.Opponent),
                        Tuple.Create(new Player("Stan", "Getz", "", null), Bidding.Opponent)),
                };
                //not valid game (NoTaker)
                yield return new object[]
                {
                    false,
                    Validity.NoTaker,
                    new Game(DateTime.Now, new FrenchTarotRules(), 21, PetitResult.Unknown, Poignée.Unknown, true, false, Chelem.Unknown,
                        Tuple.Create(new Player("Charlie", "Parker", "Bird", null), Bidding.KingCalled),
                        Tuple.Create(new Player("Dizzy", "Gillespie", "Dizz", null), Bidding.Opponent),
                        Tuple.Create(new Player("Stan", "Getz", "", null), Bidding.Opponent)),
                };
                //not valid game (2 takers)
                yield return new object[]
                {
                    false,
                    Validity.TooManyTakers,
                    new Game(DateTime.Now, new FrenchTarotRules(), 21, PetitResult.Unknown, Poignée.Unknown, true, false, Chelem.Unknown,
                        Tuple.Create(new Player("Charlie", "Parker", "Bird", null), Bidding.Pousse),
                        Tuple.Create(new Player("Dizzy", "Gillespie", "Dizz", null), Bidding.KingCalled),
                        Tuple.Create(new Player("Sonny", "Stitt", "", null), Bidding.Opponent),
                        Tuple.Create(new Player("Sonny", "Rollins", "", null), Bidding.Garde),
                        Tuple.Create(new Player("Stan", "Getz", "", null), Bidding.Opponent)),
                };
                //not valid game (1 King Called in a 4 players game)
                yield return new object[]
                {
                    false,
                    Validity.ShouldNotHaveKingCalled,
                    new Game(DateTime.Now, new FrenchTarotRules(), 21, PetitResult.Unknown, Poignée.Unknown, true, false, Chelem.Unknown,
                        Tuple.Create(new Player("Charlie", "Parker", "Bird", null), Bidding.Pousse),
                        Tuple.Create(new Player("Dizzy", "Gillespie", "Dizz", null), Bidding.KingCalled),
                        Tuple.Create(new Player("Sonny", "Stitt", "", null), Bidding.Opponent),
                        Tuple.Create(new Player("Stan", "Getz", "", null), Bidding.Opponent)),
                };
                //not valid game (1 King Called in a 3 players game)
                yield return new object[]
                {
                    false,
                    Validity.ShouldNotHaveKingCalled,
                    new Game(DateTime.Now, new FrenchTarotRules(), 21, PetitResult.Unknown, Poignée.Unknown, true, false, Chelem.Unknown,
                        Tuple.Create(new Player("Charlie", "Parker", "Bird", null), Bidding.Pousse),
                        Tuple.Create(new Player("Dizzy", "Gillespie", "Dizz", null), Bidding.KingCalled),
                        Tuple.Create(new Player("Stan", "Getz", "", null), Bidding.Opponent)),
                };
                //not valid game (2 kings called)
                yield return new object[]
                {
                    false,
                    Validity.TooManyKingsCalled,
                    new Game(DateTime.Now, new FrenchTarotRules(), 21, PetitResult.Unknown, Poignée.Unknown, true, false, Chelem.Unknown,
                        Tuple.Create(new Player("Charlie", "Parker", "Bird", null), Bidding.Pousse),
                        Tuple.Create(new Player("Dizzy", "Gillespie", "Dizz", null), Bidding.KingCalled),
                        Tuple.Create(new Player("Sonny", "Stitt", "", null), Bidding.Opponent),
                        Tuple.Create(new Player("Sonny", "Rollins", "", null), Bidding.KingCalled),
                        Tuple.Create(new Player("Stan", "Getz", "", null), Bidding.Opponent)),
                };
                //not valid game (1 player has no bidding value)
                yield return new object[]
                {
                    false,
                    Validity.PlayerShallHaveBidding,
                    new Game(DateTime.Now, new FrenchTarotRules(), 21, PetitResult.Unknown, Poignée.Unknown, true, false, Chelem.Unknown,
                        Tuple.Create(new Player("Charlie", "Parker", "Bird", null), Bidding.Pousse),
                        Tuple.Create(new Player("Dizzy", "Gillespie", "Dizz", null), Bidding.KingCalled),
                        Tuple.Create(new Player("Sonny", "Stitt", "", null), Bidding.Opponent),
                        Tuple.Create(new Player("Sonny", "Rollins", "", null), Bidding.None),
                        Tuple.Create(new Player("Stan", "Getz", "", null), Bidding.Opponent)),
                };
            }
        }

        public static IEnumerable<object[]> Scores
        {
            get
            {
                //valid 4 players game
                yield return new object[]
                {
                    new Game(DateTime.Now, new FrenchTarotRules(), 49, PetitResult.SavedAuBout, Poignée.Simple, true, false, Chelem.Unknown,
                        Tuple.Create(new Player("Charlie", "Parker", "Bird", null), Bidding.Garde),
                        Tuple.Create(new Player("Dizzy", "Gillespie", "Dizz", null), Bidding.Opponent),
                        Tuple.Create(new Player("Stan", "Getz", "", null), Bidding.Opponent),
                        Tuple.Create(new Player("Sonny", "Stitt", "", null), Bidding.Opponent)),
                    Tuple.Create(new Player("Charlie", "Parker", "Bird", null), 318),
                    Tuple.Create(new Player("Dizzy", "Gillespie", "Dizz", null), -106),
                    Tuple.Create(new Player("Stan", "Getz", "", null), -106),
                    Tuple.Create(new Player("Sonny", "Stitt", "", null), -106),
                };
                //valid 4 players game
                yield return new object[]
                {
                    new Game(DateTime.Now, new FrenchTarotRules(), 45, PetitResult.LostAuBout, Poignée.None, true, true, Chelem.Unknown,
                        Tuple.Create(new Player("Charlie", "Parker", "Bird", null), Bidding.GardeSans),
                        Tuple.Create(new Player("Dizzy", "Gillespie", "Dizz", null), Bidding.Opponent),
                        Tuple.Create(new Player("Stan", "Getz", "", null), Bidding.Opponent),
                        Tuple.Create(new Player("Sonny", "Stitt", "", null), Bidding.Opponent)),
                    Tuple.Create(new Player("Charlie", "Parker", "Bird", null), 228),
                    Tuple.Create(new Player("Dizzy", "Gillespie", "Dizz", null), -76),
                    Tuple.Create(new Player("Stan", "Getz", "", null), -76),
                    Tuple.Create(new Player("Sonny", "Stitt", "", null), -76),
                };
                //valid 4 players game
                yield return new object[]
                {
                    new Game(DateTime.Now, new FrenchTarotRules(), 44, PetitResult.SavedAuBout, Poignée.Simple, false, false, Chelem.Unknown,
                        Tuple.Create(new Player("Charlie", "Parker", "Bird", null), Bidding.Petite),
                        Tuple.Create(new Player("Dizzy", "Gillespie", "Dizz", null), Bidding.Opponent),
                        Tuple.Create(new Player("Stan", "Getz", "", null), Bidding.Opponent),
                        Tuple.Create(new Player("Sonny", "Stitt", "", null), Bidding.Opponent)),
                    Tuple.Create(new Player("Charlie", "Parker", "Bird", null), -126),
                    Tuple.Create(new Player("Dizzy", "Gillespie", "Dizz", null), 42),
                    Tuple.Create(new Player("Stan", "Getz", "", null), 42),
                    Tuple.Create(new Player("Sonny", "Stitt", "", null), 42),
                };
                //valid 4 players game
                yield return new object[]
                {
                    new Game(DateTime.Now, new FrenchTarotRules(), 52, PetitResult.Saved, Poignée.SimpleDefense, true, false, Chelem.Unknown,
                        Tuple.Create(new Player("Charlie", "Parker", "Bird", null), Bidding.Garde),
                        Tuple.Create(new Player("Dizzy", "Gillespie", "Dizz", null), Bidding.Opponent),
                        Tuple.Create(new Player("Stan", "Getz", "", null), Bidding.Opponent),
                        Tuple.Create(new Player("Sonny", "Stitt", "", null), Bidding.Opponent)),
                    Tuple.Create(new Player("Charlie", "Parker", "Bird", null), 276),
                    Tuple.Create(new Player("Dizzy", "Gillespie", "Dizz", null), -92),
                    Tuple.Create(new Player("Stan", "Getz", "", null), -92),
                    Tuple.Create(new Player("Sonny", "Stitt", "", null), -92),
                };
                //valid 4 players game
                yield return new object[]
                {
                    new Game(DateTime.Now, new FrenchTarotRules(), 87, PetitResult.SavedAuBout, Poignée.Simple, false, true, Chelem.AnnouncedSuccess,
                        Tuple.Create(new Player("Charlie", "Parker", "Bird", null), Bidding.Garde),
                        Tuple.Create(new Player("Dizzy", "Gillespie", "Dizz", null), Bidding.Opponent),
                        Tuple.Create(new Player("Stan", "Getz", "", null), Bidding.Opponent),
                        Tuple.Create(new Player("Sonny", "Stitt", "", null), Bidding.Opponent)),
                    Tuple.Create(new Player("Charlie", "Parker", "Bird", null), 1746),
                    Tuple.Create(new Player("Dizzy", "Gillespie", "Dizz", null), -582),
                    Tuple.Create(new Player("Stan", "Getz", "", null), -582),
                    Tuple.Create(new Player("Sonny", "Stitt", "", null), -582),
                };
            }
        }
    }

    

    public class FrenchTarotRules_UT
    {
        [Theory]
        [MemberData(nameof(TestData_Rules.Games), MemberType = typeof(TestData_Rules))]
        public void TestCheckIfValid(bool expectedIsValid, Validity expectedValidity, Game game)
        {
            Assert.Equal(expectedIsValid, game.CheckValid(out Validity validity));
            Assert.Equal(expectedValidity, validity);
        }

        [Theory]
        [MemberData(nameof(TestData_Rules.Scores), MemberType = typeof(TestData_Rules))]
        public void TestScores(Game game,
                               params Tuple<Player, int>[] expectedScores)
        {
            var scores = game.Scores;
            Assert.Equal(expectedScores.Length, scores.Count);
            foreach(var score in expectedScores)
            {
                Assert.Contains(score.Item1, scores);
                Assert.Equal(score.Item2, scores[score.Item1]);
            }
        }
    }
}
