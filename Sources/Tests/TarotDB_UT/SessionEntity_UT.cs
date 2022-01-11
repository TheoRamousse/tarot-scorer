using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using TarotDB;
using Xunit;

namespace TarotDB_UT
{
    public class SessionEntity_UT
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

        [Fact]
        public async Task Read_Test()
        {
            //prepares the database with one instance of the context
            using (var context = new TarotContextStub(InitDB()))
            {
                context.Database.EnsureCreated();

                SessionEntity session = await context.Sessions.Include(s => s.Players).ThenInclude(s => s.Player)
                                                .SingleOrDefaultAsync(s => s.Id == (long)1);
                Assert.Equal("Return To Forever", session.Name);
                Assert.Equal(new DateTime(1972, 2, 2), session.StartingTime);
                Assert.Equal(new DateTime(2021, 2, 9), session.EndingTime);

                var players = session.Players;
                Assert.Equal(5, players.Count());

                Assert.Single(players.Where(p => p.Player.Id == 3));
            }
        }

        [Theory]
        [MemberData(nameof(TestData_Session.Sessions), MemberType = typeof(TestData_Session))]
        async Task Add_Test(string name, DateTime? start, DateTime? end, params PlayerEntity[] players)
        {
            var options = InitDB();
            //prepares the database with one instance of the context
            using (var context = new TarotContextStub(options))
            {
                context.Database.EnsureCreated();

                var sessionsWithName = context.Sessions.Where(s => s.Name == name);
                Assert.Empty(sessionsWithName);

                SessionEntity session = new SessionEntity
                {
                    Name = name,
                    StartingTime = start,
                    EndingTime = end,
                };

                await context.Sessions.AddAsync(session);

                await context.AddRangeAsync(players.Select(p => new PlayerSessionEntity
                {
                    Player = context.Players.Find(p.Id) != null ? context.Players.Find(p.Id) : p,
                    Session = session
                }));

                await context.SaveChangesAsync();
            }

            using (var context = new TarotContextStub(options))
            {
                context.Database.EnsureCreated();

                var sessionsWithName = context.Sessions.Include(s => s.Players).ThenInclude(s => s.Player)
                                                .Where(s => s.Name == name);
                Assert.Single(sessionsWithName);

                SessionEntity session = sessionsWithName.Single();
                Assert.Equal(name, session.Name);
                Assert.Equal(start, session.StartingTime);
                Assert.Equal(end, session.EndingTime);

                Assert.Equal(players.Count(), session.Players.Count());
                foreach(var ps in session.Players)
                {
                    Assert.Equal(session.Id, ps.Session.Id);
                    var player = ps.Player;
                    Assert.Contains(player, context.Players);
                }
            }
        }

        [Fact]
        async Task Update_Test()
        {
            var options = InitDB();
            using (var context = new TarotContextStub(options))
            {
                context.Database.EnsureCreated();

                SessionEntity session = await context.Sessions.Include(s => s.Players).ThenInclude(s => s.Player)
                                                .SingleOrDefaultAsync(s => s.Id == (long)1);
                Assert.Equal("Return To Forever", session.Name);
                Assert.Equal(new DateTime(1972, 2, 2), session.StartingTime);
                Assert.Equal(new DateTime(2021, 2, 9), session.EndingTime);

                var players = session.Players;
                Assert.Equal(5, players.Count());

                Assert.Single(players.Where(p => p.Player.Id == 3));

                session.Name = "original Return To Forever";
                session.StartingTime = new DateTime(1971, 2, 3);
                session.EndingTime = null;
                session.Players.Remove(session.Players.Single(pse => pse.Player.Id == (long)3));

                Assert.NotNull(await context.FindAsync<PlayerEntity>((long)3));
                Assert.NotNull(await context.FindAsync<PlayerSessionEntity>((long)3, (long)1));

                await context.SaveChangesAsync();
            }

            using (var context = new TarotContextStub(options))
            {
                context.Database.EnsureCreated();

                SessionEntity session = await context.Sessions.Include(s => s.Players).ThenInclude(s => s.Player)
                                                .SingleOrDefaultAsync(s => s.Id == (long)1);
                Assert.Equal("original Return To Forever", session.Name);
                Assert.Equal(new DateTime(1971, 2, 3), session.StartingTime);
                Assert.Null(session.EndingTime);

                var players = session.Players;
                Assert.Equal(4, players.Count());

                Assert.Empty(players.Where(p => p.Player.Id == 3));

                Assert.NotNull(await context.FindAsync<PlayerEntity>((long)3));
                Assert.Null(await context.FindAsync<PlayerSessionEntity>((long)3, (long)1));


            }
        }

