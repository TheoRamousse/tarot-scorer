using System;
using System.Collections.Generic;
using System.Linq;
using Model;
using Xunit;

namespace Model_UT
{
    public class TestData_Ranking
    {
        public static IEnumerable<PlayerData> PlayerDatas = new List<PlayerData>()
        {
            new PlayerData
            {
                Player = new Player("Lenny", "White", null, null),
                NbPoints = 318,     //75%
                NbVictories = 3,
                NbLosses = 1
            },
            new PlayerData
            {
                Player = new Player("Chick", "Corea", null, null),
                NbPoints = 1054,    //100%
                NbVictories = 8,
                NbLosses = 0
            },
            new PlayerData
            {
                Player = new Player("Stanley", "Clarke", null, null),
                NbPoints = -87,     //30%
                NbVictories = 3,
                NbLosses = 7
            },
            new PlayerData
            {
                Player = new Player("Jean-Luc", "Ponty", null, null),
                NbPoints = -179,    //10%
                NbVictories = 1,
                NbLosses = 3
            },
            new PlayerData
            {
                Player = new Player("Steve", "Gadd", null, null),
                NbPoints = 0,       //56%
                NbVictories = 4,
                NbLosses = 3
            }
        };

        public static Ranking EmptyRanking => new Ranking("Super ranking", null, null);

        public static Ranking OneDataRanking => new Ranking("Super ranking", null, null, GameType.All,
            new PlayerData
            {
                Player = new Player("Lenny", "White", null, null),
                NbPoints = 318,
                NbVictories = 3,
                NbLosses = 1
            });

        public static Ranking FullDataRanking => new Ranking("Super ranking", null, null,
                                                            GameType.All, PlayerDatas.ToArray());

        public static IEnumerable<object[]> Constructors
        {
            get
            {
                yield return new object[]
                {
                    "Super Ranking", null, null, GameType.All, false,
                    "Super Ranking", null, null, GameType.All,
                    PlayerDatas.ToArray()
                };
                //empty name
                yield return new object[]
                {
                    "no name", null, null, GameType.All, false,
                    "", null, null, GameType.All,
                    PlayerDatas.ToArray()
                };
                //white name
                yield return new object[]
                {
                    "no name", null, null, GameType.All, false,
                    "     ", null, null, GameType.All,
                    PlayerDatas.ToArray()
                };
                //null name
                yield return new object[]
                {
                    "no name", null, null, GameType.All, false,
                    null, null, null, GameType.All,
                    PlayerDatas.ToArray()
                };
                yield return new object[]
                {
                    "Super Ranking", new DateTime(1975, 8, 5), null, GameType.All, false,
                    "Super Ranking", new DateTime(1975, 8, 5), null, GameType.All,
                    PlayerDatas.ToArray()
                };
                yield return new object[]
                {
                    "Super Ranking", null, new DateTime(1975, 8, 5), GameType.All, false,
                    "Super Ranking", null, new DateTime(1975, 8, 5), GameType.All,
                    PlayerDatas.ToArray()
                };
                yield return new object[]
                {
                    "Super Ranking", new DateTime(1975, 8, 5), new DateTime(1975, 8, 5), GameType.All, true,
                    "Super Ranking", new DateTime(1975, 8, 5), new DateTime(1975, 8, 5), GameType.All,
                    PlayerDatas.ToArray()
                };
                yield return new object[]
                {
                    "Super Ranking", new DateTime(1976, 8, 5), new DateTime(1975, 8, 5), GameType.All, true,
                    "Super Ranking", new DateTime(1976, 8, 5), new DateTime(1975, 8, 5), GameType.All,
                    PlayerDatas.ToArray()
                };
                yield return new object[]
                {
                    "Super Ranking", new DateTime(1974, 8, 5), new DateTime(1975, 8, 5), GameType.All, false,
                    "Super Ranking", new DateTime(1974, 8, 5), new DateTime(1975, 8, 5), GameType.All,
                    PlayerDatas.ToArray()
                };
                yield return new object[]
                {
                    "Super Ranking", new DateTime(1974, 8, 5), new DateTime(1975, 8, 5), GameType.ThreePlayers, false,
                    "Super Ranking", new DateTime(1974, 8, 5), new DateTime(1975, 8, 5), GameType.ThreePlayers,
                    PlayerDatas.ToArray()
                };
                yield return new object[]
                {
                    "Super Ranking", new DateTime(1974, 8, 5), new DateTime(1975, 8, 5), GameType.ThreePlayers, false,
                    "Super Ranking", new DateTime(1974, 8, 5), new DateTime(1975, 8, 5), GameType.ThreePlayers,
                };
                yield return new object[]
                {
                    "Super Ranking", new DateTime(1974, 8, 5), new DateTime(1975, 8, 5), GameType.ThreePlayers, false,
                    "Super Ranking", new DateTime(1974, 8, 5), new DateTime(1975, 8, 5), GameType.ThreePlayers,
                    new PlayerData
                    {
                        Player = new Player("Steve", "Gadd", null, null),
                        NbPoints = 0,
                        NbVictories = 4,
                        NbLosses = 3
                    }
                };
            }
        }

