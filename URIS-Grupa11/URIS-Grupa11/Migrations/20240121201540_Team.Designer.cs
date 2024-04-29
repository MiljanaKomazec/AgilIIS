﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using URIS_Grupa11.Entities;

#nullable disable

namespace URIS_Grupa11.Migrations
{
    [DbContext(typeof(TeamContext))]
    [Migration("20240121201540_Team")]
    partial class Team
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("URIS_Grupa11.Models.Team", b =>
                {
                    b.Property<Guid>("TeamId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CalendarId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("TeamDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TeamName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("TeamId");

                    b.ToTable("Teams");

                    b.HasData(
                        new
                        {
                            TeamId = new Guid("550e8400-e29b-41d4-a716-446655440000"),
                            CalendarId = new Guid("250e8400-e29b-41d4-a716-446655440000"),
                            TeamDescription = "Razvoj Agilisa",
                            TeamName = "Grupa11",
                            UserId = new Guid("150e8400-e29b-41d4-a716-446655440000")
                        },
                        new
                        {
                            TeamId = new Guid("550e8400-e29b-41d4-a716-446655440001"),
                            CalendarId = new Guid("450e8400-e29b-41d4-a716-446655440000"),
                            TeamDescription = "Modifikovanje web aplikacije",
                            TeamName = "Grupa1",
                            UserId = new Guid("350e8400-e29b-41d4-a716-446655440000")
                        });
                });
#pragma warning restore 612, 618
        }
    }
}