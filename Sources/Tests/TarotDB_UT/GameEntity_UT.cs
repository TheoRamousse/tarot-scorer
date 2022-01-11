using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using TarotDB;
using Xunit;

namespace TarotDB_UT
{
    public class GameEntity_UT
    {
        private static DbContextOptions<TarotContext> InitDB()
        {
            //connection must be opened to use In-memory database
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<TarotContext>()
                .UseSqlite(connection)
                .Options;

            return options;
        }

        [Theory]
        [MemberData(nameof(TestData_Game.Games), MemberType = typeof(TestData_Game))]
        public async Task Read_Test(long id, DateTime expectedDateTime, string expectedRules,
                                    int expectedTakerPoints, PetitResult expectedPetitResult,
                                    Poignée expectedPoignée, bool? expectedExcuse, bool? expected21,
                                    Chelem expectedChelem, params Tuple<Bidding, long>[] expectedPlayers)
        {
            //prepares the database with one instance of the context
            using (var context = new TarotContextStub(InitDB()))
            {
                context.Database.EnsureCreated();

                GameEntity game = await context.Games.Include(s => s.Biddings).ThenInclude(s => s.Player)
                                                .SingleOrDefaultAsync(s => s.Id == id);
                Assert.Equal(expectedDateTime, game.DateTime);
                Assert.Equal(expectedRules, game.Rules);
                Assert.Equal(expectedTakerPoints, game.TakerPoints);
                Assert.Equal(expectedPetitResult, game.Petit);
                Assert.Equal(expectedPoignée, game.Poignée);
                Assert.Equal(expectedExcuse, game.Excuse);
                Assert.Equal(expected21, game.TwentyOne);
                Assert.Equal(expectedChelem, game.Chelem);

                Assert.Equal(expectedPlayers.Count(), game.Biddings.Count());
                foreach(var playerBidding in expectedPlayers)
                {
                    Assert.Single(game.Biddings.Where(b => b.Bidding == playerBidding.Item1
                                                            && b.Player.Equals(context.Players.Find(playerBidding.Item2))
                                                            && b.Game == game));
                }
            }
        }

        [Theory]
        [MemberData(nameof(TestData_Game.GamesToAdd), MemberType = typeof(TestData_Game))]
        async Task Add_Test(DateTime date, string rulesName, int takerPoints, PetitResult expectedPetitResult,
                                    Poignée expectedPoignée, bool? expectedExcuse, bool? expected21,
                                    Chelem expectedChelem, params Tuple<PlayerEntity, Bidding>[] players)
        {
            var options = InitDB();
            //prepares the database with one instance of the context
            using (var context = new TarotContextStub(options))
            {
                context.Database.EnsureCreated();

                var gamesWithDate = context.Games.Where(g => g.DateTime == date);
                Assert.Empty(gamesWithDate);

                GameEntity game = new GameEntity
                {
                    DateTime = date,
                    Rules = rulesName,
                    TakerPoints = takerPoints,
                    Petit = expectedPetitResult,
                    Poignée = expectedPoignée,
                    Chelem = expectedChelem,
                    Excuse = expectedExcuse,
                    TwentyOne = expected21,
                };

                await context.Games.AddAsync(game);

                await context.AddRangeAsync(players.Select(p => new PlayerBiddingEntity
                {
                    Bidding = p.Item2,
                    Player = context.Find<PlayerEntity>(p.Item1.Id) == null ? p.Item1 : context.Find<PlayerEntity>(p.Item1.Id),
                    Game = game
                }));

                await context.SaveChangesAsync();
            }

            using (var context = new TarotContextStub(options))
            {
                context.Database.EnsureCreated();

                var gamesWithDate = context.Games.Include(g => g.Biddings).ThenInclude(g => g.Player)
                                                .Where(g => g.DateTime == date);
                Assert.Single(gamesWithDate);

                GameEntity game = gamesWithDate.Single();
                Assert.Equal(date, game.DateTime);
                Assert.Equal(rulesName, game.Rules);
                Assert.Equal(takerPoints, game.TakerPoints);
                Assert.Equal(expectedExcuse, game.Excuse);
                Assert.Equal(expected21, game.TwentyOne);
                Assert.Equal(expectedPetitResult, game.Petit);
                Assert.Equal(expectedPoignée, game.Poignée);
                Assert.Equal(expectedChelem, game.Chelem);

                Assert.Equal(players.Count(), game.Biddings.Count());
                foreach (var pb in game.Biddings)
                {
                    Assert.Equal(game.Id, pb.Game.Id);
                    var player = pb.Player;
                    Assert.Contains(player, context.Players);
                }
            }
        }

