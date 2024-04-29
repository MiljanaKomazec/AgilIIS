using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Grupa11_Calendar.Migrations
{
    public partial class Calendar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Calendars",
                columns: table => new
                {
                    CalendarId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CalendarName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberOfDaysCalendar = table.Column<int>(type: "int", nullable: false),
                    YearCalendar = table.Column<int>(type: "int", nullable: false),
                    MonthCalendar = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calendars", x => x.CalendarId);
                });

            migrationBuilder.CreateTable(
                name: "EventTypes",
                columns: table => new
                {
                    EventTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventTypes", x => x.EventTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EventTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EventDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CalendarId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.EventId);
                    table.ForeignKey(
                        name: "FK_Events_Calendars_CalendarId",
                        column: x => x.CalendarId,
                        principalTable: "Calendars",
                        principalColumn: "CalendarId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Events_EventTypes_EventTypeId",
                        column: x => x.EventTypeId,
                        principalTable: "EventTypes",
                        principalColumn: "EventTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Calendars",
                columns: new[] { "CalendarId", "CalendarName", "MonthCalendar", "NumberOfDaysCalendar", "YearCalendar" },
                values: new object[,]
                {
                    { new Guid("550e8400-e29b-41d4-a716-446655440006"), "Timski kalendar 2", 1, 12, 2013 },
                    { new Guid("550e8400-e29b-41d4-a716-446655440007"), "Kalendar TimLidera", 5, 2, 2022 }
                });

            migrationBuilder.InsertData(
                table: "EventTypes",
                columns: new[] { "EventTypeId", "EventTypeName" },
                values: new object[,]
                {
                    { new Guid("550e8400-e29b-41d4-a716-446655440002"), "Sastanak" },
                    { new Guid("550e8400-e29b-41d4-a716-446655440003"), "Prezentacija" }
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventId", "CalendarId", "EventDate", "EventDescription", "EventName", "EventTime", "EventTypeId" },
                values: new object[] { new Guid("550e8400-e29b-41d4-a716-446655440004"), new Guid("550e8400-e29b-41d4-a716-446655440006"), new DateTime(2023, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description", "Sastanak sa ScrumMasterom Projekta 3", new DateTime(2024, 9, 26, 14, 30, 0, 0, DateTimeKind.Unspecified), new Guid("550e8400-e29b-41d4-a716-446655440003") });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventId", "CalendarId", "EventDate", "EventDescription", "EventName", "EventTime", "EventTypeId" },
                values: new object[] { new Guid("550e8400-e29b-41d4-a716-446655440005"), new Guid("550e8400-e29b-41d4-a716-446655440006"), new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Description", "Upoznavanje sa novim partnerom", new DateTime(2024, 1, 10, 15, 0, 0, 0, DateTimeKind.Unspecified), new Guid("550e8400-e29b-41d4-a716-446655440003") });

            migrationBuilder.CreateIndex(
                name: "IX_Events_CalendarId",
                table: "Events",
                column: "CalendarId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_EventTypeId",
                table: "Events",
                column: "EventTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Calendars");

            migrationBuilder.DropTable(
                name: "EventTypes");
        }
    }
}
