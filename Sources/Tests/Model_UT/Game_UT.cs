using System;
using System.Collections.Generic;
using System.Linq;
using Model;
using Xunit;

namespace Model_UT
{
    public class TestData_Results
    {
        //public static IEnumerable<object[]> Results
        //{
        //    get
        //    {
        //        //valid 3 players game won by the taker
        //        yield return new object[]
        //        {
        //            DateTime.Now,
        //            true,
        //            Validity.Valid,
        //            new Result(new Player("Charlie", "Parker", "Bird", null), 42, Bidding.Pousse, PetitResult.Unknown, Poignée.Unknown, true, false),
        //            new Result(new Player("Dizzy", "Gillespie", "Dizz", null), -21, Bidding.Opponent, PetitResult.Unknown, Poignée.Unknown, false, true),
        //            new Result(new Player("Stan", "Getz", "", null), -21, Bidding.Opponent, PetitResult.Unknown, Poignée.Unknown, false, true),
        //        };
        //        //valid 3 players game lost by the taker
        //        yield return new object[]
        //        {
        //            DateTime.Now,
        //            true,
        //            Validity.Valid,
        //            new Result(new Player("Charlie", "Parker", "Bird", null), -42, Bidding.Pousse, PetitResult.Unknown, Poignée.Unknown, true, false),
        //            new Result(new Player("Dizzy", "Gillespie", "Dizz", null), 21, Bidding.Opponent, PetitResult.Unknown, Poignée.Unknown, false, true),
        //            new Result(new Player("Stan", "Getz", "", null), 21, Bidding.Opponent, PetitResult.Unknown, Poignée.Unknown, false, true),
        //        };
        //        //valid 4 players game won by the taker
        //        yield return new object[]
        //        {
        //            DateTime.Now,
        //            true,
        //            Validity.Valid,
        //            new Result(new Player("Charlie", "Parker", "Bird", null), 63, Bidding.Pousse, PetitResult.Unknown, Poignée.Unknown, true, false),
        //            new Result(new Player("Dizzy", "Gillespie", "Dizz", null), -21, Bidding.Opponent, PetitResult.Unknown, Poignée.Unknown, false, true),
        //            new Result(new Player("Stan", "Getz", "", null), -21, Bidding.Opponent, PetitResult.Unknown, Poignée.Unknown, false, true),
        //            new Result(new Player("Sonny", "Stitt", "", null), -21, Bidding.Opponent, PetitResult.Unknown, Poignée.Unknown, false, true),
        //        };
        //        //valid 4 players game lost by the taker
        //        yield return new object[]
        //        {
        //            DateTime.Now,
        //            true,
        //            Validity.Valid,
        //            new Result(new Player("Charlie", "Parker", "Bird", null), -63, Bidding.Pousse, PetitResult.Unknown, Poignée.Unknown, true, false),
        //            new Result(new Player("Dizzy", "Gillespie", "Dizz", null), 21, Bidding.Opponent, PetitResult.Unknown, Poignée.Unknown, false, true),
        //            new Result(new Player("Stan", "Getz", "", null), 21, Bidding.Opponent, PetitResult.Unknown, Poignée.Unknown, false, true),
        //            new Result(new Player("Sonny", "Stitt", "", null), 21, Bidding.Opponent, PetitResult.Unknown, Poignée.Unknown, false, true),
        //        };
        //        //valid 5 players game (2 vs 3) won by the taker
        //        yield return new object[]
        //        {
        //            DateTime.Now,
        //            true,
        //            Validity.Valid,
        //            new Result(new Player("Charlie", "Parker", "Bird", null), 42, Bidding.Pousse, PetitResult.Unknown, Poignée.Unknown, true, false),
        //            new Result(new Player("Dizzy", "Gillespie", "Dizz", null), 21, Bidding.KingCalled, PetitResult.Unknown, Poignée.Unknown, true, false),
        //            new Result(new Player("Stan", "Getz", "", null), -21, Bidding.Opponent, PetitResult.Unknown, Poignée.Unknown, false, true),
        //            new Result(new Player("Sonny", "Stitt", "", null), -21, Bidding.Opponent, PetitResult.Unknown, Poignée.Unknown, false, true),
        //            new Result(new Player("Sonny", "Rollins", "", null), -21, Bidding.Opponent, PetitResult.Unknown, Poignée.Unknown, false, true),
        //        };
        //        //valid 5 players game (2 vs 3) lost by the taker
        //        yield return new object[]
        //        {
        //            DateTime.Now,
        //            true,
        //            Validity.Valid,
        //            new Result(new Player("Charlie", "Parker", "Bird", null), -42, Bidding.Pousse, PetitResult.Unknown, Poignée.Unknown, true, false),
        //            new Result(new Player("Dizzy", "Gillespie", "Dizz", null), -21, Bidding.KingCalled, PetitResult.Unknown, Poignée.Unknown, true, false),
        //            new Result(new Player("Stan", "Getz", "", null), 21, Bidding.Opponent, PetitResult.Unknown, Poignée.Unknown, false, true),
        //            new Result(new Player("Sonny", "Stitt", "", null), 21, Bidding.Opponent, PetitResult.Unknown, Poignée.Unknown, false, true),
        //            new Result(new Player("Sonny", "Rollins", "", null), 21, Bidding.Opponent, PetitResult.Unknown, Poignée.Unknown, false, true),
        //        };
        //        //valid 5 players game (1 vs 4) won by the taker
        //        yield return new object[]
        //        {
        //            DateTime.Now,
        //            true,
        //            Validity.Valid,
        //            new Result(new Player("Charlie", "Parker", "Bird", null), 84, Bidding.Pousse, PetitResult.Unknown, Poignée.Unknown, true, false),
        //            new Result(new Player("Dizzy", "Gillespie", "Dizz", null), -21, Bidding.Opponent, PetitResult.Unknown, Poignée.Unknown, false, true),
        //            new Result(new Player("Stan", "Getz", "", null), -21, Bidding.Opponent, PetitResult.Unknown, Poignée.Unknown, false, true),
        //            new Result(new Player("Sonny", "Stitt", "", null), -21, Bidding.Opponent, PetitResult.Unknown, Poignée.Unknown, false, true),
        //            new Result(new Player("Sonny", "Rollins", "", null), -21, Bidding.Opponent, PetitResult.Unknown, Poignée.Unknown, false, true),
        //        };
        //        //valid 5 players game (1 vs 4) lost by the taker
        //        yield return new object[]
        //        {
        //            DateTime.Now,
        //            true,
        //            Validity.Valid,
        //            new Result(new Player("Charlie", "Parker", "Bird", null), -84, Bidding.Pousse, PetitResult.Unknown, Poignée.Unknown, true, false),
        //            new Result(new Player("Dizzy", "Gillespie", "Dizz", null), 21, Bidding.Opponent, PetitResult.Unknown, Poignée.Unknown, false, true),
        //            new Result(new Player("Stan", "Getz", "", null), 21, Bidding.Opponent, PetitResult.Unknown, Poignée.Unknown, false, true),
        //            new Result(new Player("Sonny", "Stitt", "", null), 21, Bidding.Opponent, PetitResult.Unknown, Poignée.Unknown, false, true),
        //            new Result(new Player("Sonny", "Rollins", "", null), 21, Bidding.Opponent, PetitResult.Unknown, Poignée.Unknown, false, true),
        //        };
        //    }
        //}
    }

