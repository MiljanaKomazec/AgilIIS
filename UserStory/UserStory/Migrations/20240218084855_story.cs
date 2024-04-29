using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserStory.Migrations
{
    public partial class story : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PrioritetizationParameters",
                columns: table => new
                {
                    PrioritetizationParameterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ValueForCustomerPP = table.Column<int>(type: "int", nullable: false),
                    CostPP = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrioritetizationParameters", x => x.PrioritetizationParameterId);
                });

            migrationBuilder.CreateTable(
                name: "UserStories",
                columns: table => new
                {
                    UserStoryRootId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TextUserStory = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PartOfEpic = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PrioritetizationParameterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BacklogId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SprintId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserStories", x => x.UserStoryRootId);
                    table.ForeignKey(
                        name: "FK_UserStories_PrioritetizationParameters_PrioritetizationParameterId",
                        column: x => x.PrioritetizationParameterId,
                        principalTable: "PrioritetizationParameters",
                        principalColumn: "PrioritetizationParameterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Functionallities",
                columns: table => new
                {
                    FunctionalityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TextFunctionality = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UserStoryRootId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SprintId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Functionallities", x => x.FunctionalityId);
                    table.ForeignKey(
                        name: "FK_Functionallities_UserStories_UserStoryRootId",
                        column: x => x.UserStoryRootId,
                        principalTable: "UserStories",
                        principalColumn: "UserStoryRootId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    TaskId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TextTask = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FunctionalityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SprintId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.TaskId);
                    table.ForeignKey(
                        name: "FK_Tasks_Functionallities_FunctionalityId",
                        column: x => x.FunctionalityId,
                        principalTable: "Functionallities",
                        principalColumn: "FunctionalityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "PrioritetizationParameters",
                columns: new[] { "PrioritetizationParameterId", "CostPP", "ValueForCustomerPP" },
                values: new object[] { new Guid("1c68a0db-ed8c-446d-a0ba-2f00e9df8c4c"), 250.30m, 25 });

            migrationBuilder.InsertData(
                table: "PrioritetizationParameters",
                columns: new[] { "PrioritetizationParameterId", "CostPP", "ValueForCustomerPP" },
                values: new object[] { new Guid("83988e22-a297-4158-b829-ef5df2344a3f"), 150.60m, 10 });

            migrationBuilder.InsertData(
                table: "UserStories",
                columns: new[] { "UserStoryRootId", "BacklogId", "PartOfEpic", "PrioritetizationParameterId", "SprintId", "TextUserStory" },
                values: new object[] { new Guid("05da16d0-6c28-4206-b770-e458afd0e2d2"), new Guid("937ecfaa-58fe-4ac0-88b3-fa2810c67bfc"), "Uptavljanje korisnicima", new Guid("83988e22-a297-4158-b829-ef5df2344a3f"), null, "Kao admnistrator zelim dodati novog korisnika." });

            migrationBuilder.InsertData(
                table: "UserStories",
                columns: new[] { "UserStoryRootId", "BacklogId", "PartOfEpic", "PrioritetizationParameterId", "SprintId", "TextUserStory" },
                values: new object[] { new Guid("3d48c095-a7d0-4f13-96d7-4d694564ec1d"), new Guid("937ecfaa-58fe-4ac0-88b3-fa2810c67bfc"), "Planiranje i organizovanje", new Guid("1c68a0db-ed8c-446d-a0ba-2f00e9df8c4c"), null, "Kao product owner zelim pogledati predstojece sastanke." });

            migrationBuilder.InsertData(
                table: "Functionallities",
                columns: new[] { "FunctionalityId", "SprintId", "TextFunctionality", "UserStoryRootId" },
                values: new object[] { new Guid("28b2a55a-0f35-41b8-aca2-83a49479369f"), null, "Dodavanje novog korisnika", new Guid("05da16d0-6c28-4206-b770-e458afd0e2d2") });

            migrationBuilder.InsertData(
                table: "Functionallities",
                columns: new[] { "FunctionalityId", "SprintId", "TextFunctionality", "UserStoryRootId" },
                values: new object[] { new Guid("cb553e9d-7594-485e-8449-2d8aa8b2fd68"), null, "Pregled sastanaka", new Guid("3d48c095-a7d0-4f13-96d7-4d694564ec1d") });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "TaskId", "FunctionalityId", "SprintId", "TextTask" },
                values: new object[] { new Guid("51ca50c4-0f5a-48fa-a3d6-84b56c392bd9"), new Guid("28b2a55a-0f35-41b8-aca2-83a49479369f"), null, "Validacija podataka novog korisnika" });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "TaskId", "FunctionalityId", "SprintId", "TextTask" },
                values: new object[] { new Guid("a869d41e-9647-49a7-9029-5a25b5ce0633"), new Guid("cb553e9d-7594-485e-8449-2d8aa8b2fd68"), null, "Implementacija dugmeta za dodavanje sprinta" });

            migrationBuilder.CreateIndex(
                name: "IX_Functionallities_UserStoryRootId",
                table: "Functionallities",
                column: "UserStoryRootId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_FunctionalityId",
                table: "Tasks",
                column: "FunctionalityId");

            migrationBuilder.CreateIndex(
                name: "IX_UserStories_PrioritetizationParameterId",
                table: "UserStories",
                column: "PrioritetizationParameterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Functionallities");

            migrationBuilder.DropTable(
                name: "UserStories");

            migrationBuilder.DropTable(
                name: "PrioritetizationParameters");
        }
    }
}
