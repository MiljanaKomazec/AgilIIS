using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication5.Migrations
{
    public partial class userRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Users_UserIDUser",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Roles_UserIDUser",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "UserIDUser",
                table: "Roles");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "UserIDUser",
                table: "Roles",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Roles_UserIDUser",
                table: "Roles",
                column: "UserIDUser");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Users_UserIDUser",
                table: "Roles",
                column: "UserIDUser",
                principalTable: "Users",
                principalColumn: "IDUser");
        }
    }
}
