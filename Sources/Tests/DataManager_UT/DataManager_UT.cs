using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Model;
using Xunit;

namespace DataManager_UT
{
    public class DataManager_UT
    {
        [Theory]
        [MemberData(nameof(TestData_DataManager.NbPlayers), MemberType = typeof(TestData_DataManager))]
        public async Task GetNbPlayers_Test(int expectedNbPlayers, IDataManager dataManager)
        {
            Assert.Equal(expectedNbPlayers, await dataManager.GetNbPlayers());
            dataManager.Dispose();
        }

        [Theory]
        [MemberData(nameof(TestData_DataManager.GetPlayers), MemberType = typeof(TestData_DataManager))]
        public async Task GetPlayers_Test(int expectedNbPlayers, int index, int count, IDataManager dataManager)
        {
            Assert.Equal(expectedNbPlayers, (await dataManager.GetPlayers(index, count)).Count());
            dataManager.Dispose();
        }

        [Theory]
        [MemberData(nameof(TestData_DataManager.GetPlayerById), MemberType = typeof(TestData_DataManager))]
        public async Task GetPlayerById_Test(int id, IDataManager dataManager, Player expectedPlayer)
        {
            Player player = await dataManager.GetPlayerById(id);
            Assert.Equal(expectedPlayer, player);
            Assert.Equal(expectedPlayer.Id, player.Id);
            Assert.Equal(expectedPlayer.FirstName, player.FirstName);
            Assert.Equal(expectedPlayer.LastName, player.LastName);
            Assert.Equal(expectedPlayer.NickName, player.NickName);
            Assert.Equal(expectedPlayer.Image, player.Image);
            dataManager.Dispose();
        }

        [Theory]
        [MemberData(nameof(TestData_DataManager.GetPlayersByName), MemberType = typeof(TestData_DataManager))]
        public async Task GetPlayersByName_Test(string substring, int index, int count, IDataManager dataManager, params Player[] expectedPlayers)
        {
            var players = await dataManager.GetPlayersByName(substring, index, count);
            Assert.Equal(expectedPlayers.Count(), players.Count());
            foreach(var player in players)
            {
                Assert.Contains(player, expectedPlayers);
                var expectedPlayer = expectedPlayers.Single(p => p.Equals(player));
                Assert.Equal(expectedPlayer, player);
                Assert.Equal(expectedPlayer.Id, player.Id);
                Assert.Equal(expectedPlayer.FirstName, player.FirstName);
                Assert.Equal(expectedPlayer.LastName, player.LastName);
                Assert.Equal(expectedPlayer.NickName, player.NickName);
                Assert.Equal(expectedPlayer.Image, player.Image);
            }
            
            dataManager.Dispose();
        }

        [Theory]
        [MemberData(nameof(TestData_DataManager.AddPlayer), MemberType = typeof(TestData_DataManager))]
        public async Task AddPlayer_Test(bool success, int expectedNbPlayersWithThisNameBeforeAdd,
                                         int id, string firstname, string lastname,
                                         string nickname, string image, IDataManager dataManager)
        {
            var playersWithThisName = await dataManager.GetPlayersByName(lastname, 0, 100);
            Assert.Equal(expectedNbPlayersWithThisNameBeforeAdd, playersWithThisName.Count());

            Player playerToAdd = new Player(id, firstname, lastname, nickname, image);
            bool result = await dataManager.AddPlayer(playerToAdd);
            Assert.Equal(success, result);

            playersWithThisName = await dataManager.GetPlayersByName(lastname, 0, 100);

            Assert.Equal(expectedNbPlayersWithThisNameBeforeAdd + (result ? 1 : 0), playersWithThisName.Count());
            if(result)
            {
                var addedPlayer = playersWithThisName.OrderByDescending(p => p.Id).First();
                Assert.Equal(firstname, addedPlayer.FirstName);
                Assert.Equal(lastname, addedPlayer.LastName);
                Assert.Equal(nickname, addedPlayer.NickName);
                Assert.Equal(image, addedPlayer.Image);
            }
        }

