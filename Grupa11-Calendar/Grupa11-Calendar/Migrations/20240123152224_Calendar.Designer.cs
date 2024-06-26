﻿// <auto-generated />
using System;
using Grupa11_Calendar.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Grupa11_Calendar.Migrations
{
    [DbContext(typeof(CalendarContext))]
    [Migration("20240123152224_Calendar")]
    partial class Calendar
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Grupa11_Calendar.Models.Calendar", b =>
                {
                    b.Property<Guid>("CalendarId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CalendarName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MonthCalendar")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfDaysCalendar")
                        .HasColumnType("int");

                    b.Property<int>("YearCalendar")
                        .HasColumnType("int");

                    b.HasKey("CalendarId");

                    b.ToTable("Calendars");

                    b.HasData(
                        new
                        {
                            CalendarId = new Guid("550e8400-e29b-41d4-a716-446655440006"),
                            CalendarName = "Timski kalendar 2",
                            MonthCalendar = 1,
                            NumberOfDaysCalendar = 12,
                            YearCalendar = 2013
                        },
                        new
                        {
                            CalendarId = new Guid("550e8400-e29b-41d4-a716-446655440007"),
                            CalendarName = "Kalendar TimLidera",
                            MonthCalendar = 5,
                            NumberOfDaysCalendar = 2,
                            YearCalendar = 2022
                        });
                });

            modelBuilder.Entity("Grupa11_Calendar.Models.Event", b =>
                {
                    b.Property<Guid>("EventId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CalendarId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("EventDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("EventDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EventName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EventTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("EventTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("EventId");

                    b.HasIndex("CalendarId");

                    b.HasIndex("EventTypeId");

                    b.ToTable("Events");

                    b.HasData(
                        new
                        {
                            EventId = new Guid("550e8400-e29b-41d4-a716-446655440004"),
                            CalendarId = new Guid("550e8400-e29b-41d4-a716-446655440006"),
                            EventDate = new DateTime(2023, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EventDescription = "Description",
                            EventName = "Sastanak sa ScrumMasterom Projekta 3",
                            EventTime = new DateTime(2024, 9, 26, 14, 30, 0, 0, DateTimeKind.Unspecified),
                            EventTypeId = new Guid("550e8400-e29b-41d4-a716-446655440003")
                        },
                        new
                        {
                            EventId = new Guid("550e8400-e29b-41d4-a716-446655440005"),
                            CalendarId = new Guid("550e8400-e29b-41d4-a716-446655440006"),
                            EventDate = new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EventDescription = "Description",
                            EventName = "Upoznavanje sa novim partnerom",
                            EventTime = new DateTime(2024, 1, 10, 15, 0, 0, 0, DateTimeKind.Unspecified),
                            EventTypeId = new Guid("550e8400-e29b-41d4-a716-446655440003")
                        });
                });

            modelBuilder.Entity("Grupa11_Calendar.Models.EventType", b =>
                {
                    b.Property<Guid>("EventTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("EventTypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EventTypeId");

                    b.ToTable("EventTypes");

                    b.HasData(
                        new
                        {
                            EventTypeId = new Guid("550e8400-e29b-41d4-a716-446655440002"),
                            EventTypeName = "Sastanak"
                        },
                        new
                        {
                            EventTypeId = new Guid("550e8400-e29b-41d4-a716-446655440003"),
                            EventTypeName = "Prezentacija"
                        });
                });

            modelBuilder.Entity("Grupa11_Calendar.Models.Event", b =>
                {
                    b.HasOne("Grupa11_Calendar.Models.Calendar", "Calendar")
                        .WithMany()
                        .HasForeignKey("CalendarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Grupa11_Calendar.Models.EventType", "EventType")
                        .WithMany()
                        .HasForeignKey("EventTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Calendar");

                    b.Navigation("EventType");
                });
#pragma warning restore 612, 618
        }
    }
}
