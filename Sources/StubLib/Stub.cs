using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model;

namespace StubLib
{
    public class Stub : IDataManager
    {
        private int playerIdCounter = 16;

        private List<Player> players = new List<Player>()
        {
            new Player(1, "Lenny", "White", null, null),
            new Player(2, "Chick", "Corea", null, null),
            new Player(3, "Stanley", "Clarke", null, null),
            new Player(4, "Jean-Luc", "Ponty", null, null),
            new Player(5, "Steve", "Gadd", null, null),
            new Player(6, "Tony", "Williams", null, null),
            new Player(7, "Ron", "Carter", null, null),
            new Player(8, "Miles", "Davis", null, null),
            new Player(9, "Wayne", "Shorter", null, null),
            new Player(10, "John", "McLaughlin", null, null),
            new Player(11, "Herbie", "Hancock", null, null),
            new Player(12, "Joe", "Zawinul", null, null),
            new Player(13, "Dave", "Holland", null, null),
            new Player(14, "Miroslav", "Vitous", null, null),
            new Player(15, "Jaco", "Pastorius", null, null),
            new Player(16, "Peter", "Erskine", null, null),
            new Player(-1, "Jane", "Doe", null, null)
        };

        private List<Player> validPlayers => players.Except(new Player[] {players[16] }).ToList();

        private int sessionIdCounter = 3;

        private List<Session> sessions = new List<Session>()
        {
            new Session(1, "Return To Forever", new DateTime(1972, 2, 2), new DateTime(2021, 2, 9)),
            new Session(2, "In a silent way", new DateTime(1969, 2, 1), new DateTime(1969, 2, 28)),
            new Session(3, "Weather Report", new DateTime(1970, 1, 1), new DateTime(1986, 12, 31)),
        };

        private int gameIdCounter = 5;

        private List<Game> games = new List<Game>();
        
        public Stub()
        {
            sessions[0].AddPlayers(players[0], players[1], players[2], players[3], players[4]);
            sessions[1].AddPlayers(players[7], players[8], players[9], players[10], players[11], players[12], players[5]);
            sessions[2].AddPlayers(players[8], players[11], players[13], players[14], players[15]);

            games.AddRange(new Game[] {
            
                new Game(1, new DateTime(2021, 1, 1), new FrenchTarotRules(), 49, PetitResult.SavedAuBout, Poignée.Simple, true, false, Chelem.Unknown,
                            Tuple.Create(players[0], Bidding.GardeSans),
                            Tuple.Create(players[1], Bidding.Opponent),
                            Tuple.Create(players[2], Bidding.Opponent)),
                new Game(2, new DateTime(2021, 2, 2), new FrenchTarotRules(), 45, PetitResult.LostAuBout, Poignée.None, true, true, Chelem.Unknown,
                            Tuple.Create(players[3], Bidding.Garde),
                            Tuple.Create(players[4], Bidding.Opponent),
                            Tuple.Create(players[5], Bidding.Opponent),
                            Tuple.Create(players[6], Bidding.Opponent)),
                new Game(3, new DateTime(2021, 3, 3), new FrenchTarotRules(), 44, PetitResult.SavedAuBout, Poignée.Simple, false, false, Chelem.Unknown,
                            Tuple.Create(players[7], Bidding.Petite),
                            Tuple.Create(players[8], Bidding.KingCalled),
                            Tuple.Create(players[9], Bidding.Opponent),
                            Tuple.Create(players[10], Bidding.Opponent),
                            Tuple.Create(players[11], Bidding.Opponent)),
                new Game(4, new DateTime(2021, 4, 4), new FrenchTarotRules(), 52, PetitResult.Saved, Poignée.SimpleDefense, true, false, Chelem.Unknown,
                            Tuple.Create(players[12], Bidding.GardeContre),
                            Tuple.Create(players[13], Bidding.Opponent),
                            Tuple.Create(players[0], Bidding.Opponent),
                            Tuple.Create(players[15], Bidding.Opponent)),
                new Game(5, new DateTime(2021, 5, 5), new FrenchTarotRules(), 87, PetitResult.SavedAuBout, Poignée.Simple, false, true, Chelem.AnnouncedSuccess,
                            Tuple.Create(players[9], Bidding.GardeContre),
                            Tuple.Create(players[15], Bidding.KingCalled),
                            Tuple.Create(players[6], Bidding.Opponent),
                            Tuple.Create(players[3], Bidding.Opponent),
                            Tuple.Create(players[1], Bidding.Opponent))
                }
            );

        }