        [Fact]
        async Task Delete_Test()
        {
            var options = InitDB();
            using (var context = new TarotContextStub(options))
            {
                context.Database.EnsureCreated();

                SessionEntity session = await context.Sessions.Include(s => s.Players).ThenInclude(s => s.Player)
                                                .SingleOrDefaultAsync(s => s.Id == (long)1);
                Assert.Equal("Return To Forever", session.Name);
                Assert.Equal(new DateTime(1972, 2, 2), session.StartingTime);
                Assert.Equal(new DateTime(2021, 2, 9), session.EndingTime);

                var players = session.Players;
                Assert.Equal(5, players.Count());

                Assert.Single(players.Where(p => p.Player.Id == 1));
                Assert.Single(players.Where(p => p.Player.Id == 2));
                Assert.Single(players.Where(p => p.Player.Id == 3));
                Assert.Single(players.Where(p => p.Player.Id == 4));
                Assert.Single(players.Where(p => p.Player.Id == 5));

                Assert.NotNull(await context.FindAsync<PlayerEntity>((long)1));
                Assert.NotNull(await context.FindAsync<PlayerEntity>((long)2));
                Assert.NotNull(await context.FindAsync<PlayerEntity>((long)3));
                Assert.NotNull(await context.FindAsync<PlayerEntity>((long)4));
                Assert.NotNull(await context.FindAsync<PlayerEntity>((long)5));

                Assert.NotNull(await context.FindAsync<PlayerSessionEntity>((long)1, (long)1));
                Assert.NotNull(await context.FindAsync<PlayerSessionEntity>((long)2, (long)1));
                Assert.NotNull(await context.FindAsync<PlayerSessionEntity>((long)3, (long)1));
                Assert.NotNull(await context.FindAsync<PlayerSessionEntity>((long)4, (long)1));
                Assert.NotNull(await context.FindAsync<PlayerSessionEntity>((long)5, (long)1));

                context.Sessions.Remove(session);

                await context.SaveChangesAsync();
            }

            using (var context = new TarotContextStub(options))
            {
                context.Database.EnsureCreated();

                SessionEntity session = await context.Sessions.Include(s => s.Players).ThenInclude(s => s.Player)
                                                .SingleOrDefaultAsync(s => s.Id == (long)1);
                Assert.Null(session);

                Assert.NotNull(await context.FindAsync<PlayerEntity>((long)1));
                Assert.NotNull(await context.FindAsync<PlayerEntity>((long)2));
                Assert.NotNull(await context.FindAsync<PlayerEntity>((long)3));
                Assert.NotNull(await context.FindAsync<PlayerEntity>((long)4));
                Assert.NotNull(await context.FindAsync<PlayerEntity>((long)5));

                Assert.Empty(context.Set<PlayerSessionEntity>().Where(pse => pse.Session.Id == (long)1));
                Assert.Null(await context.FindAsync<PlayerSessionEntity>((long)1, (long)1));
                Assert.Null(await context.FindAsync<PlayerSessionEntity>((long)2, (long)1));
                Assert.Null(await context.FindAsync<PlayerSessionEntity>((long)3, (long)1));
                Assert.Null(await context.FindAsync<PlayerSessionEntity>((long)4, (long)1));
                Assert.Null(await context.FindAsync<PlayerSessionEntity>((long)5, (long)1));
            }
        }

    }
}
