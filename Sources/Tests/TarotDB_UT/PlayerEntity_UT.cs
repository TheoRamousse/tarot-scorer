using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using TarotDB;
using Xunit;

namespace TarotDB_UT
{
    public class PlayerEntity_UT
    {
        [Fact]
        public async Task Read_Test()
        {
            //connection must be opened to use In-memory database
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<TarotContext>()
                .UseSqlite(connection)
                .Options;

            //prepares the database with one instance of the context
            using (var context = new TarotContextStub(options))
            {
                context.Database.EnsureCreated();

                Assert.Equal(17, await context.Players.CountAsync());

                var wayne = await context.Players.FindAsync((long)9);
                Assert.Equal("Wayne", wayne.FirstName);

                var playersWith_on = context.Players.Where(p => p.FirstName.Contains("on"));
                Assert.Equal(2, await playersWith_on.CountAsync());
            }
        }

        [Theory]
        [InlineData(18, "Thomas Wright", "Waller", "Fats", "fats.jpg")]
        [InlineData(18, "", "Waller", "Fats", "fats.jpg")]
        [InlineData(18, "  ", "Waller", "Fats", "fats.jpg")]
        [InlineData(18, null, "Waller", "Fats", "fats.jpg")]
        [InlineData(18, "Thomas Wright", "", "Fats", "fats.jpg")]
        [InlineData(18, "Thomas Wright", "   ", "Fats", "fats.jpg")]
        [InlineData(18, "Thomas Wright", null, "Fats", "fats.jpg")]
        [InlineData(18, "Thomas Wright", "Waller", "", "fats.jpg")]
        [InlineData(18, "Thomas Wright", "Waller", "  ", "fats.jpg")]
        [InlineData(18, "Thomas Wright", "Waller", null, "fats.jpg")]
        [InlineData(18, "", "", "Fats", "fats.jpg")]
        [InlineData(18, null, "", "Fats", "fats.jpg")]
        [InlineData(18, "", null, "Fats", "fats.jpg")]
        [InlineData(18, "Thomas Wright", "", "", "fats.jpg")]
        [InlineData(18, "", "Waller", "", "fats.jpg")]
        [InlineData(18, null, "Waller", null, "fats.jpg")]
        [InlineData(18, null, null, null, "fats.jpg")]
        [InlineData(18, "", null, null, "fats.jpg")]
        [InlineData(18, "  ", null, null, "fats.jpg")]
        [InlineData(18, null, "", null, "fats.jpg")]
        [InlineData(18, "", "", null, "fats.jpg")]
        [InlineData(18, "  ", "", null, "fats.jpg")]
        [InlineData(18, null, null, "", "fats.jpg")]
        [InlineData(18, "", null, "", "fats.jpg")]
        [InlineData(18, "  ", null, "", "fats.jpg")]
        [InlineData(18, null, "", "", "fats.jpg")]
        [InlineData(18, "", "", "", "fats.jpg")]
        [InlineData(18, "  ", "", "", "fats.jpg")]
        public async Task Add_Test(int expectedNbPlayersAfterInsertion, string firstname, string lastname, string nickname, string image)
        {
            //connection must be opened to use In-memory database
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<TarotContext>()
                .UseSqlite(connection)
                .Options;

            //prepares the database with one instance of the context
            using (var context = new TarotContextStub(options))
            {
                context.Database.EnsureCreated();

                Assert.Equal(expectedNbPlayersAfterInsertion-1, context.Players.Count());

                context.Players.Add(new PlayerEntity
                {
                    FirstName = firstname,
                    LastName = lastname,
                    NickName = nickname,
                    ImageName = image
                });

                await context.SaveChangesAsync();
            }

            using(TarotContextStub context = new TarotContextStub(options))
            {
                Assert.Equal(expectedNbPlayersAfterInsertion, context.Players.Count());

                Assert.Equal(1, context.Players.Where(p => p.FirstName == firstname
                                                                && p.LastName == lastname
                                                                && p.NickName == nickname
                                                                && p.ImageName == image).Count());
            }
        }

        [Theory]
        [InlineData("Thomas Wright", "Waller", "Fats", "fats.jpg",
                    "Oscar", "Peterson", null, "oscar.jpg")]
        public async Task Modify_Test(string firstname, string lastname, string nickname, string image,
                                      string firstname2, string lastname2, string nickname2, string image2)
        {
            //connection must be opened to use In-memory database
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<TarotContext>()
                .UseSqlite(connection)
                .Options;

            //prepares the database with one instance of the context
            using (var context = new TarotContextStub(options))
            {
                context.Database.EnsureCreated();

                context.Players.Add(new PlayerEntity
                {
                    FirstName = firstname,
                    LastName = lastname,
                    NickName = nickname,
                    ImageName = image
                });

                await context.SaveChangesAsync();
            }

            long playerId = -1;

            using(TarotContextStub context = new TarotContextStub(options))
            {
                var players = context.Players.Where(p => p.FirstName == firstname
                                                                && p.LastName == lastname
                                                                && p.NickName == nickname
                                                                && p.ImageName == image);

                var playersAfter = context.Players.Where(p => p.FirstName == firstname2
                                                                && p.LastName == lastname2
                                                                && p.NickName == nickname2
                                                                && p.ImageName == image2);
                Assert.Equal(1, players.Count());
                Assert.Equal(0, playersAfter.Count());

                var player = players.Single();
                playerId = player.Id;
                player.FirstName = firstname2;
                player.LastName = lastname2;
                player.NickName = nickname2;
                player.ImageName = image2;

                await context.SaveChangesAsync();
            }

            using(TarotContextStub context = new TarotContextStub(options))
            {
                var players = context.Players.Where(p => p.FirstName == firstname
                                                                && p.LastName == lastname
                                                                && p.NickName == nickname
                                                                && p.ImageName == image);

                var playersAfter = context.Players.Where(p => p.FirstName == firstname2
                                                                && p.LastName == lastname2
                                                                && p.NickName == nickname2
                                                                && p.ImageName == image2);
                Assert.Equal(0, players.Count());
                Assert.Equal(1, playersAfter.Count());

                Assert.Equal(playerId, playersAfter.Single().Id);
            }
        }

        [Theory]
        [InlineData("Thomas Wright", "Waller", "Fats", "fats.jpg")]
        public async Task Delete_Test(string firstname, string lastname, string nickname, string image)
        {
            //connection must be opened to use In-memory database
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<TarotContext>()
                .UseSqlite(connection)
                .Options;

            //prepares the database with one instance of the context
            using (var context = new TarotContextStub(options))
            {
                context.Database.EnsureCreated();

                context.Players.Add(new PlayerEntity
                {
                    FirstName = firstname,
                    LastName = lastname,
                    NickName = nickname,
                    ImageName = image
                });

                await context.SaveChangesAsync();
            }

            long playerId = -1;

            using(TarotContextStub context = new TarotContextStub(options))
            {
                var players = context.Players.Where(p => p.FirstName == firstname
                                                                && p.LastName == lastname
                                                                && p.NickName == nickname
                                                                && p.ImageName == image);

                Assert.Equal(1, players.Count());

                var player = players.Single();
                playerId = player.Id;

                context.Players.Remove(player);

                await context.SaveChangesAsync();
            }

            using(TarotContextStub context = new TarotContextStub(options))
            {
                var players = context.Players.Where(p => p.FirstName == firstname
                                                                && p.LastName == lastname
                                                                && p.NickName == nickname
                                                                && p.ImageName == image);

                Assert.Equal(0, players.Count());
                Assert.Null(await context.Players.FindAsync(playerId));
            }
        }
    }
}
