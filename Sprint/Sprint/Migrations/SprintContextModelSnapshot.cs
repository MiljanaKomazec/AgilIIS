﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Sprint.Entities;

#nullable disable

namespace Sprint.Migrations
{
    [DbContext(typeof(SprintContext))]
    partial class SprintContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Sprint.Models.ModelBacklog.Backlog", b =>
                {
                    b.Property<Guid>("BacklogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("NameBacklog")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BacklogId");

                    b.ToTable("Backlog");

                    b.HasData(
                        new
                        {
                            BacklogId = new Guid("937ecfaa-58fe-4ac0-88b3-fa2810c67bfc"),
                            NameBacklog = "Project 1"
                        },
                        new
                        {
                            BacklogId = new Guid("db7e7a04-8082-4ebb-88b0-d05f9dae4243"),
                            NameBacklog = "Project 2"
                        });
                });

            modelBuilder.Entity("Sprint.Models.ModelBacklogItem.BacklogItem", b =>
                {
                    b.Property<Guid>("BacklogItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BacklogId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("POBIId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SprintId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("TimeAddedBacklogItem")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BacklogItemId");

                    b.HasIndex("BacklogId");

                    b.HasIndex("POBIId");

                    b.HasIndex("SprintId");

                    b.ToTable("BacklogItem");

                    b.HasData(
                        new
                        {
                            BacklogItemId = new Guid("45d01a65-a992-45cc-b670-1ffdd179a8f2"),
                            BacklogId = new Guid("937ecfaa-58fe-4ac0-88b3-fa2810c67bfc"),
                            POBIId = new Guid("290591c5-7054-4b03-9e40-42c5e1eadde2"),
                            SprintId = new Guid("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                            TimeAddedBacklogItem = "11:00 PM"
                        },
                        new
                        {
                            BacklogItemId = new Guid("6edbc9cb-32bb-48a3-90b6-fa070eede946"),
                            BacklogId = new Guid("937ecfaa-58fe-4ac0-88b3-fa2810c67bfc"),
                            POBIId = new Guid("290591c5-7054-4b03-9e40-42c5e1eadde2"),
                            SprintId = new Guid("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                            TimeAddedBacklogItem = "11:00 PM"
                        });
                });

            modelBuilder.Entity("Sprint.Models.ModelPOBI.PhaseOfBacklogItem", b =>
                {
                    b.Property<Guid>("POBIId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("NameOfPOBI")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("POBIId");

                    b.ToTable("PhaseOfBacklogItem");

                    b.HasData(
                        new
                        {
                            POBIId = new Guid("290591c5-7054-4b03-9e40-42c5e1eadde2"),
                            NameOfPOBI = "Done"
                        },
                        new
                        {
                            POBIId = new Guid("ed6e1f21-748a-4801-9a94-85e52b8fb256"),
                            NameOfPOBI = "Waiting"
                        });
                });

            modelBuilder.Entity("Sprint.Models.ModelSprint.SprintS", b =>
                {
                    b.Property<Guid>("SprintId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DurationSprint")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndOfSprint")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartOfSprint")
                        .HasColumnType("datetime2");

                    b.HasKey("SprintId");

                    b.ToTable("Sprint");

                    b.HasData(
                        new
                        {
                            SprintId = new Guid("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                            DurationSprint = "2 weeks",
                            EndOfSprint = new DateTime(2020, 12, 30, 9, 0, 0, 0, DateTimeKind.Unspecified),
                            StartOfSprint = new DateTime(2020, 12, 15, 9, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            SprintId = new Guid("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                            DurationSprint = "2 weeks",
                            EndOfSprint = new DateTime(2020, 12, 30, 9, 0, 0, 0, DateTimeKind.Unspecified),
                            StartOfSprint = new DateTime(2020, 12, 15, 9, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("Sprint.Models.ModelBacklogItem.BacklogItem", b =>
                {
                    b.HasOne("Sprint.Models.ModelBacklog.Backlog", "Backlog")
                        .WithMany()
                        .HasForeignKey("BacklogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sprint.Models.ModelPOBI.PhaseOfBacklogItem", "POBI")
                        .WithMany()
                        .HasForeignKey("POBIId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sprint.Models.ModelSprint.SprintS", "Sprint")
                        .WithMany()
                        .HasForeignKey("SprintId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Backlog");

                    b.Navigation("POBI");

                    b.Navigation("Sprint");
                });
#pragma warning restore 612, 618
        }
    }
}