    public class TestData_Game
    {
        public static IEnumerable<object[]> DataConstructor
        {
            get
            {
                yield return new object[]
                {
                    0,
                    42,
                    DateTime.Now, new FrenchTarotRules(),
                    0,
                    PetitResult.Unknown, Poignée.Unknown, false, false, Chelem.Unknown,
                };

                yield return new object[]
                {
                    1,
                    42,
                    DateTime.Now, new FrenchTarotRules(),
                    42,
                    PetitResult.Unknown, Poignée.Unknown, true, false, Chelem.Announced,
                    new Tuple<Player, Bidding>(new Player("Charlie", "Parker", "Bird", null), Bidding.Pousse),
                };

                yield return new object[]
                {
                    2,
                    42,
                    DateTime.Now, new FrenchTarotRules(),
                    42,
                    PetitResult.Unknown, Poignée.Unknown, true, false, Chelem.AnnouncedFail,
                    new Tuple<Player, Bidding>(new Player("Charlie", "Parker", "Bird", null), Bidding.Pousse),
                    new Tuple<Player, Bidding>(new Player("Dizzy", "Gillespie", "Dizz", null), Bidding.Opponent),
                };

                yield return new object[]
                {
                    3,
                    42,
                    DateTime.Now, new FrenchTarotRules(),
                    42,
                    PetitResult.Unknown, Poignée.Unknown, true, false, Chelem.AnnouncedSuccess,
                    new Tuple<Player, Bidding>(new Player("Charlie", "Parker", "Bird", null), Bidding.Pousse),
                    new Tuple<Player, Bidding>(new Player("Dizzy", "Gillespie", "Dizz", null), Bidding.Opponent),
                    new Tuple<Player, Bidding>(new Player("Stan", "Getz", "", null), Bidding.Opponent),
                };

                yield return new object[]
                {
                    4,
                    42,
                    DateTime.Now, new FrenchTarotRules(),
                    -63,
                    PetitResult.Unknown, Poignée.Unknown, true, false, Chelem.Fail,
                    new Tuple<Player, Bidding>(new Player("Charlie", "Parker", "Bird", null), Bidding.Pousse),
                    new Tuple<Player, Bidding>(new Player("Dizzy", "Gillespie", "Dizz", null), Bidding.Opponent),
                    new Tuple<Player, Bidding>(new Player("Stan", "Getz", "", null), Bidding.Opponent),
                    new Tuple<Player, Bidding>(new Player("Sonny", "Stitt", "", null), Bidding.Opponent),
                };

                yield return new object[]
                {
                    5,
                    42,
                    DateTime.Now, new FrenchTarotRules(),
                    84,
                    PetitResult.Unknown, Poignée.Unknown, true, false, Chelem.NotAnnouncedSuccess,
                    new Tuple<Player, Bidding>(new Player("Charlie", "Parker", "Bird", null), Bidding.Pousse),
                    new Tuple<Player, Bidding>(new Player("Dizzy", "Gillespie", "Dizz", null), Bidding.Opponent),
                    new Tuple<Player, Bidding>(new Player("Stan", "Getz", "", null), Bidding.Opponent),
                    new Tuple<Player, Bidding>(new Player("Sonny", "Stitt", "", null), Bidding.Opponent),
                    new Tuple<Player, Bidding>(new Player("Sonny", "Rollins", "", null), Bidding.Opponent),
                };

                yield return new object[]
                {
                    6,
                    42,
                    DateTime.Now, new FrenchTarotRules(),
                    84,
                    PetitResult.Unknown, Poignée.Unknown, true, false, Chelem.Success,
                    new Tuple<Player, Bidding>(new Player("Charlie", "Parker", "Bird", null), Bidding.Pousse),
                    new Tuple<Player, Bidding>(new Player("Dizzy", "Gillespie", "Dizz", null), Bidding.Opponent),
                    new Tuple<Player, Bidding>(new Player("Stan", "Getz", "", null), Bidding.Opponent),
                    new Tuple<Player, Bidding>(new Player("Sonny", "Stitt", "", null), Bidding.Opponent),
                    new Tuple<Player, Bidding>(new Player("Sonny", "Rollins", "", null), Bidding.Opponent),
                    new Tuple<Player, Bidding>(new Player("Miles", "Davis", "", null), Bidding.Opponent),
                };

                yield return new object[]
                {
                    3,
                    42,
                    DateTime.Now, new FrenchTarotRules(),
                    -63,
                    PetitResult.Unknown, Poignée.Unknown, true, false, Chelem.Unknown,
                    new Tuple<Player, Bidding>(new Player("Charlie", "Parker", "Bird", null), Bidding.Pousse),
                    new Tuple<Player, Bidding>(new Player("Dizzy", "Gillespie", "Dizz", null), Bidding.Opponent),
                    new Tuple<Player, Bidding>(new Player("Dizzy", "Gillespie", "Dizz", null), Bidding.Opponent),
                    new Tuple<Player, Bidding>(new Player("Stan", "Getz", "", null), Bidding.Opponent),
                };

                yield return new object[]
                {
                    0,
                    42,
                    DateTime.Now, new FrenchTarotRules(),
                    84,
                    PetitResult.Unknown, Poignée.Unknown, true, false, Chelem.Success,
                    new Tuple<Player, Bidding>(new Player("Charlie", "Parker", "Bird", null), Bidding.Pousse),
                    new Tuple<Player, Bidding>(null, Bidding.Opponent),
                    new Tuple<Player, Bidding>(new Player("Stan", "Getz", "", null), Bidding.Opponent),
                };
            }
        }

