using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication5.Migrations
{
    public partial class user : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    IDUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameUser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SurnameUser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailUser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordUser = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.IDUser);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