        [Fact]
        async Task Update_Test()
        {
            var options = InitDB();
            //prepares the database with one instance of the context
            using (var context = new TarotContextStub(options))
            {
                context.Database.EnsureCreated();

                GameEntity game = await context.Games.Include(g => g.Biddings).ThenInclude(b => b.Player)
                                                .SingleOrDefaultAsync(g => g.Id == (long)5);
                Assert.Equal(new DateTime(2021, 5, 5), game.DateTime);
                Assert.Equal("FrenchTarotRules", game.Rules);
                Assert.Equal(87, game.TakerPoints);
                Assert.Equal(PetitResult.SavedAuBout, game.Petit);
                Assert.Equal(Poignée.Simple, game.Poignée);
                Assert.Equal(false, game.Excuse);
                Assert.Equal(true, game.TwentyOne);
                Assert.Equal(Chelem.AnnouncedSuccess, game.Chelem);

                var biddings = game.Biddings;
                Assert.Equal(5, biddings.Count());

                Assert.Single(biddings.Where(b => b.Player.Id == 10));
                Assert.Single(biddings.Where(b => b.Player.Id == 16));
                Assert.Single(biddings.Where(b => b.Player.Id == 9));
                Assert.Single(biddings.Where(b => b.Player.Id == 4));
                Assert.Single(biddings.Where(b => b.Player.Id == 2));

                game.DateTime = new DateTime(2021, 12, 25);
                game.Chelem = Chelem.AnnouncedFail;
                game.Excuse = true;
                game.Petit = PetitResult.NotOwned;
                game.Poignée = Poignée.None;
                game.Rules = "other";
                game.TakerPoints = 42;
                game.TwentyOne = false;
                var playerToRemove = await context.Set<PlayerBiddingEntity>().FindAsync((long)4, (long)5);
                game.Biddings.Remove(playerToRemove);

                Assert.NotNull(await context.FindAsync<PlayerEntity>((long)4));
                Assert.NotNull(await context.FindAsync<PlayerBiddingEntity>((long)4, (long)5));

                await context.SaveChangesAsync();
            }

            using (var context = new TarotContextStub(options))
            {
                context.Database.EnsureCreated();

                GameEntity game = await context.Games.Include(g => g.Biddings).ThenInclude(b => b.Player)
                                                .SingleOrDefaultAsync(g => g.Id == (long)5);
                Assert.Equal(new DateTime(2021, 12, 25), game.DateTime);
                Assert.Equal("other", game.Rules);
                Assert.Equal(42, game.TakerPoints);
                Assert.Equal(PetitResult.NotOwned, game.Petit);
                Assert.Equal(Poignée.None, game.Poignée);
                Assert.Equal(true, game.Excuse);
                Assert.Equal(false, game.TwentyOne);
                Assert.Equal(Chelem.AnnouncedFail, game.Chelem);

                var biddings = game.Biddings;
                Assert.Equal(4, biddings.Count());

                Assert.Empty(biddings.Where(p => p.Player.Id == 4));
                Assert.Single(biddings.Where(b => b.Player.Id == 10));
                Assert.Single(biddings.Where(b => b.Player.Id == 16));
                Assert.Single(biddings.Where(b => b.Player.Id == 9));
                Assert.Single(biddings.Where(b => b.Player.Id == 2));

                Assert.NotNull(await context.FindAsync<PlayerEntity>((long)4));
                Assert.Null(await context.FindAsync<PlayerSessionEntity>((long)4, (long)5));
            }
        }

