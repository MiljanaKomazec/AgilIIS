using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace URIS_TD.Migrations
{
    public partial class techSprint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SprintId",
                table: "Debts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SprintId",
                table: "Debts");
        }
    }
}