        public static IEnumerable<object[]> DataAddPlayer
        {
            get
            {
                yield return new object[]
                {
                    new Game(DateTime.Now, new FrenchTarotRules(), -63, PetitResult.Unknown, Poignée.Unknown, true, false, Chelem.Unknown, new Tuple<Player, Bidding>(new Player("Charlie", "Parker", "Bird", null), Bidding.Pousse)),
                    false,
                    1,
                    null, Bidding.None
                };
                yield return new object[]
                {
                    new Game(DateTime.Now, new FrenchTarotRules(), - 63, PetitResult.Unknown, Poignée.Unknown, true, false, Chelem.Unknown, new Tuple<Player, Bidding>(new Player("Charlie", "Parker", "Bird", null), Bidding.Pousse)),
                    true,
                    1,
                    new Player("Charlie", "Parker", "Bird", null), Bidding.Garde,
                };
                yield return new object[]
                {
                    new Game(DateTime.Now, new FrenchTarotRules(), -63, PetitResult.Unknown, Poignée.Unknown, true, false, Chelem.Unknown, new Tuple<Player, Bidding>(new Player("Charlie", "Parker", "Bird", null), Bidding.Pousse)),
                    true,
                    2,
                    new Player("Dizzy", "Gillespie", "Dizz", null), Bidding.Opponent
                };
            }
        }

