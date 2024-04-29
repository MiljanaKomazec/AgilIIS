using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace URIS_TD.Migrations
{
    public partial class technicalDebt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Debts",
                columns: table => new
                {
                    IdTd = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameTd = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescriptionTd = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Debts", x => x.IdTd);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Debts");
        }
    }
}
