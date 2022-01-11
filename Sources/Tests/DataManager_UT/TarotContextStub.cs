using System;
using Microsoft.EntityFrameworkCore;
using TarotDB;

namespace DataManager_UT
{
    class TarotContextStub : TarotContext
    {
        public TarotContextStub(DbContextOptions<TarotContext> options)
            : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            base.OnConfiguring(options);
            options.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PlayerEntity>().HasData(new PlayerEntity{ Id = 1, FirstName="Lenny", LastName="White" });
            modelBuilder.Entity<PlayerEntity>().HasData(new PlayerEntity{ Id = 2, FirstName="Chick", LastName="Corea" });
            modelBuilder.Entity<PlayerEntity>().HasData(new PlayerEntity{ Id = 3, FirstName="Stanley", LastName="Clarke" });
            modelBuilder.Entity<PlayerEntity>().HasData(new PlayerEntity{ Id = 4, FirstName="Jean-Luc", LastName="Ponty" });
            modelBuilder.Entity<PlayerEntity>().HasData(new PlayerEntity{ Id = 5, FirstName="Steve", LastName="Gadd" });
            modelBuilder.Entity<PlayerEntity>().HasData(new PlayerEntity{ Id = 6, FirstName="Tony", LastName="Williams" });
            modelBuilder.Entity<PlayerEntity>().HasData(new PlayerEntity{ Id = 7, FirstName="Ron", LastName="Carter" });
            modelBuilder.Entity<PlayerEntity>().HasData(new PlayerEntity{ Id = 8, FirstName="Miles", LastName="Davis" });
            modelBuilder.Entity<PlayerEntity>().HasData(new PlayerEntity{ Id = 9, FirstName="Wayne", LastName="Shorter" });
            modelBuilder.Entity<PlayerEntity>().HasData(new PlayerEntity{ Id = 10, FirstName="John", LastName="McLaughlin" });
            modelBuilder.Entity<PlayerEntity>().HasData(new PlayerEntity{ Id = 11, FirstName="Herbie", LastName="Hancock" });
            modelBuilder.Entity<PlayerEntity>().HasData(new PlayerEntity{ Id = 12, FirstName="Joe", LastName="Zawinul" });
            modelBuilder.Entity<PlayerEntity>().HasData(new PlayerEntity{ Id = 13, FirstName="Dave", LastName="Holland" });
            modelBuilder.Entity<PlayerEntity>().HasData(new PlayerEntity{ Id = 14, FirstName="Miroslav", LastName="Vitous" });
            modelBuilder.Entity<PlayerEntity>().HasData(new PlayerEntity{ Id = 15, FirstName="Jaco", LastName="Pastorius" });
            modelBuilder.Entity<PlayerEntity>().HasData(new PlayerEntity{ Id = 16, FirstName="Peter", LastName="Erskine" });

            modelBuilder.Entity<SessionEntity>().HasData(new SessionEntity {Id = 1, Name="Return To Forever", StartingTime = new DateTime(1972, 2, 2), EndingTime = new DateTime(2021, 2, 9) });
            modelBuilder.Entity<SessionEntity>().HasData(new SessionEntity {Id = 2, Name="In a silent way", StartingTime = new DateTime(1969, 2, 1), EndingTime = new DateTime(1969, 2, 28) });
            modelBuilder.Entity<SessionEntity>().HasData(new SessionEntity {Id = 3, Name="Weather Report", StartingTime = new DateTime(1970, 1, 1), EndingTime = new DateTime(1986, 12, 31) });

            modelBuilder.Entity<PlayerSessionEntity>().HasData(new { PlayerId = (long)1, SessionId = (long)1});
            modelBuilder.Entity<PlayerSessionEntity>().HasData(new { PlayerId = (long)2, SessionId = (long)1});
            modelBuilder.Entity<PlayerSessionEntity>().HasData(new { PlayerId = (long)3, SessionId = (long)1});
            modelBuilder.Entity<PlayerSessionEntity>().HasData(new { PlayerId = (long)4, SessionId = (long)1});
            modelBuilder.Entity<PlayerSessionEntity>().HasData(new { PlayerId = (long)5, SessionId = (long)1});
            modelBuilder.Entity<PlayerSessionEntity>().HasData(new { PlayerId = (long)8, SessionId = (long)2});
            modelBuilder.Entity<PlayerSessionEntity>().HasData(new { PlayerId = (long)9, SessionId = (long)2});
            modelBuilder.Entity<PlayerSessionEntity>().HasData(new { PlayerId = (long)10, SessionId = (long)2});
            modelBuilder.Entity<PlayerSessionEntity>().HasData(new { PlayerId = (long)11, SessionId = (long)2});
            modelBuilder.Entity<PlayerSessionEntity>().HasData(new { PlayerId = (long)12, SessionId = (long)2});
            modelBuilder.Entity<PlayerSessionEntity>().HasData(new { PlayerId = (long)13, SessionId = (long)2});
            modelBuilder.Entity<PlayerSessionEntity>().HasData(new { PlayerId = (long)6, SessionId = (long)2});
            modelBuilder.Entity<PlayerSessionEntity>().HasData(new { PlayerId = (long)9, SessionId = (long)3});
            modelBuilder.Entity<PlayerSessionEntity>().HasData(new { PlayerId = (long)12, SessionId = (long)3});
            modelBuilder.Entity<PlayerSessionEntity>().HasData(new { PlayerId = (long)14, SessionId = (long)3});
            modelBuilder.Entity<PlayerSessionEntity>().HasData(new { PlayerId = (long)15, SessionId = (long)3});
            modelBuilder.Entity<PlayerSessionEntity>().HasData(new { PlayerId = (long)16, SessionId = (long)3});

