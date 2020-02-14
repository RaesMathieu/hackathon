using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ThermoBet.SQLServer.Migrations
{
    public partial class AddResultTimeUtcOnTournament : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ResultTimeUtc",
                table: "Tournaments",
                nullable: false,
                defaultValue: new DateTime(9999, 12, 31, 23, 59, 59, 999, DateTimeKind.Unspecified).AddTicks(9999));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResultTimeUtc",
                table: "Tournaments");
        }
    }
}
