using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TarotDB.Migrations
{
    public partial class migr1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DateTime = table.Column<DateTime>(nullable: false),
                    TakerPoints = table.Column<int>(nullable: false),
                    Excuse = table.Column<bool>(nullable: true),
                    TwentyOne = table.Column<bool>(nullable: true),
                    Petit = table.Column<int>(nullable: false),
                    Poignée = table.Column<int>(nullable: false),
                    Chelem = table.Column<int>(nullable: false),
                    Rules = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    NickName = table.Column<string>(nullable: true),
                    ImageName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    StartingTime = table.Column<DateTime>(nullable: true),
                    EndingTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlayerBiddingEntity",
                columns: table => new
                {
                    PlayerId = table.Column<long>(nullable: false),
                    GameId = table.Column<long>(nullable: false),
                    Bidding = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerBiddingEntity", x => new { x.PlayerId, x.GameId });
                    table.ForeignKey(
                        name: "FK_PlayerBiddingEntity_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerBiddingEntity_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayerSessionEntity",
                columns: table => new
                {
                    PlayerId = table.Column<long>(nullable: false),
                    SessionId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerSessionEntity", x => new { x.PlayerId, x.SessionId });
                    table.ForeignKey(
                        name: "FK_PlayerSessionEntity_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerSessionEntity_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Sessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "FirstName", "ImageName", "LastName", "NickName" },
                values: new object[] { -1L, "Jane", "", "Doe", "" });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerBiddingEntity_GameId",
                table: "PlayerBiddingEntity",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerSessionEntity_SessionId",
                table: "PlayerSessionEntity",
                column: "SessionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerBiddingEntity");

            migrationBuilder.DropTable(
                name: "PlayerSessionEntity");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Sessions");
        }
    }
}