        public Task<int> GetNbPlayers() => Task.FromResult(validPlayers.Count);

        public Task<IEnumerable<Player>> GetPlayers(int index, int count)
        {
            var selectedPlayers = validPlayers.Skip(index * count).Take(count);
            return Task.FromResult(selectedPlayers);
        }

        public Task<Player> GetPlayerById(long id)
        {
            return Task.FromResult(players.SingleOrDefault(p => p.Id == id));
        }

        public Task<IEnumerable<Player>> GetPlayersByName(string substring, int index, int count)
        {
            var selectedPlayers = validPlayers.Where(p => p.StartsWith(substring) || p.Contains(substring))
                                         .OrderBy(p => p, new PlayerNamesEqualityComparer(substring))
                                         .Skip(index * count).Take(count);
            return Task.FromResult(selectedPlayers);
        }

        public Task<bool> AddPlayer(Player player)
        {
            if (player == null || player.Id != 0)
            {
                return Task.FromResult(false);
            }
            players.Add(new Player(++playerIdCounter, player.FirstName, player.LastName, player.NickName, player.Image));
            return Task.FromResult(true);

        }

        public Task<bool> DeletePlayer(Player player)
        {
            if (player == null) return Task.FromResult(false);
            return DeletePlayer(player.Id);
        }

        public Task<bool> DeletePlayer(long id)
        {
            if (id == 0) return Task.FromResult(false);
            Player p = players.SingleOrDefault(pl => pl.Id == id);
            if (p != null) Task.FromResult(false);
            foreach(var session in GetSessionsByPlayer(p, 0, GetNbSessions().Result).Result)
            {
                DeleteSession(session);
            }
            foreach(var game in GetGamesByPlayer(p, 0, GetNbGames().Result).Result)
            {
                var playerBidding = game.Players.SingleOrDefault(pb => pb.Key == p);
                game.RemovePlayer(p);
                game.AddPlayer(players[16], playerBidding.Value);
            }

            return Task.FromResult(players.RemoveAll(pl => pl.Id == id) > 0);
        }

        public Task<bool> UpdatePlayer(long id, Player player)
        {
            if (id == 0 || player == null) return Task.FromResult(false);
            var foundPlayer = players.Find(p => p.Id == id);
            if (foundPlayer == null) return Task.FromResult(false);

            var updatedPlayer = new Player(id, player.FirstName, player.LastName, player.NickName, player.Image);
            players.Remove(foundPlayer);
            players.Add(updatedPlayer);
            return Task.FromResult(true);
        }

        public Task<int> GetNbSessions() => Task.FromResult(sessions.Count);

        public Task<IEnumerable<Session>> GetSessions(int index, int count)
        {
            var selectedSessions = sessions.Skip(index * count).Take(count);
            return Task.FromResult(selectedSessions);
        }

        public Task<Session> GetSessionById(long id)
        {
            return Task.FromResult(sessions.SingleOrDefault(p => p.Id == id));
        }

        public Task<IEnumerable<Session>> GetSessionsByName(string substring, int index, int count)
        {
            var selectedSessions = sessions.Where(s => s.Name.StartsWith(substring) || s.Name.ToUpper().Contains(substring.ToUpper())
                                                       || s.Players.Select(p => $"{p.FirstName} {p.LastName} {p.NickName}".ToUpper().Contains(substring.ToUpper())).Contains(true))
                                         .OrderBy(s => s.Name)
                                         .Skip(index * count).Take(count);
            return Task.FromResult(selectedSessions);
        }

        public Task<IEnumerable<Session>> GetSessionsByPlayer(Player player, int index, int count)
        {
            var selectedSessions = sessions.Where(s => s.Players.Contains(player))
                                         .OrderBy(s => s.Name)
                                         .Skip(index * count).Take(count);
            return Task.FromResult(selectedSessions);
        }

        public Task<bool> AddSession(Session session)
        {
            if (session == null || session.Id != 0)
            {
                return Task.FromResult(false);
            }
            foreach(Player player in session.Players)
            {
                AddPlayer(player);
            }
            sessions.Add(new Session(++sessionIdCounter, session.Name, session.StartingTime, session.EndingTime, session.Players.ToArray()));
            return Task.FromResult(true);
        }

        public Task<bool> DeleteSession(Session session)
        {
            if (session == null) return Task.FromResult(false);
            return DeleteSession(session.Id);
        }

