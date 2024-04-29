using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace URIS_Grupa11.Migrations
{
    public partial class Team : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    TeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeamName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeamDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CalendarId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.TeamId);
                });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "TeamId", "CalendarId", "TeamDescription", "TeamName", "UserId" },
                values: new object[] { new Guid("550e8400-e29b-41d4-a716-446655440000"), new Guid("250e8400-e29b-41d4-a716-446655440000"), "Razvoj Agilisa", "Grupa11", new Guid("150e8400-e29b-41d4-a716-446655440000") });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "TeamId", "CalendarId", "TeamDescription", "TeamName", "UserId" },
                values: new object[] { new Guid("550e8400-e29b-41d4-a716-446655440001"), new Guid("450e8400-e29b-41d4-a716-446655440000"), "Modifikovanje web aplikacije", "Grupa1", new Guid("350e8400-e29b-41d4-a716-446655440000") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Teams");
        }
    }
}