        [Theory]
        [MemberData(nameof(TestData_DataManager.DeletePlayer), MemberType = typeof(TestData_DataManager))]
        public async Task DeletePlayer_Test(bool expectedResult,
                                            int id, string firstname, string lastname,
                                            string nickname, string image,
                                            IDataManager dataManager)
        {
            if(expectedResult)
            {
                Assert.NotNull(await dataManager.GetPlayerById(id));
            }
            Player player = new Player(id, firstname, lastname, nickname, image);
            bool result = await dataManager.DeletePlayer(player);
            Assert.Equal(expectedResult, result);
            Assert.Null(await dataManager.GetPlayerById(id));
        }

        [Theory]
        [MemberData(nameof(TestData_DataManager.DeletePlayerById), MemberType = typeof(TestData_DataManager))]
        public async Task DeletePlayerById_Test(bool expectedResult,
                                            int id, IDataManager dataManager)
        {
            if(expectedResult)
            {
                Assert.NotNull(await dataManager.GetPlayerById(id));
            }
            bool result = await dataManager.DeletePlayer(id);
            Assert.Equal(expectedResult, result);
            Assert.Null(await dataManager.GetPlayerById(id));
        }

        [Theory]
        [MemberData(nameof(TestData_DataManager.UpdatePlayer), MemberType = typeof(TestData_DataManager))]
        public async Task UpdatePlayer_Test(bool expectedResult,
                                            int id, Player player,
                                            IDataManager dataManager)
        {
            if(expectedResult)
            {
                Assert.NotNull(await dataManager.GetPlayerById(id));
            }
            bool result = await dataManager.UpdatePlayer(id, player);
            Assert.Equal(expectedResult, result);
            if(result == false) return;

            Player modifiedPlayer = await dataManager.GetPlayerById(id);
            Assert.NotNull(modifiedPlayer);

            Assert.Equal(id, modifiedPlayer.Id);
            Assert.Equal(player.FirstName, modifiedPlayer.FirstName);
            Assert.Equal(player.LastName, modifiedPlayer.LastName);
            Assert.Equal(player.NickName, modifiedPlayer.NickName);
            Assert.Equal(player.Image, modifiedPlayer.Image);
        }

        [Theory]
        [MemberData(nameof(TestData_DataManager.NbSessions), MemberType = typeof(TestData_DataManager))]
        public async Task GetNbSessions_Test(int expectedNbSessions, IDataManager dataManager)
        {
            Assert.Equal(expectedNbSessions, await dataManager.GetNbSessions());
            dataManager.Dispose();
        }

        [Theory]
        [MemberData(nameof(TestData_DataManager.GetSessions), MemberType = typeof(TestData_DataManager))]
        public async Task GetSessions_Test(int expectedNbSessions, int index, int count, IDataManager dataManager)
        {
            Assert.Equal(expectedNbSessions, (await dataManager.GetSessions(index, count)).Count());
            dataManager.Dispose();
        }

        [Theory]
        [MemberData(nameof(TestData_DataManager.GetSessionById), MemberType = typeof(TestData_DataManager))]
        public async Task GetSessionById_Test(int id, IDataManager dataManager, Session expectedSession)
        {
            Session session = await dataManager.GetSessionById(id);
            if(expectedSession == null)
            {
                Assert.Null(session);
                return;
            }
            Assert.Equal(expectedSession.Id, session.Id);
            Assert.Equal(expectedSession.EndingTime, session.EndingTime);
            Assert.Equal(expectedSession.Name, session.Name);
            Assert.Equal(expectedSession.StartingTime, session.StartingTime);
            Assert.Equal(expectedSession.Players.Count, session.Players.Count);
            foreach(var p in session.Players)
            {
                Assert.Contains(p, expectedSession.Players);
            }
            dataManager.Dispose();
        }

        [Theory]
        [MemberData(nameof(TestData_DataManager.GetSessionsByName), MemberType = typeof(TestData_DataManager))]
        public async Task GetSessionsByName_Test(string substring, int index, int count, IDataManager dataManager, params Session[] expectedSessions)
        {
            var sessions = await dataManager.GetSessionsByName(substring, index, count);
            Assert.Equal(expectedSessions.Count(), sessions.Count());
            foreach(var session in sessions)
            {
                Assert.Contains(session, expectedSessions);
                var expectedSession = expectedSessions.Single(s => s.Equals(session));
                Assert.Equal(expectedSession, session);
                Assert.Equal(expectedSession.Id, session.Id);
                Assert.Equal(expectedSession.EndingTime, session.EndingTime);
                Assert.Equal(expectedSession.Name, session.Name);
                Assert.Equal(expectedSession.StartingTime, session.StartingTime);
                Assert.Equal(expectedSession.Players.Count, session.Players.Count);
                foreach(var p in session.Players)
                {
                    Assert.Contains(p, expectedSession.Players);
                }
            }
            
            dataManager.Dispose();
        }