        [Fact]
        async Task Delete_Test()
        {
            var options = InitDB();
            using (var context = new TarotContextStub(options))
            {
                context.Database.EnsureCreated();

                GameEntity game = await context.Games.Include(g => g.Biddings).ThenInclude(b => b.Player)
                                                .SingleOrDefaultAsync(g => g.Id == (long)5);
                Assert.Equal(new DateTime(2021, 5, 5), game.DateTime);
                Assert.Equal("FrenchTarotRules", game.Rules);
                Assert.Equal(87, game.TakerPoints);
                Assert.Equal(PetitResult.SavedAuBout, game.Petit);
                Assert.Equal(Poignée.Simple, game.Poignée);
                Assert.Equal(false, game.Excuse);
                Assert.Equal(true, game.TwentyOne);
                Assert.Equal(Chelem.AnnouncedSuccess, game.Chelem);

                var biddings = game.Biddings;
                Assert.Equal(5, biddings.Count());

                Assert.Single(biddings.Where(b => b.Player.Id == 10));
                Assert.Single(biddings.Where(b => b.Player.Id == 16));
                Assert.Single(biddings.Where(b => b.Player.Id == 9));
                Assert.Single(biddings.Where(b => b.Player.Id == 4));
                Assert.Single(biddings.Where(b => b.Player.Id == 2));

                Assert.NotNull(await context.FindAsync<PlayerEntity>((long)10));
                Assert.NotNull(await context.FindAsync<PlayerEntity>((long)16));
                Assert.NotNull(await context.FindAsync<PlayerEntity>((long)9));
                Assert.NotNull(await context.FindAsync<PlayerEntity>((long)4));
                Assert.NotNull(await context.FindAsync<PlayerEntity>((long)2));

                Assert.NotNull(await context.FindAsync<PlayerBiddingEntity>((long)10, (long)5));
                Assert.NotNull(await context.FindAsync<PlayerBiddingEntity>((long)16, (long)5));
                Assert.NotNull(await context.FindAsync<PlayerBiddingEntity>((long)9, (long)5));
                Assert.NotNull(await context.FindAsync<PlayerBiddingEntity>((long)4, (long)5));
                Assert.NotNull(await context.FindAsync<PlayerBiddingEntity>((long)2, (long)5));

                context.Games.Remove(game);

                await context.SaveChangesAsync();
            }

            using (var context = new TarotContextStub(options))
            {
                context.Database.EnsureCreated();

                GameEntity game = await context.Games.Include(g => g.Biddings).ThenInclude(b => b.Player)
                                                .SingleOrDefaultAsync(g => g.Id == (long)5);
                Assert.Null(game);

                Assert.NotNull(await context.FindAsync<PlayerEntity>((long)10));
                Assert.NotNull(await context.FindAsync<PlayerEntity>((long)16));
                Assert.NotNull(await context.FindAsync<PlayerEntity>((long)9));
                Assert.NotNull(await context.FindAsync<PlayerEntity>((long)4));
                Assert.NotNull(await context.FindAsync<PlayerEntity>((long)2));

                Assert.Null(await context.FindAsync<PlayerBiddingEntity>((long)10, (long)5));
                Assert.Null(await context.FindAsync<PlayerBiddingEntity>((long)16, (long)5));
                Assert.Null(await context.FindAsync<PlayerBiddingEntity>((long)9, (long)5));
                Assert.Null(await context.FindAsync<PlayerBiddingEntity>((long)4, (long)5));
                Assert.Null(await context.FindAsync<PlayerBiddingEntity>((long)2, (long)5));

                Assert.Empty(context.Set<PlayerBiddingEntity>().Where(pbe => pbe.Game.Id == (long)5));
            }
        }
    }
}
