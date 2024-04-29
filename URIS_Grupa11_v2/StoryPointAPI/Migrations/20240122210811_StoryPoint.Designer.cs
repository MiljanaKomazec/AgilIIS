﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StoryPointAPI.Models;

#nullable disable

namespace StoryPointAPI.Migrations
{
    [DbContext(typeof(StoryPointContext))]
    [Migration("20240122210811_StoryPoint")]
    partial class StoryPoint
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("StoryPointAPI.Models.StoryPoint", b =>
                {
                    b.Property<Guid>("StoryPointId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ValueStoryPoint")
                        .HasColumnType("int");

                    b.HasKey("StoryPointId");

                    b.ToTable("StoryPoints");
                });
#pragma warning restore 612, 618
        }
    }
}