        [Theory]
        [MemberData(nameof(TestData_DataManager.GetSessionsByPlayer), MemberType = typeof(TestData_DataManager))]
        public async Task GetSessionsByPlayer_Test(Player player, int index, int count, IDataManager dataManager, params Session[] expectedSessions)
        {
            var sessions = await dataManager.GetSessionsByPlayer(player, index, count);
            Assert.Equal(expectedSessions.Count(), sessions.Count());
            foreach(var session in sessions)
            {
                Assert.Contains(session, expectedSessions);
                var expectedSession = expectedSessions.Single(s => s.Equals(session));
                Assert.Equal(expectedSession, session);
                Assert.Equal(expectedSession.Id, session.Id);
                Assert.Equal(expectedSession.EndingTime, session.EndingTime);
                Assert.Equal(expectedSession.Name, session.Name);
                Assert.Equal(expectedSession.StartingTime, session.StartingTime);
                Assert.Equal(expectedSession.Players.Count, session.Players.Count);
                foreach(var p in session.Players)
                {
                    Assert.Contains(p, expectedSession.Players);
                }
            }
            
            dataManager.Dispose();
        }

        [Theory]
        [MemberData(nameof(TestData_DataManager.AddSession), MemberType = typeof(TestData_DataManager))]
        public async Task AddSession_Test(bool success, IDataManager dataManager, int expectedId,
                                         int id, string name, DateTime? startTime, DateTime? endTime,
                                         int expectedNbOfNewPlayers,
                                         params Player[] players)
        {
            Session existingSession = await dataManager.GetSessionById(expectedId);

            if(success)
            {
                Assert.Null(existingSession);
            }
            else
            {
                Assert.NotNull(existingSession);
            }
            int nbPlayersBefore = await dataManager.GetNbPlayers();
            Session sessionToAdd = new Session(id, name, startTime, endTime, players);
            bool result = await dataManager.AddSession(sessionToAdd);
            Assert.Equal(success, result);

            if(result)
            {
                Session addedSession = await dataManager.GetSessionById(expectedId);
                Assert.Equal(expectedId, addedSession.Id);
                Assert.Equal(name, addedSession.Name);
                Assert.Equal(startTime, addedSession.StartingTime);
                Assert.Equal(endTime, addedSession.EndingTime);
                int nbPlayersAfter = await dataManager.GetNbPlayers();
                Assert.Equal(expectedNbOfNewPlayers, nbPlayersAfter-nbPlayersBefore);
                Assert.Equal(players.Count(), addedSession.Players.Count());

                Assert.All(players,
                            async p =>
                            {
                                if(p.Id != 0)
                                    Assert.Contains(p, addedSession.Players);

                                else
                                    Assert.NotEmpty(addedSession.Players.Where(pl =>
                                                        pl.FirstName == p.FirstName
                                                        && pl.LastName == p.LastName
                                                        && pl.NickName == p.NickName
                                                        && pl.Image == p.Image));
                                Assert.NotEmpty(await dataManager.GetPlayersByName(p.LastName, 0, 10));
                            });
            }
        }

        [Theory]
        [MemberData(nameof(TestData_DataManager.DeleteSession), MemberType = typeof(TestData_DataManager))]
        public async Task DeleteSession_Test(bool expectedResult,
                                            Session session,
                                            IDataManager dataManager)
        {
            if (expectedResult)
            {
                Assert.NotNull(await dataManager.GetSessionById(session.Id));
            }
            IEnumerable<long> existingPlayerIds = session.Players.Where(p => dataManager.GetPlayerById(p.Id).Result != null)
                                                                .Select(p => p.Id);
            bool result = await dataManager.DeleteSession(session);
            Assert.Equal(expectedResult, result);
            Assert.Null(await dataManager.GetSessionById(session.Id));

            foreach(long playerId in existingPlayerIds)
            {
                Assert.NotNull(await dataManager.GetPlayerById(playerId));
            }
        }

