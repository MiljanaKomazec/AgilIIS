using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication5.Migrations
{
    public partial class roleUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleIDRole",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_RoleIDRole",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RoleIDRole",
                table: "Users");

            migrationBuilder.AddColumn<Guid>(
                name: "IDRole",
                table: "Users",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TeamId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IDRole",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Users");

            migrationBuilder.AddColumn<Guid>(
                name: "RoleIDRole",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleIDRole",
                table: "Users",
                column: "RoleIDRole");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleIDRole",
                table: "Users",
                column: "RoleIDRole",
                principalTable: "Roles",
                principalColumn: "IDRole");
        }
    }
}