        public static IEnumerable<object[]> DataAddExistingPlayer
        {
            get
            {
                yield return new object[]
                {
                    new Game(DateTime.Now, new FrenchTarotRules(), -63, PetitResult.Unknown, Poignée.Unknown, true, false, Chelem.Unknown),
                    new Player("Charlie", "Parker", "Bird", null), Bidding.Pousse,
                    new Player("Charlie", "Parker", "Bird", null), Bidding.Pousse,
                    1
                };
                yield return new object[]
                {
                    new Game(DateTime.Now, new FrenchTarotRules(), -63, PetitResult.Unknown, Poignée.Unknown, true, false, Chelem.Unknown),
                    new Player("Charlie", "Parker", "Bird", null), Bidding.Pousse,
                    new Player("Charlie", "Parker", "Bird", null), Bidding.Pousse,
                    1
                };
                yield return new object[]
                {
                    new Game(DateTime.Now, new FrenchTarotRules(), -63, PetitResult.Unknown, Poignée.Unknown, true, false, Chelem.Unknown),
                    new Player("Charlie", "Parker", "Bird", null), Bidding.Pousse,
                    new Player("Dizzy", "Gillespie", "Dizz", null), Bidding.Pousse,
                    2
                };
            }
        }

        public static IEnumerable<object[]> DataAddPlayers
        {
            get
            {
                yield return new object[]
                {
                    new Game(DateTime.Now, new FrenchTarotRules(), -63, PetitResult.Unknown, Poignée.Unknown, true, false, Chelem.Unknown),
                    false,
                    0,
                };

                yield return new object[]
                {
                    new Game(DateTime.Now, new FrenchTarotRules(), -63, PetitResult.Unknown, Poignée.Unknown, true, false, Chelem.Unknown),
                    false,
                    0,
                    Tuple.Create<Player, Bidding>(null, Bidding.Opponent)
                };

                yield return new object[]
                {
                    new Game(DateTime.Now, new FrenchTarotRules(), -63, PetitResult.Unknown, Poignée.Unknown, true, false, Chelem.Unknown),
                    false,
                    0,
                    Tuple.Create(new Player("Dizzy", "Gillespie", "Dizz", null), Bidding.Opponent),
                    Tuple.Create<Player, Bidding>(null, Bidding.Opponent)
                };

                yield return new object[]
                {
                    new Game(DateTime.Now, new FrenchTarotRules(), -63, PetitResult.Unknown, Poignée.Unknown, true, false, Chelem.Unknown),
                    true,
                    1,
                    Tuple.Create(new Player("Dizzy", "Gillespie", "Dizz", null), Bidding.Opponent)
                };
                yield return new object[]
                {
                    new Game(DateTime.Now, new FrenchTarotRules(), -63, PetitResult.Unknown, Poignée.Unknown, true, false, Chelem.Unknown),
                    true,
                    1,
                    Tuple.Create(new Player("Dizzy", "Gillespie", "Dizz", null), Bidding.Opponent),
                    Tuple.Create(new Player("Dizzy", "Gillespie", "Dizz", null), Bidding.Opponent)
                };
                yield return new object[]
                {
                    new Game(DateTime.Now, new FrenchTarotRules(), -63, PetitResult.Unknown, Poignée.Unknown, true, false, Chelem.Unknown),
                    true,
                    2,
                    Tuple.Create(new Player("Dizzy", "Gillespie", "Dizz", null), Bidding.Opponent),
                    Tuple.Create(new Player("Stan", "Getz", "", null), Bidding.Opponent)
                };
            }
        }

