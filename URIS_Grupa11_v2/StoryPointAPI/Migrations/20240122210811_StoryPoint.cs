using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoryPointAPI.Migrations
{
    public partial class StoryPoint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StoryPoints",
                columns: table => new
                {
                    StoryPointId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ValueStoryPoint = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoryPoints", x => x.StoryPointId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StoryPoints");
        }
    }
}
