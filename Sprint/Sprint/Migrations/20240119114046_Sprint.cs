using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sprint.Migrations
{
    public partial class Sprint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Backlog",
                columns: table => new
                {
                    BacklogId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameBacklog = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Backlog", x => x.BacklogId);
                });

            migrationBuilder.CreateTable(
                name: "PhaseOfBacklogItem",
                columns: table => new
                {
                    POBIId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameOfPOBI = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhaseOfBacklogItem", x => x.POBIId);
                });

            migrationBuilder.CreateTable(
                name: "Sprint",
                columns: table => new
                {
                    SprintId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DurationSprint = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartOfSprint = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndOfSprint = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sprint", x => x.SprintId);
                });

            migrationBuilder.CreateTable(
                name: "BacklogItem",
                columns: table => new
                {
                    BacklogItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TimeAddedBacklogItem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BacklogId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SprintId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    POBIId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BacklogItem", x => x.BacklogItemId);
                    table.ForeignKey(
                        name: "FK_BacklogItem_Backlog_BacklogId",
                        column: x => x.BacklogId,
                        principalTable: "Backlog",
                        principalColumn: "BacklogId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BacklogItem_PhaseOfBacklogItem_POBIId",
                        column: x => x.POBIId,
                        principalTable: "PhaseOfBacklogItem",
                        principalColumn: "POBIId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BacklogItem_Sprint_SprintId",
                        column: x => x.SprintId,
                        principalTable: "Sprint",
                        principalColumn: "SprintId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Backlog",
                columns: new[] { "BacklogId", "NameBacklog" },
                values: new object[,]
                {
                    { new Guid("937ecfaa-58fe-4ac0-88b3-fa2810c67bfc"), "Project 1" },
                    { new Guid("db7e7a04-8082-4ebb-88b0-d05f9dae4243"), "Project 2" }
                });

            migrationBuilder.InsertData(
                table: "PhaseOfBacklogItem",
                columns: new[] { "POBIId", "NameOfPOBI" },
                values: new object[,]
                {
                    { new Guid("290591c5-7054-4b03-9e40-42c5e1eadde2"), "Done" },
                    { new Guid("ed6e1f21-748a-4801-9a94-85e52b8fb256"), "Waiting" }
                });

            migrationBuilder.InsertData(
                table: "Sprint",
                columns: new[] { "SprintId", "DurationSprint", "EndOfSprint", "StartOfSprint" },
                values: new object[,]
                {
                    { new Guid("1c7ea607-8ddb-493a-87fa-4bf5893e965b"), "2 weeks", new DateTime(2020, 12, 30, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 12, 15, 9, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("6a411c13-a195-48f7-8dbd-67596c3974c0"), "2 weeks", new DateTime(2020, 12, 30, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 12, 15, 9, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "BacklogItem",
                columns: new[] { "BacklogItemId", "BacklogId", "POBIId", "SprintId", "TimeAddedBacklogItem" },
                values: new object[] { new Guid("45d01a65-a992-45cc-b670-1ffdd179a8f2"), new Guid("937ecfaa-58fe-4ac0-88b3-fa2810c67bfc"), new Guid("290591c5-7054-4b03-9e40-42c5e1eadde2"), new Guid("1c7ea607-8ddb-493a-87fa-4bf5893e965b"), "11:00 PM" });

            migrationBuilder.InsertData(
                table: "BacklogItem",
                columns: new[] { "BacklogItemId", "BacklogId", "POBIId", "SprintId", "TimeAddedBacklogItem" },
                values: new object[] { new Guid("6edbc9cb-32bb-48a3-90b6-fa070eede946"), new Guid("937ecfaa-58fe-4ac0-88b3-fa2810c67bfc"), new Guid("290591c5-7054-4b03-9e40-42c5e1eadde2"), new Guid("1c7ea607-8ddb-493a-87fa-4bf5893e965b"), "11:00 PM" });

            migrationBuilder.CreateIndex(
                name: "IX_BacklogItem_BacklogId",
                table: "BacklogItem",
                column: "BacklogId");

            migrationBuilder.CreateIndex(
                name: "IX_BacklogItem_POBIId",
                table: "BacklogItem",
                column: "POBIId");

            migrationBuilder.CreateIndex(
                name: "IX_BacklogItem_SprintId",
                table: "BacklogItem",
                column: "SprintId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BacklogItem");

            migrationBuilder.DropTable(
                name: "Backlog");

            migrationBuilder.DropTable(
                name: "PhaseOfBacklogItem");

            migrationBuilder.DropTable(
                name: "Sprint");
        }
    }
}
