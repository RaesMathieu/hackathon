using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ThermoBet.SQLServer.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tournaments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 250, nullable: false),
                    Description = table.Column<string>(maxLength: 2500, nullable: true),
                    ImgUrl = table.Column<string>(maxLength: 255, nullable: true),
                    StartTimeUtc = table.Column<DateTime>(nullable: false),
                    EndTimeUtc = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tournaments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(maxLength: 50, nullable: false),
                    HashPassword = table.Column<string>(maxLength: 32, nullable: false),
                    IsAdmin = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Markets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 250, nullable: false),
                    ImgUrl = table.Column<string>(maxLength: 255, nullable: true),
                    TournamentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Markets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Markets_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Selections",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Odds = table.Column<decimal>(type: "decimal(3,2)", nullable: false),
                    Name = table.Column<string>(maxLength: 250, nullable: false),
                    ImgUrl = table.Column<string>(maxLength: 250, nullable: true),
                    Result = table.Column<bool>(nullable: true),
                    MarketId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Selections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Selections_Markets_MarketId",
                        column: x => x.MarketId,
                        principalTable: "Markets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Bet",
                columns: table => new
                {
                    TournamentId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    MarketId = table.Column<int>(nullable: false),
                    SelectionId = table.Column<int>(nullable: false),
                    DateUtc = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bet", x => new { x.MarketId, x.TournamentId, x.UserId });
                    table.ForeignKey(
                        name: "FK_Bet_Markets_MarketId",
                        column: x => x.MarketId,
                        principalTable: "Markets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bet_Selections_SelectionId",
                        column: x => x.SelectionId,
                        principalTable: "Selections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bet_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bet_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bet_SelectionId",
                table: "Bet",
                column: "SelectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Bet_TournamentId",
                table: "Bet",
                column: "TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_Bet_UserId",
                table: "Bet",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Markets_TournamentId",
                table: "Markets",
                column: "TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_Selections_MarketId",
                table: "Selections",
                column: "MarketId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Login_HashPassword",
                table: "Users",
                columns: new[] { "Login", "HashPassword" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bet");

            migrationBuilder.DropTable(
                name: "Selections");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Markets");

            migrationBuilder.DropTable(
                name: "Tournaments");
        }
    }
}