        public static IEnumerable<object[]> AddPlayerDatas
        {
            get
            {
                //adds data in an empty ranking
                yield return new object[]
                {
                    true, 1,
                    EmptyRanking,
                    new PlayerData
                    {
                        Player = new Player("Stanley", "Clarke", null, null),
                        NbPoints = -87,
                        NbVictories = 3,
                        NbLosses = 7
                    },
                };
                //adds data in an one data ranking
                yield return new object[]
                {
                    true, 2,
                    OneDataRanking,
                    new PlayerData
                    {
                        Player = new Player("Stanley", "Clarke", null, null),
                        NbPoints = -87,
                        NbVictories = 3,
                        NbLosses = 7
                    },
                };
                //adds a non-existing data in a full data ranking
                yield return new object[]
                {
                    true, 6,
                    FullDataRanking,
                    new PlayerData
                    {
                        Player = new Player("Tony", "Williams", null, null),
                        NbPoints = -87,
                        NbVictories = 3,
                        NbLosses = 7
                    },
                };
                //adds existing data in an one data ranking
                yield return new object[]
                {
                    false, 1,
                    OneDataRanking,
                    new PlayerData
                    {
                        Player = new Player("Lenny", "White", null, null),
                        NbPoints = -87,
                        NbVictories = 3,
                        NbLosses = 7
                    },
                };
                //adds an existing data in a full data ranking
                yield return new object[]
                {
                    false, 5,
                    FullDataRanking,
                    new PlayerData
                    {
                        Player = new Player("Lenny", "White", null, null),
                        NbPoints = -87,
                        NbVictories = 3,
                        NbLosses = 7
                    },
                };
            }

        }

