using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommentService.Migrations
{
    public partial class CommentDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    CommentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateComment = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TextComment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserStoryRootId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.CommentId);
                });

            migrationBuilder.InsertData(
                table: "Comment",
                columns: new[] { "CommentId", "DateComment", "TextComment", "UserId", "UserStoryRootId" },
                values: new object[] { new Guid("1c7ea607-8ddb-493a-87fa-4bf5893e965b"), new DateTime(2023, 11, 15, 14, 0, 0, 0, DateTimeKind.Unspecified), "Dobro napisano.", new Guid("cbea5366-bf13-40ab-a518-c9b6f81bbfdf"), new Guid("05da16d0-6c28-4206-b770-e458afd0e2d2") });

            migrationBuilder.InsertData(
                table: "Comment",
                columns: new[] { "CommentId", "DateComment", "TextComment", "UserId", "UserStoryRootId" },
                values: new object[] { new Guid("6a411c13-a195-48f7-8dbd-67596c3974c0"), new DateTime(2021, 6, 27, 8, 0, 0, 0, DateTimeKind.Unspecified), "Potrebno je proširiti ovu korisničku priču.", new Guid("cbea5366-bf13-40ab-a518-c9b6f81bbfdf"), new Guid("6cf6c4c5-40bc-4c67-b2a6-5d61959d6b84") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comment");
        }
    }
}