        [Theory]
        [MemberData(nameof(TestData_DataManager.DeleteSessionById), MemberType = typeof(TestData_DataManager))]
        public async Task DeleteSessionById_Test(bool expectedResult,
                                                int id, IDataManager dataManager)
        {
            Session session = await dataManager.GetSessionById(id);
            if (expectedResult)
            {
                Assert.NotNull(session);
            }
            bool result = await dataManager.DeleteSession(id);
            Assert.Equal(expectedResult, result);

            if(!result)
                return;

            Assert.Null(await dataManager.GetSessionById(id));

            Assert.All(session.Players, async p => Assert.NotNull(await dataManager.GetPlayerById(p.Id)));
        }

        [Theory]
        [MemberData(nameof(TestData_DataManager.UpdateSession), MemberType = typeof(TestData_DataManager))]
        public async Task UpdateSession_Test(bool expectedResult,
                                            int id, Session session,
                                            IDataManager dataManager)
        {
            if (expectedResult)
            {
                Assert.NotNull(await dataManager.GetSessionById(id));
            }
            bool result = await dataManager.UpdateSession(id, session);
            Assert.Equal(expectedResult, result);
            if (result == false) return;

            Session modifiedSession = await dataManager.GetSessionById(id);
            Assert.NotNull(modifiedSession);

            Assert.Equal(id, modifiedSession.Id);
            Assert.Equal(session.Name, modifiedSession.Name);
            Assert.Equal(session.StartingTime, modifiedSession.StartingTime);
            Assert.Equal(session.EndingTime, modifiedSession.EndingTime);

            Assert.Equal(session.Players.Count, modifiedSession.Players.Count);

            Assert.All(session.Players,
                        p =>
                        {
                            Assert.NotEmpty(modifiedSession.Players.Where(
                                pl => pl.FirstName == p.FirstName
                                    && pl.LastName == p.LastName
                                    && pl.NickName == p.NickName
                                    && pl.Image == p.Image));
                        });

            Assert.All(session.Players,
                        p =>
                        {
                            Assert.NotEmpty(dataManager.GetPlayers(0, 100).Result.Where(
                                pl => pl.FirstName == p.FirstName
                                    && pl.LastName == p.LastName
                                    && pl.NickName == p.NickName
                                    && pl.Image == p.Image));
                        });
        }

        [Theory]
        [MemberData(nameof(TestData_DataManager.NbGames), MemberType = typeof(TestData_DataManager))]
        public async Task GetNbGames_Test(int expectedNbGames, IDataManager dataManager)
        {
            Assert.Equal(expectedNbGames, await dataManager.GetNbGames());
            dataManager.Dispose();
        }

        [Theory]
        [MemberData(nameof(TestData_DataManager.GetGames), MemberType = typeof(TestData_DataManager))]
        public async Task GetGames_Test(int expectedNbGames, int index, int count, IDataManager dataManager)
        {
            Assert.Equal(expectedNbGames, (await dataManager.GetGames(index, count)).Count());
            dataManager.Dispose();
        }

        private void CompareGames(Game expectedGame, Game game, bool includeId = true)
        {
            if(includeId)
            { 
                Assert.Equal(expectedGame.Id, game.Id);
            }
            Assert.Equal(expectedGame.Chelem, game.Chelem);
            Assert.Equal(expectedGame.Date, game.Date);
            Assert.Equal(expectedGame.Excuse, game.Excuse);
            Assert.Equal(expectedGame.NbPlayers, game.NbPlayers);
            Assert.Equal(expectedGame.PetitResult, game.PetitResult);
            Assert.Equal(expectedGame.Poignée, game.Poignée);
            Assert.Equal(expectedGame.Rules, game.Rules);
            Assert.Equal(expectedGame.TakerPoints, game.TakerPoints);
            Assert.Equal(expectedGame.TwentyOne, game.TwentyOne);
            Assert.Equal(expectedGame.Players.Count, game.Players.Count);
            foreach (var pb in game.Players)
            {
                Assert.Contains(pb, expectedGame.Players);
            }
        }

