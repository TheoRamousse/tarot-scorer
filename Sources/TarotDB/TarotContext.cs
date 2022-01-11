using System;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;

[assembly:InternalsVisibleTo("TarotDB_Tests")]
[assembly:InternalsVisibleTo("TarotDB_UT")]
[assembly:InternalsVisibleTo("DataManager_UT")]
namespace TarotDB
{
    class TarotContext : DbContext
    {
        public DbSet<PlayerEntity> Players { get; set; }

        public DbSet<SessionEntity> Sessions { get; set; }

        public DbSet<GameEntity> Games { get; set; }

        public TarotContext()
        { }

        public TarotContext(DbContextOptions<TarotContext> options)
            : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlite(@"Data Source=Tarot.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PlayerSessionEntity>().Property<long>("PlayerId");
            modelBuilder.Entity<PlayerSessionEntity>().Property<long>("SessionId");

            modelBuilder.Entity<PlayerSessionEntity>().HasKey("PlayerId", "SessionId");

            modelBuilder.Entity<PlayerSessionEntity>()
                .HasOne(ps => ps.Player)
                .WithMany(p => p.Sessions)
                .HasForeignKey("PlayerId");

            modelBuilder.Entity<PlayerSessionEntity>()
                .HasOne(ps => ps.Session)
                .WithMany(s => s.Players)
                .HasForeignKey("SessionId");


            modelBuilder.Entity<PlayerBiddingEntity>().Property<long>("PlayerId");
            modelBuilder.Entity<PlayerBiddingEntity>().Property<long>("GameId");
            modelBuilder.Entity<PlayerBiddingEntity>().HasKey("PlayerId", "GameId");

            modelBuilder.Entity<PlayerBiddingEntity>()
                .HasOne(pb => pb.Player)
                .WithMany(p => p.Games)
                .HasForeignKey("PlayerId");

            modelBuilder.Entity<PlayerBiddingEntity>()
                .HasOne(pb => pb.Game)
                .WithMany(g => g.Biddings)
                .HasForeignKey("GameId");

            modelBuilder.Entity<PlayerEntity>().HasData(
                new PlayerEntity { Id = -1, FirstName="Jane", LastName="Doe", NickName="", ImageName=""});
        }

        internal void LinkPlayerSession(PlayerEntity pe, SessionEntity se)
        {
            PlayerSessionEntity pse = new PlayerSessionEntity();
            pse.Player = pe;
            pse.Session = se;
            pe.Sessions.Add(pse);
            se.Players.Add(pse);
        }
    }
}
