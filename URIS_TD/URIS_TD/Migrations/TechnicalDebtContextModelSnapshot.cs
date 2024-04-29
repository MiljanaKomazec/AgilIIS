﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using URIS_TD.Entities;

#nullable disable

namespace URIS_TD.Migrations
{
    [DbContext(typeof(TechnicalDebtContext))]
    partial class TechnicalDebtContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("URIS_TD.Models.TechnicalDebt", b =>
                {
                    b.Property<Guid>("IdTd")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DescriptionTd")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameTd")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("SprintId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TypeID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("IdTd");

                    b.ToTable("Debts");
                });

            modelBuilder.Entity("URIS_TD.Models.TypeOfTechnicalDebt", b =>
                {
                    b.Property<Guid>("IdTod")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("NameTotd")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdTod");

                    b.ToTable("Type");
                });
#pragma warning restore 612, 618
        }
    }
}