        [Theory]
        [MemberData(nameof(TestData_DataManager.GetGameById), MemberType = typeof(TestData_DataManager))]
        public async Task GetGameById_Test(int id, IDataManager dataManager, Game expectedGame)
        {
            Game game = await dataManager.GetGameById(id);
            if (expectedGame == null)
            {
                Assert.Null(game);
                return;
            }
            CompareGames(expectedGame, game);
            dataManager.Dispose();
        }

        [Theory]
        [MemberData(nameof(TestData_DataManager.GetGamesByDate), MemberType = typeof(TestData_DataManager))]
        public async Task GetGamesByDate_Test(
            DateTime? start, DateTime? end, int index, int count, IDataManager dataManager,
            params Game[] expectedGames)
        {
            var result = await dataManager.GetGamesByDate(start, end, index, count);
            Assert.Equal(expectedGames.Length, result.Count());

            Assert.All(expectedGames, expectedGame =>
                    Assert.Contains(expectedGame, result, Game.FullComparer));
            dataManager.Dispose();
        }

        [Theory]
        [MemberData(nameof(TestData_DataManager.GetGamesBySession), MemberType = typeof(TestData_DataManager))]
        public async Task GetGamesBySession_Test(
            Session session, int index, int count, IDataManager dataManager,
            params Game[] expectedGames)
        {
            var result = await dataManager.GetGamesBySession(session, index, count);
            Assert.Equal(expectedGames.Length, result.Count());

            Assert.All(expectedGames, expectedGame =>
                    Assert.Contains(expectedGame, result, Game.FullComparer));
            dataManager.Dispose();
        }

        [Theory]
        [MemberData(nameof(TestData_DataManager.GetGamesByPlayer), MemberType = typeof(TestData_DataManager))]
        public async Task GetGamesByPlayer_Test(
            Player player, int index, int count, IDataManager dataManager,
            params Game[] expectedGames)
        {
            var result = await dataManager.GetGamesByPlayer(player, index, count);
            Assert.Equal(expectedGames.Length, result.Count());

            Assert.All(expectedGames, expectedGame =>
                    Assert.Contains(expectedGame, result, Game.FullComparer));
            dataManager.Dispose();
        }

        [Theory]
        [MemberData(nameof(TestData_DataManager.GetGamesByPlayers), MemberType = typeof(TestData_DataManager))]
        public async Task GetGamesByPlayers_Test(
            Player[] players, int index, int count, IDataManager dataManager,
            params Game[] expectedGames)
        {
            var result = await dataManager.GetGamesByPlayers(players, index, count);
            Assert.Equal(expectedGames.Length, result.Count());

            Assert.All(expectedGames, expectedGame =>
                    Assert.Contains(expectedGame, result, Game.FullComparer));
            dataManager.Dispose();
        }

        [Theory]
        [MemberData(nameof(TestData_DataManager.AddGame), MemberType = typeof(TestData_DataManager))]
        public async Task AddGame_Test(bool success, IDataManager dataManager, long expectedId,
                                         IEnumerable<Tuple<Player, Model.Bidding>> expectedPlayers,
                                         int id, DateTime date, IRules rules,
                                         int takerPoints, Model.PetitResult petitResult, Model.Poignée poignée,
                                         bool excuse, bool twentyOne, Model.Chelem chelem,
                                         params Tuple<Player, Model.Bidding>[] biddings)
        {
            Game existingGame = await dataManager.GetGameById(expectedId);

            if(success)
            {
                Assert.Null(existingGame);
            }
            Game gameToAdd = new Game(id, date, rules, takerPoints, petitResult, poignée,
                                      excuse, twentyOne, chelem, biddings);
            bool result = await dataManager.AddGame(gameToAdd);
            Assert.Equal(success, result);

            if(result)
            {
                Game addedGame = null;
                addedGame = await dataManager.GetGameById(expectedId);
                Assert.Equal(expectedId, addedGame.Id);
                Assert.Equal(chelem, addedGame.Chelem);
                Assert.Equal(date, addedGame.Date);
                Assert.Equal(excuse, addedGame.Excuse);
                Assert.Equal(petitResult, addedGame.PetitResult);
                Assert.Equal(poignée, addedGame.Poignée);
                Assert.Equal(rules, addedGame.Rules);
                Assert.Equal(takerPoints, addedGame.TakerPoints);
                Assert.Equal(twentyOne, addedGame.TwentyOne);
                Assert.Equal(expectedPlayers.Count(), addedGame.NbPlayers);
                Assert.Equal(expectedPlayers.Count(), addedGame.Players.Count);
                Assert.All(addedGame.Players, kvp => Assert.Contains(Tuple.Create(kvp.Key, kvp.Value), expectedPlayers, new PlayerBiddingComparer()));

                Assert.All(expectedPlayers,
                            async t =>
                            {
                                if(t.Item1.Id != 0)
                                    Assert.NotNull(await dataManager.GetPlayerById(t.Item1.Id));
                                else
                                    Assert.NotEmpty(await dataManager.GetPlayersByName(t.Item1.LastName, 0, 100));
                            });
            }

            
        }