        public static IEnumerable<object[]> DataRemovePlayer
        {
            get
            {
                yield return new object[]
                {
                    new Game(DateTime.Now, new FrenchTarotRules(), 21, PetitResult.Unknown, Poignée.Unknown, true, false, Chelem.Unknown,
                        Tuple.Create(new Player("Charlie", "Parker", "Bird", null), Bidding.Pousse),
                        Tuple.Create(new Player("Dizzy", "Gillespie", "Dizz", null), Bidding.Opponent),
                        Tuple.Create(new Player("Stan", "Getz", "", null), Bidding.Opponent),
                        Tuple.Create(new Player("Sonny", "Stitt", "", null), Bidding.Opponent),
                        Tuple.Create(new Player("Sonny", "Rollins", "", null), Bidding.Opponent)),
                    new Player("Charlie", "Parker", "Bird", null),
                    true,
                    4
                };
                yield return new object[]
                {
                    new Game(DateTime.Now, new FrenchTarotRules(), 21, PetitResult.Unknown, Poignée.Unknown, true, false, Chelem.Unknown,
                        Tuple.Create(new Player("Charlie", "Parker", "Bird", null), Bidding.Pousse),
                        Tuple.Create(new Player("Dizzy", "Gillespie", "Dizz", null), Bidding.Opponent),
                        Tuple.Create(new Player("Stan", "Getz", "", null), Bidding.Opponent),
                        Tuple.Create(new Player("Sonny", "Stitt", "", null), Bidding.Opponent),
                        Tuple.Create(new Player("Sonny", "Rollins", "", null), Bidding.Opponent)),
                    new Player("Stan", "Getz", "", null),
                    true,
                    4
                };
                yield return new object[]
                {
                    new Game(DateTime.Now, new FrenchTarotRules(), 21, PetitResult.Unknown, Poignée.Unknown, true, false, Chelem.Unknown,
                        Tuple.Create(new Player("Charlie", "Parker", "Bird", null), Bidding.Pousse),
                        Tuple.Create(new Player("Dizzy", "Gillespie", "Dizz", null), Bidding.Opponent),
                        Tuple.Create(new Player("Stan", "Getz", "", null), Bidding.Opponent),
                        Tuple.Create(new Player("Sonny", "Stitt", "", null), Bidding.Opponent),
                        Tuple.Create(new Player("Sonny", "Rollins", "", null), Bidding.Opponent)),
                    new Player("Sonny", "Rollins", "", null),
                    true,
                    4
                };
                yield return new object[]
                {
                    new Game(DateTime.Now, new FrenchTarotRules(), 21, PetitResult.Unknown, Poignée.Unknown, true, false, Chelem.Unknown,
                        Tuple.Create(new Player("Charlie", "Parker", "Bird", null), Bidding.Pousse),
                        Tuple.Create(new Player("Dizzy", "Gillespie", "Dizz", null), Bidding.Opponent),
                        Tuple.Create(new Player("Sonny", "Stitt", "", null), Bidding.Opponent),
                        Tuple.Create(new Player("Sonny", "Rollins", "", null), Bidding.Opponent)),
                    new Player("Stan", "Getz", "", null),
                    false,
                    4
                };
                yield return new object[]
                {
                    new Game(DateTime.Now, new FrenchTarotRules(), 21, PetitResult.Unknown, Poignée.Unknown, true, false, Chelem.Unknown),
                    new Player("Stan", "Getz", "", null),
                    false,
                    0
                };
            }
        }

        public static IEnumerable<object[]> DataRemovePlayers
        {
            get
            {
                yield return new object[]
                {
                    new Game(DateTime.Now, new FrenchTarotRules(), 21, PetitResult.Unknown, Poignée.Unknown, true, false, Chelem.Unknown,
                        Tuple.Create(new Player("Charlie", "Parker", "Bird", null), Bidding.Pousse),
                        Tuple.Create(new Player("Dizzy", "Gillespie", "Dizz", null), Bidding.Opponent),
                        Tuple.Create(new Player("Stan", "Getz", "", null), Bidding.Opponent),
                        Tuple.Create(new Player("Sonny", "Stitt", "", null), Bidding.Opponent),
                        Tuple.Create(new Player("Sonny", "Rollins", "", null), Bidding.Opponent)),
                    0
                };
                yield return new object[]
                {
                    new Game(DateTime.Now, new FrenchTarotRules(), 21, PetitResult.Unknown, Poignée.Unknown, true, false, Chelem.Unknown),
                    0
                };
            }
        }
    }

