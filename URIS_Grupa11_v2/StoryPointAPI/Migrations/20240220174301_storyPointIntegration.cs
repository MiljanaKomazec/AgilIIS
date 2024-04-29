using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoryPointAPI.Migrations
{
    public partial class storyPointIntegration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserStoryRootId",
                table: "StoryPoints",
                type: "uniqueidentifier",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserStoryRootId",
                table: "StoryPoints");
        }
    }
}
