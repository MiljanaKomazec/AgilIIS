using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace URIS_TD.Migrations
{
    public partial class type : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Type",
                columns: table => new
                {
                    IdTod = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameTotd = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Type", x => x.IdTod);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Type");
        }
    }
}