        class PlayerBiddingComparer : EqualityComparer<Tuple<Player, Bidding>>
        {
            public override bool Equals([AllowNull] Tuple<Player, Bidding> x, [AllowNull] Tuple<Player, Bidding> y)
            {
                return Player.PropertiesComparer.Equals(x.Item1, y.Item1) && x.Item2 == y.Item2;
            }

            public override int GetHashCode([DisallowNull] Tuple<Player, Bidding> obj)
            {
                return Player.PropertiesComparer.GetHashCode(obj.Item1);
            }
        }

        [Theory]
        [MemberData(nameof(TestData_DataManager.DeleteGame), MemberType = typeof(TestData_DataManager))]
        public async Task DeleteGame_Test(bool expectedResult,
                                            Game game,
                                            IDataManager dataManager)
        {
            if (expectedResult)
            {
                Assert.NotNull(await dataManager.GetGameById(game.Id));
            }
            IEnumerable<long> existingPlayerIds = game.Players.Where(pb => dataManager.GetPlayerById(pb.Key.Id).Result != null)
                                                              .Select(pb => pb.Key.Id);
            bool result = await dataManager.DeleteGame(game);
            Assert.Equal(expectedResult, result);
            Assert.Null(await dataManager.GetGameById(game.Id));

            foreach (long playerId in existingPlayerIds)
            {
                Assert.NotNull(await dataManager.GetPlayerById(playerId));
            }
        }

        [Theory]
        [MemberData(nameof(TestData_DataManager.DeleteGameById), MemberType = typeof(TestData_DataManager))]
        public async Task DeleteGameById_Test(bool expectedResult,
                                                int id, IDataManager dataManager)
        {
            Game game = await dataManager.GetGameById(id);
            if (expectedResult)
            {
                Assert.NotNull(game);
            }
            bool result = await dataManager.DeleteGame(id);
            Assert.Equal(expectedResult, result);

            if (!result)
                return;

            Assert.Null(await dataManager.GetGameById(id));

            Assert.All(game.Players.Keys, async p => Assert.NotNull(await dataManager.GetPlayerById(p.Id)));
        }

        [Theory]
        [MemberData(nameof(TestData_DataManager.UpdateGame), MemberType = typeof(TestData_DataManager))]
        public async Task UpdateGame_Test(bool expectedResult,
                                            int id, Game game,
                                            IDataManager dataManager)
        {
            if (expectedResult)
            {
                Assert.NotNull(await dataManager.GetGameById(id));
            }
            bool result = await dataManager.UpdateGame(id, game);
            Assert.Equal(expectedResult, result);
            if (result == false) return;

            Game modifiedGame = await dataManager.GetGameById(id);
            Assert.NotNull(modifiedGame);

            Assert.Equal(id, modifiedGame.Id);
            CompareGames(modifiedGame, game, false);

            Assert.All(game.Players.Keys,
                        p =>
                        {
                            Assert.Single(modifiedGame.Players.Keys,
                                          pl => Player.PropertiesComparer.Equals(p, pl));
                        });

            Assert.All(game.Players.Keys,
                        p =>
                        {
                            Assert.Single(dataManager.GetPlayers(0, 100).Result,
                                          pl => Player.PropertiesComparer.Equals(p, pl));
                        });
        }
    }
}
