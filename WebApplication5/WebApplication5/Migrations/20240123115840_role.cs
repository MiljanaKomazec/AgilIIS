using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication5.Migrations
{
    public partial class role : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    IDRole = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameRole = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserIDUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.IDRole);
                    table.ForeignKey(
                        name: "FK_Roles_Users_UserIDUser",
                        column: x => x.UserIDUser,
                        principalTable: "Users",
                        principalColumn: "IDUser");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Roles_UserIDUser",
                table: "Roles",
                column: "UserIDUser");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