        public static IEnumerable<object[]> AddPlayersDatas
        {
            get
            {
                //adds nothing in an empty ranking
                yield return new object[]
                {
                    0,
                    EmptyRanking,
                };
                //adds one data in an empty ranking
                yield return new object[]
                {
                    1,
                    EmptyRanking,
                    new PlayerData
                    {
                        Player = new Player("Stanley", "Clarke", null, null),
                        NbPoints = -87,
                        NbVictories = 3,
                        NbLosses = 7
                    },
                };
                //adds data in an empty ranking
                yield return new object[]
                {
                    2,
                    EmptyRanking,
                    new PlayerData
                    {
                        Player = new Player("Stanley", "Clarke", null, null),
                        NbPoints = -87,
                        NbVictories = 3,
                        NbLosses = 7
                    },
                    new PlayerData
                    {
                        Player = new Player("Chick", "Corea", null, null),
                        NbPoints = 1054,
                        NbVictories = 8,
                        NbLosses = 0
                    },
                };
                //adds twice the same data in an empty ranking
                yield return new object[]
                {
                    1,
                    EmptyRanking,
                    new PlayerData
                    {
                        Player = new Player("Stanley", "Clarke", null, null),
                        NbPoints = -87,
                        NbVictories = 3,
                        NbLosses = 7
                    },
                    new PlayerData
                    {
                        Player = new Player("Stanley", "Clarke", null, null),
                        NbPoints = 1054,
                        NbVictories = 8,
                        NbLosses = 0
                    },
                };
                //adds nothing in a one data ranking
                yield return new object[]
                {
                    1,
                    OneDataRanking,
                };
                //adds one data in a one data ranking
                yield return new object[]
                {
                    2,
                    OneDataRanking,
                    new PlayerData
                    {
                        Player = new Player("Stanley", "Clarke", null, null),
                        NbPoints = -87,
                        NbVictories = 3,
                        NbLosses = 7
                    },
                };
                //adds one existing data in a one data ranking
                yield return new object[]
                {
                    1,
                    OneDataRanking,
                    new PlayerData
                    {
                        Player = new Player("Lenny", "White", null, null),
                        NbPoints = 99,
                        NbVictories = 2,
                        NbLosses = 1
                    }
                };
                //adds data in a one data ranking
                yield return new object[]
                {
                    3,
                    OneDataRanking,
                    new PlayerData
                    {
                        Player = new Player("Stanley", "Clarke", null, null),
                        NbPoints = -87,
                        NbVictories = 3,
                        NbLosses = 7
                    },
                    new PlayerData
                    {
                        Player = new Player("Chick", "Corea", null, null),
                        NbPoints = 1054,
                        NbVictories = 8,
                        NbLosses = 0
                    },
                };
                //adds twice the same data in a one data ranking
                yield return new object[]
                {
                    2,
                    OneDataRanking,
                    new PlayerData
                    {
                        Player = new Player("Stanley", "Clarke", null, null),
                        NbPoints = -87,
                        NbVictories = 3,
                        NbLosses = 7
                    },
                    new PlayerData
                    {
                        Player = new Player("Stanley", "Clarke", null, null),
                        NbPoints = 1054,
                        NbVictories = 8,
                        NbLosses = 0
                    },
                };

                //adds nothing in a full data ranking
                yield return new object[]
                {
                    5,
                    FullDataRanking,
                };
                //adds a non-existing data in a full data ranking
                yield return new object[]
                {
                    6,
                    FullDataRanking,
                    new PlayerData
                    {
                        Player = new Player("Tony", "Williams", null, null),
                        NbPoints = -87,
                        NbVictories = 3,
                        NbLosses = 7
                    },
                };
                //adds an existing data in a full data ranking
                yield return new object[]
                {
                    5,
                    FullDataRanking,
                    new PlayerData
                    {
                        Player = new Player("Lenny", "White", null, null),
                        NbPoints = -87,
                        NbVictories = 3,
                        NbLosses = 7
                    },
                };
                //adds two non-existing data in a full data ranking
                yield return new object[]
                {
                    7,
                    FullDataRanking,
                    new PlayerData
                    {
                        Player = new Player("Tony", "Williams", null, null),
                        NbPoints = -87,
                        NbVictories = 3,
                        NbLosses = 7
                    },
                    new PlayerData
                    {
                        Player = new Player("Ron", "Carter", null, null),
                        NbPoints = -87,
                        NbVictories = 3,
                        NbLosses = 7
                    },
                };
                //adds a non-existing data and an existing data in a full data ranking
                yield return new object[]
                {
                    6,
                    FullDataRanking,
                    new PlayerData
                    {
                        Player = new Player("Tony", "Williams", null, null),
                        NbPoints = -87,
                        NbVictories = 3,
                        NbLosses = 7
                    },
                    new PlayerData
                    {
                        Player = new Player("Lenny", "White", null, null),
                        NbPoints = -87,
                        NbVictories = 3,
                        NbLosses = 7
                    },
                };
                //adds two existing data in a full data ranking
                yield return new object[]
                {
                    5,
                    FullDataRanking,
                    new PlayerData
                    {
                        Player = new Player("Chick", "Corea", null, null),
                        NbPoints = -87,
                        NbVictories = 3,
                        NbLosses = 7
                    },
                    new PlayerData
                    {
                        Player = new Player("Lenny", "White", null, null),
                        NbPoints = -87,
                        NbVictories = 3,
                        NbLosses = 7
                    },
                };
                //adds data in a full data ranking
                yield return new object[]
                {
                    7,
                    FullDataRanking,
                    new PlayerData
                    {
                        Player = new Player("Chick", "Corea", null, null),
                        NbPoints = -87,
                        NbVictories = 3,
                        NbLosses = 7
                    },
                    new PlayerData
                    {
                        Player = new Player("Tony", "Williams", null, null),
                        NbPoints = -87,
                        NbVictories = 3,
                        NbLosses = 7
                    },
                    new PlayerData
                    {
                        Player = new Player("Ron", "Carter", null, null),
                        NbPoints = -87,
                        NbVictories = 3,
                        NbLosses = 7
                    },
                    new PlayerData
                    {
                        Player = new Player("Lenny", "White", null, null),
                        NbPoints = -87,
                        NbVictories = 3,
                        NbLosses = 7
                    },
                    new PlayerData
                    {
                        Player = new Player("Tony", "Williams", null, null),
                        NbPoints = -87,
                        NbVictories = 3,
                        NbLosses = 7
                    },
                };
            }

        }