        public Task<bool> DeleteSession(long id)
        {
            if (id == 0) return Task.FromResult(false);
            return Task.FromResult(sessions.RemoveAll(s => s.Id == id) > 0);
        }

        public Task<bool> UpdateSession(long id, Session session)
        {
            if (id == 0 || session == null) return Task.FromResult(false);
            var foundSession = sessions.Find(p => p.Id == id);
            if (foundSession == null) return Task.FromResult(false);

            var updatedSession = new Session(id, session.Name, session.StartingTime, session.EndingTime, session.Players.ToArray());
            sessions.Remove(foundSession);
            sessions.Add(updatedSession);
            return Task.FromResult(true);
        }

        public Task<int> GetNbGames() => Task.FromResult(games.Count);

        public Task<IEnumerable<Game>> GetGames(int index, int count)
        {
            var selectedGames = games.Skip(index * count).Take(count);
            return Task.FromResult(selectedGames);
        }

        public Task<Game> GetGameById(long id)
        {
            return Task.FromResult(games.SingleOrDefault(p => p.Id == id));
        }

        public Task<IEnumerable<Game>> GetGamesByDate(DateTime? startTime, DateTime? endTime, int index, int count)
        {
            var selectedGames = games.Where(g => (!startTime.HasValue || g.Date > startTime)
                                                && (!endTime.HasValue || g.Date < endTime))
                                         .OrderByDescending(g => g.Date)
                                         .Skip(index * count).Take(count);
            return Task.FromResult(selectedGames);
        }

        public Task<IEnumerable<Game>> GetGamesBySession(Session session, int index, int count)
        {
            var selectedGames = games.Where(g => g.Players.Count == session.Players.Count
                                                && g.Players.Select(p => p.Key).All(p => session.Players.Contains(p))
                                                && (!session.StartingTime.HasValue || g.Date > session.StartingTime.Value)
                                                && (!session.EndingTime.HasValue || g.Date < session.EndingTime.Value)    )
                                         .OrderByDescending(g => g.Date)
                                         .Skip(index * count).Take(count);
            return Task.FromResult(selectedGames);
        }

        public Task<IEnumerable<Game>> GetGamesByPlayer(Player player, int index, int count)
        {
            var selectedGames = games.Where(g => g.Players.Select(p => p.Key).Contains(player))
                                         .OrderByDescending(g => g.Date)
                                         .Skip(index * count).Take(count);
            return Task.FromResult(selectedGames);
        }

        public Task<IEnumerable<Game>> GetGamesByPlayers(IEnumerable<Player> players, int index, int count)
        {
            var selectedGames = games.Where(g => players.All(pl => g.Players.Select(p => p.Key).Contains(pl)))
                                         .OrderByDescending(g => g.Date)
                                         .Skip(index * count).Take(count);
            return Task.FromResult(selectedGames);
        }

        public Task<bool> AddGame(Game game)
        {
            if (game == null || game.Id != 0)
            {
                return Task.FromResult(false);
            }
            foreach(Player player in game.Players.Select(p => p.Key))
            {
                AddPlayer(player);
            }
            games.Add(new Game(++gameIdCounter, game.Date, game.Rules, game.TakerPoints,
                                game.PetitResult, game.Poignée, game.Excuse, game.TwentyOne,
                                game.Chelem,
                                game.Players.Select(p => Tuple.Create(p.Key, p.Value)).ToArray() ));
            return Task.FromResult(true);
        }

        public Task<bool> DeleteGame(Game game)
        {
            if (game == null) return Task.FromResult(false);
            return DeleteGame(game.Id);
        }

        public Task<bool> DeleteGame(long id)
        {
            if (id == 0) return Task.FromResult(false);
            return Task.FromResult(games.RemoveAll(s => s.Id == id) > 0);
        }

        public Task<bool> UpdateGame(long id, Game game)
        {
            if (id == 0 || game == null) return Task.FromResult(false);
            var foundGame = games.Find(p => p.Id == id);
            if (foundGame == null) return Task.FromResult(false);

            var updatedGame = new Game(id, game.Date, game.Rules, game.TakerPoints,
                                game.PetitResult, game.Poignée, game.Excuse, game.TwentyOne,
                                game.Chelem,
                                game.Players.Select(p => Tuple.Create(p.Key, p.Value)).ToArray());
            games.Remove(foundGame);
            games.Add(updatedGame);
            return Task.FromResult(true);
        }

        public void Dispose()
        {
        }
    }
}