            modelBuilder.Entity<GameEntity>().HasData(new GameEntity { Id = 1, DateTime = new DateTime(2021, 1, 1), Rules = "FrenchTarotRules", TakerPoints = 49, Petit = PetitResult.SavedAuBout, Poignée = Poignée.Simple, Excuse = true, TwentyOne = false, Chelem = Chelem.Unknown });
            modelBuilder.Entity<GameEntity>().HasData(new GameEntity { Id = 2, DateTime = new DateTime(2021, 2, 2), Rules = "FrenchTarotRules", TakerPoints = 45, Petit = PetitResult.LostAuBout, Poignée = Poignée.None, Excuse = true, TwentyOne = true, Chelem = Chelem.Unknown });
            modelBuilder.Entity<GameEntity>().HasData(new GameEntity { Id = 3, DateTime = new DateTime(2021, 3, 3), Rules = "FrenchTarotRules", TakerPoints = 44, Petit = PetitResult.SavedAuBout, Poignée = Poignée.Simple, Excuse = false, TwentyOne = false, Chelem = Chelem.Unknown });
            modelBuilder.Entity<GameEntity>().HasData(new GameEntity { Id = 4, DateTime = new DateTime(2021, 4, 4), Rules = "FrenchTarotRules", TakerPoints = 52, Petit = PetitResult.Saved, Poignée = Poignée.SimpleDefense, Excuse = true, TwentyOne = false, Chelem = Chelem.Unknown });
            modelBuilder.Entity<GameEntity>().HasData(new GameEntity { Id = 5, DateTime = new DateTime(2021, 5, 5), Rules = "FrenchTarotRules", TakerPoints = 87, Petit = PetitResult.SavedAuBout, Poignée = Poignée.Simple, Excuse = false, TwentyOne = true, Chelem = Chelem.AnnouncedSuccess });

            modelBuilder.Entity<PlayerBiddingEntity>().HasData(new { Bidding = Bidding.GardeSans, GameId = (long)1, PlayerId = (long)1});
            modelBuilder.Entity<PlayerBiddingEntity>().HasData(new { Bidding = Bidding.Opponent, GameId = (long)1, PlayerId = (long)2});
            modelBuilder.Entity<PlayerBiddingEntity>().HasData(new { Bidding = Bidding.Opponent, GameId = (long)1, PlayerId = (long)3});

            modelBuilder.Entity<PlayerBiddingEntity>().HasData(new { Bidding = Bidding.Garde, GameId = (long)2, PlayerId = (long)4});
            modelBuilder.Entity<PlayerBiddingEntity>().HasData(new { Bidding = Bidding.Opponent, GameId = (long)2, PlayerId = (long)5});
            modelBuilder.Entity<PlayerBiddingEntity>().HasData(new { Bidding = Bidding.Opponent, GameId = (long)2, PlayerId = (long)6});
            modelBuilder.Entity<PlayerBiddingEntity>().HasData(new { Bidding = Bidding.Opponent, GameId = (long)2, PlayerId = (long)7});

            modelBuilder.Entity<PlayerBiddingEntity>().HasData(new { Bidding = Bidding.Petite, GameId = (long)3, PlayerId = (long)8});
            modelBuilder.Entity<PlayerBiddingEntity>().HasData(new { Bidding = Bidding.KingCalled, GameId = (long)3, PlayerId = (long)9});
            modelBuilder.Entity<PlayerBiddingEntity>().HasData(new { Bidding = Bidding.Opponent, GameId = (long)3, PlayerId = (long)10});
            modelBuilder.Entity<PlayerBiddingEntity>().HasData(new { Bidding = Bidding.Opponent, GameId = (long)3, PlayerId = (long)11});
            modelBuilder.Entity<PlayerBiddingEntity>().HasData(new { Bidding = Bidding.Opponent, GameId = (long)3, PlayerId = (long)12});

            modelBuilder.Entity<PlayerBiddingEntity>().HasData(new { Bidding = Bidding.GardeContre, GameId = (long)4, PlayerId = (long)13});
            modelBuilder.Entity<PlayerBiddingEntity>().HasData(new { Bidding = Bidding.Opponent, GameId = (long)4, PlayerId = (long)14});
            modelBuilder.Entity<PlayerBiddingEntity>().HasData(new { Bidding = Bidding.Opponent, GameId = (long)4, PlayerId = (long)1});
            modelBuilder.Entity<PlayerBiddingEntity>().HasData(new { Bidding = Bidding.Opponent, GameId = (long)4, PlayerId = (long)16});

            modelBuilder.Entity<PlayerBiddingEntity>().HasData(new { Bidding = Bidding.GardeContre, GameId = (long)5, PlayerId = (long)10});
            modelBuilder.Entity<PlayerBiddingEntity>().HasData(new { Bidding = Bidding.KingCalled, GameId = (long)5, PlayerId = (long)16});
            modelBuilder.Entity<PlayerBiddingEntity>().HasData(new { Bidding = Bidding.Opponent, GameId = (long)5, PlayerId = (long)7});
            modelBuilder.Entity<PlayerBiddingEntity>().HasData(new { Bidding = Bidding.Opponent, GameId = (long)5, PlayerId = (long)4});
            modelBuilder.Entity<PlayerBiddingEntity>().HasData(new { Bidding = Bidding.Opponent, GameId = (long)5, PlayerId = (long)2});

        }
    }
}