        public static IEnumerable<object[]> RemovePlayerDatas
        {
            get
            {
                //removes data in an empty ranking
                yield return new object[]
                {
                    false, 0,
                    EmptyRanking,
                    new Player("Stanley", "Clarke", null, null),
                };
                //removes an existing data in an one data ranking
                yield return new object[]
                {
                    true, 0,
                    OneDataRanking,
                    new Player("Lenny", "White", null, null),
                };
                //removes a non-existing data in an one data ranking
                yield return new object[]
                {
                    false, 1,
                    OneDataRanking,
                    new Player("Stanley", "Clarke", null, null),
                };
                //removes a non-existing data in a full data ranking
                yield return new object[]
                {
                    false, 5,
                    FullDataRanking,
                    new Player("Tony", "Williams", null, null),
                };
                //removes an existing data in a full data ranking
                yield return new object[]
                {
                    true, 4,
                    FullDataRanking,
                    new Player("Lenny", "White", null, null),
                };
            }

        }

        public static IEnumerable<object[]> UpdatePlayerDatas
        {
            get
            {
                //tries to update a non-existing data in an empty ranking
                yield return new object[]
                {
                    false, 0, null, null, null,
                    EmptyRanking,
                    new PlayerData
                    {
                        Player = new Player("Stanley", "Clarke", null, null),
                        NbPoints = -87,
                        NbVictories = 3,
                        NbLosses = 7
                    },
                };
                //tries to update a non-existing data in an one data ranking
                yield return new object[]
                {
                    false, 1, null, null, null,
                    OneDataRanking,
                    new PlayerData
                    {
                        Player = new Player("Stanley", "Clarke", null, null),
                        NbPoints = -87,
                        NbVictories = 3,
                        NbLosses = 7
                    },
                };
                //tries to update a non-existing data in a full data ranking
                yield return new object[]
                {
                    false, 5, null, null, null,
                    FullDataRanking,
                    new PlayerData
                    {
                        Player = new Player("Tony", "Williams", null, null),
                        NbPoints = -87,
                        NbVictories = 3,
                        NbLosses = 7
                    },
                };
                //updates existing data in an one data ranking
                yield return new object[]
                {
                    true, 1, -87, 3, 7,
                    OneDataRanking,
                    new PlayerData
                    {
                        Player = new Player("Lenny", "White", null, null),
                        NbPoints = -87,
                        NbVictories = 3,
                        NbLosses = 7
                    },
                };
                //updates an existing data in a full data ranking
                yield return new object[]
                {
                    true, 5, 100, 10, 5,
                    FullDataRanking,
                    new PlayerData
                    {
                        Player = new Player("Lenny", "White", null, null),
                        NbPoints = 100,
                        NbVictories = 10,
                        NbLosses = 5
                    },
                };
            }

        }

