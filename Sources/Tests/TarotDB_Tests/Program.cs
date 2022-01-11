using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TarotDB;

namespace TarotDB_Tests
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using(TarotContext context = new TarotContext())
            {
                List<PlayerEntity> players = new List<PlayerEntity>()
                { new PlayerEntity{ FirstName = "Lenny", LastName = "White", NickName = null, ImageName = null },
                    new PlayerEntity{ FirstName = "Chick", LastName = "Corea", NickName = null, ImageName = null },
                    new PlayerEntity{ FirstName = "Stanley", LastName = "Clarke", NickName = null, ImageName = null },
                    new PlayerEntity{ FirstName = "Jean-Luc", LastName = "Ponty", NickName = null, ImageName = null },
                    new PlayerEntity{ FirstName = "Steve", LastName = "Gadd", NickName = null, ImageName = null },
                    new PlayerEntity{ FirstName = "Tony", LastName = "Williams", NickName = null, ImageName = null },
                    new PlayerEntity{ FirstName = "Ron", LastName = "Carter", NickName = null, ImageName = null }
                };
                await context.Players.AddRangeAsync(players);

                var session = new SessionEntity { Name = "Return To Forever", StartingTime = new DateTime(1970, 3, 4), EndingTime = new DateTime (2019, 8, 4) };
                context.LinkPlayerSession(players[0], session);
                context.LinkPlayerSession(players[1], session);
                context.LinkPlayerSession(players[2], session);
                context.LinkPlayerSession(players[3], session);
                context.LinkPlayerSession(players[4], session);
                var session2 = new SessionEntity { Name = "Funky", StartingTime = new DateTime(1970, 3, 4), EndingTime = new DateTime (2019, 8, 4) };
                context.LinkPlayerSession(players[0], session2);
                context.LinkPlayerSession(players[1], session2);
                context.LinkPlayerSession(players[2], session2);
                await context.Sessions.AddRangeAsync(session, session2);

                var game = new GameEntity { DateTime = DateTime.Now, Chelem = Chelem.Unknown, Poignée = Poignée.None, Petit = PetitResult.Owned, Excuse = false, TwentyOne = false, TakerPoints = 54 };
                game.Biddings.Add(new PlayerBiddingEntity { Player = players[0], Bidding = Bidding.Garde });
                game.Biddings.Add(new PlayerBiddingEntity { Player = players[1], Bidding = Bidding.Opponent });
                game.Biddings.Add(new PlayerBiddingEntity { Player = players[2], Bidding = Bidding.Opponent });
                game.Biddings.Add(new PlayerBiddingEntity { Player = players[3], Bidding = Bidding.Opponent });
                await context.Games.AddAsync(game);

                var game2 = new GameEntity { DateTime = DateTime.Now - TimeSpan.FromDays(300), Chelem = Chelem.Unknown, Poignée = Poignée.Simple, Petit = PetitResult.SavedAuBout, Excuse = true, TwentyOne = false, TakerPoints = 64 };
                game2.Biddings.Add(new PlayerBiddingEntity { Player = players[0], Bidding = Bidding.GardeSans });
                game2.Biddings.Add(new PlayerBiddingEntity { Player = players[2], Bidding = Bidding.Opponent });
                game2.Biddings.Add(new PlayerBiddingEntity { Player = players[4], Bidding = Bidding.Opponent });
                game2.Biddings.Add(new PlayerBiddingEntity { Player = players[6], Bidding = Bidding.Opponent });
                await context.Games.AddAsync(game2);

                await context.SaveChangesAsync();
            }

            using(TarotContext context = new TarotContext())
            {
                Console.WriteLine("PLAYERS");
                foreach(var pe in context.Players)
                {
                    Console.WriteLine($"({pe.Id}) {pe.FirstName} {pe.LastName}");
                }

                Console.WriteLine("SESSIONS");
                foreach(var s in context.Sessions.Include(s => s.Players).ThenInclude(ps => ps.Player))
                {
                    Console.WriteLine($"({s.Id}) {s.Name} [{s.StartingTime.Value.ToShortDateString()} -> {s.EndingTime.Value.ToShortDateString()}]");
                    foreach(var pe in s.Players)
                    {
                        Console.WriteLine($"\t({pe.Player.Id}) {pe.Player.FirstName} {pe.Player.LastName}");
                    }
                }

                Console.WriteLine("GAMES");
                foreach(var g in context.Games.Include(g => g.Biddings))
                {
                    Console.WriteLine($"({g.Id}) {g.DateTime.ToShortDateString()} - Points : {g.TakerPoints} ");
                    foreach(var p in g.Biddings)
                    {
                        Console.WriteLine($"\t{p.Bidding} - {p.Player}");
                    }
                }
            }
        }
    }
}