    public class Game_UT
    {
        //constructor
        [Theory]
        [MemberData(nameof(TestData_Game.DataConstructor), MemberType = typeof(TestData_Game))]
        public void TestConstructor(int expectedAddedPlayers, long id, DateTime date, IRules rules,
                                    int takerPoints, PetitResult petitResult, Poignée poignée,
                                    bool? excuse, bool? twentyOne, Chelem chelem,
                                    params Tuple<Player, Bidding>[] players)
        {
            Game game = new Game(id, date, rules, takerPoints, petitResult, poignée, excuse, twentyOne, chelem, players);
            Game game2 = new Game(date, rules, takerPoints, petitResult, poignée, excuse, twentyOne, chelem, players);
            Assert.Equal(id, game.Id);
            Assert.Equal(0, game2.Id);
            Assert.Equal(date, game.Date);
            Assert.Equal(date, game2.Date);
            Assert.Equal(rules.GetType(), game.Rules.GetType());
            Assert.Equal(rules.GetType(), game2.Rules.GetType());
            Assert.Equal(takerPoints, game.TakerPoints);
            Assert.Equal(takerPoints, game2.TakerPoints);
            Assert.Equal(petitResult, game.PetitResult);
            Assert.Equal(petitResult, game2.PetitResult);
            Assert.Equal(poignée, game.Poignée);
            Assert.Equal(poignée, game2.Poignée);
            Assert.Equal(excuse, game.Excuse);
            Assert.Equal(excuse, game2.Excuse);
            Assert.Equal(twentyOne, game.TwentyOne);
            Assert.Equal(twentyOne, game2.TwentyOne);
            Assert.Equal(chelem, game.Chelem);
            Assert.Equal(chelem, game2.Chelem);
            Assert.Equal(expectedAddedPlayers, game.Players.Count);
        }

        //AddResult
        [Theory]
        [MemberData(nameof(TestData_Game.DataAddPlayer), MemberType = typeof(TestData_Game))]
        public void TestAddPlayer(Game game, bool expectedAddPlayer, int expectedNbPlayers, Player player, Bidding bidding)
        {
            bool isAddedPlayer = game.AddPlayer(player, bidding);
            Assert.Equal(expectedAddPlayer, isAddedPlayer);
            Assert.Equal(expectedNbPlayers, game.Players.Count);
        }

        //Add existing player
        [Theory]
        [MemberData(nameof(TestData_Game.DataAddExistingPlayer), MemberType = typeof(TestData_Game))]
        public void TestAddExistingPlayer(Game game, Player firstPlayer, Bidding firstBidding,
                                                    Player playerToAdd, Bidding biddingToAdd,
                                                    int expectedNbOfPlayers)
        {
            game.AddPlayer(firstPlayer, firstBidding);
            game.AddPlayer(playerToAdd, biddingToAdd);
            Assert.Equal(expectedNbOfPlayers, game.Players.Count);
            Assert.Equal(biddingToAdd, game.Players[playerToAdd]);
        }

        //AddResults
        [Theory]
        [MemberData(nameof(TestData_Game.DataAddPlayers), MemberType = typeof(TestData_Game))]
        public void TestAddPlayers(Game game, bool expectedAreAddedPlayers, int expectedNbPlayers, params Tuple<Player, Bidding>[] players)
        {
            bool areAddedPlayers = game.AddPlayers(players);
            Assert.Equal(expectedAreAddedPlayers, areAddedPlayers);
            Assert.Equal(expectedNbPlayers, game.Players.Count);
        }

        //RemoveResult
        [Theory]
        [MemberData(nameof(TestData_Game.DataRemovePlayer), MemberType = typeof(TestData_Game))]
        public void TestRemovePlayer(Game game, Player playerToRemove, bool expectedReturn, int expectedNbOfPlayers)
        {
            bool removeResult = game.RemovePlayer(playerToRemove);
            Assert.Equal(expectedReturn, removeResult);
            Assert.Equal(expectedNbOfPlayers, game.Players.Count);
        }

        //RemoveAllResults
        [Theory]
        [MemberData(nameof(TestData_Game.DataRemovePlayers), MemberType = typeof(TestData_Game))]
        public void TestRemoveResults(Game game, int expectedNbOfPlayers)
        {
            game.RemoveAllPlayers();
            Assert.Equal(expectedNbOfPlayers, game.Players.Count);
        }
    }
}