        public static IEnumerable<object[]> ClearPlayerDatas
        {
            get
            {
                yield return new object[] { EmptyRanking };
                yield return new object[] { OneDataRanking };
                yield return new object[] { FullDataRanking };
            }
        }

        public static IEnumerable<object[]> RankingByDescendingPoints
        {
            get
            {
                yield return new object[]
                {
                    FullDataRanking,
                    new List<int>(){ 1, 0, 4, 2, 3 },
                    new Func<Ranking, IEnumerable<PlayerData>>(
                        (Ranking ranking) => ranking.ByDescendingPoints())
                };
                yield return new object[]
                {
                    FullDataRanking,
                    new List<int>(){ 1, 4, 0, 2, 3 },
                    new Func<Ranking, IEnumerable<PlayerData>>(
                        (Ranking ranking) => ranking.ByDescendingVictories())
                };
                yield return new object[]
                {
                    FullDataRanking,
                    new List<int>(){ 1, 0, 4, 2, 3 },
                    new Func<Ranking, IEnumerable<PlayerData>>(
                        (Ranking ranking) => ranking.ByMeanPointsPerGame())
                };
                yield return new object[]
                {
                    FullDataRanking,
                    new List<int>(){ 1, 0, 4, 2, 3 },
                    new Func<Ranking, IEnumerable<PlayerData>>(
                        (Ranking ranking) => ranking.ByMeanPointsPerGame())
                };
                yield return new object[]
                {
                    FullDataRanking,
                    new List<int>(){ 1, 0, 4, 2, 3 },
                    new Func<Ranking, IEnumerable<PlayerData>>(
                        (Ranking ranking) => ranking.RankPlayersBy(
                            playerdata=>playerdata.OrderByDescending(p => (float)p.NbVictories/(p.NbVictories+p.NbLosses))))
                };
            }
        }
    }

    public class Ranking_UT
    {
        [Theory]
        [MemberData(nameof(TestData_Ranking.Constructors), MemberType=typeof(TestData_Ranking))]
        public void TestConstructor(string expectedName, DateTime? expectedStart, DateTime? expectedEnd, GameType expectedType, bool shouldThrowException,
            string name, DateTime? start, DateTime? end, GameType type, params PlayerData[] datas)
        {
            if(shouldThrowException)
            {
                Assert.Throws<ArgumentException>(() => new Ranking(name, start, end, type, datas));
                Assert.Throws<ArgumentException>(() => new Ranking(name, start, end, type));
                Assert.Throws<ArgumentException>(() => new Ranking(name, start, end));
                return;
            }

            Ranking ranking1 = new Ranking(name, start, end, type, datas);
            Ranking ranking2 = new Ranking(name, start, end, type);
            Ranking ranking3 = new Ranking(name, start, end);

            Assert.Equal(expectedName, ranking1.Name);
            Assert.Equal(expectedName, ranking2.Name);
            Assert.Equal(expectedName, ranking3.Name);

            Assert.Equal(expectedStart, ranking1.StartingTime);
            Assert.Equal(expectedStart, ranking2.StartingTime);
            Assert.Equal(expectedStart, ranking3.StartingTime);

            Assert.Equal(expectedEnd, ranking1.EndingTime);
            Assert.Equal(expectedEnd, ranking2.EndingTime);
            Assert.Equal(expectedEnd, ranking3.EndingTime);

            Assert.Equal(expectedType, ranking1.Type);
            Assert.Equal(expectedType, ranking2.Type);
            Assert.Equal(GameType.All, ranking3.Type);
        }

        [Theory]
        [MemberData(nameof(TestData_Ranking.AddPlayerDatas), MemberType=typeof(TestData_Ranking))]
        public void TestAddPlayerData(bool expectedResult, int expectedNbDatas,
                                      Ranking ranking, PlayerData dataToAdd)
        {
            bool result = ranking.AddPlayerData(dataToAdd);
            Assert.Equal(expectedResult, result);
            Assert.Equal(expectedNbDatas, ranking.PlayerDatas.Count());
            Assert.Contains(dataToAdd, ranking.PlayerDatas);
        }

        [Theory]
        [MemberData(nameof(TestData_Ranking.AddPlayersDatas), MemberType=typeof(TestData_Ranking))]
        public void TestAddPlayersData(int expectedNbDatas,
                                      Ranking ranking, params PlayerData[] dataToAdd)
        {
            ranking.AddPlayersData(dataToAdd);
            Assert.Equal(expectedNbDatas, ranking.PlayerDatas.Count());
            foreach(var data in dataToAdd)
            {
                Assert.Contains(data, ranking.PlayerDatas);
            }
        }

        [Theory]
        [MemberData(nameof(TestData_Ranking.RemovePlayerDatas), MemberType=typeof(TestData_Ranking))]
        public void TestRemovePlayerData(bool expectedResult, int expectedNbDatas,
                                      Ranking ranking, Player dataToRemove)
        {
            bool result = ranking.RemovePlayer(dataToRemove);
            Assert.Equal(expectedResult, result);
            Assert.Equal(expectedNbDatas, ranking.PlayerDatas.Count());
            Assert.DoesNotContain(new PlayerData() {Player = dataToRemove }, ranking.PlayerDatas);
        }

        [Theory]
        [MemberData(nameof(TestData_Ranking.UpdatePlayerDatas), MemberType=typeof(TestData_Ranking))]
        public void TestUpdatePlayerData(bool expectedResult, int expectedNbDatas, int? expectedNbPoints, int? expectedNbVictories, int? expectedNbLosses,
                                      Ranking ranking, PlayerData dataToUpdate)
        {
            bool result = ranking.UpdatePlayerData(dataToUpdate);
            Assert.Equal(expectedResult, result);
            Assert.Equal(expectedNbDatas, ranking.PlayerDatas.Count());
            if(result)
            {
                Assert.Contains(dataToUpdate, ranking.PlayerDatas);
                Assert.Equal(expectedNbPoints.Value, ranking.PlayerDatas.Single(data => data.Equals(dataToUpdate)).NbPoints);
                Assert.Equal(expectedNbVictories.Value, ranking.PlayerDatas.Single(data => data.Equals(dataToUpdate)).NbVictories);
                Assert.Equal(expectedNbLosses.Value, ranking.PlayerDatas.Single(data => data.Equals(dataToUpdate)).NbLosses);
            }
            else
            {
                Assert.DoesNotContain(dataToUpdate, ranking.PlayerDatas);
            }
        }

        [Theory]
        [MemberData(nameof(TestData_Ranking.ClearPlayerDatas), MemberType=typeof(TestData_Ranking))]
        public void TestClearPlayerData(Ranking ranking)
        {
            ranking.ClearPlayersData();
            Assert.Empty(ranking.PlayerDatas);
        }

        [Theory]
        [MemberData(nameof(TestData_Ranking.RankingByDescendingPoints), MemberType=typeof(TestData_Ranking))]
        public void TestRankingByDescendingPoints(Ranking ranking, IList<int> orderedPlayerDatas, Func<Ranking, IEnumerable<PlayerData>> action)
        {
            var notRankedData = ranking.PlayerDatas.ToArray();
            var rankedData = action(ranking).ToArray();
            Assert.Equal(notRankedData.Length, rankedData.Count());
            for(int i = 0; i<orderedPlayerDatas.Count; i++)
            {
                Assert.Equal(notRankedData[orderedPlayerDatas[i]], rankedData[i]);
            }
        }
    }
}
