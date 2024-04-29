﻿// <auto-generated />
using System;
using CommentService.Entiti;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CommentService.Migrations
{
    [DbContext(typeof(CommentContext))]
    partial class CommentContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CommentService.Model.Comment", b =>
                {
                    b.Property<Guid>("CommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateComment")
                        .HasColumnType("datetime2");

                    b.Property<string>("TextComment")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UserStoryRootId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CommentId");

                    b.ToTable("Comment");

                    b.HasData(
                        new
                        {
                            CommentId = new Guid("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                            DateComment = new DateTime(2021, 6, 27, 8, 0, 0, 0, DateTimeKind.Unspecified),
                            TextComment = "Potrebno je proširiti ovu korisničku priču.",
                            UserId = new Guid("cbea5366-bf13-40ab-a518-c9b6f81bbfdf"),
                            UserStoryRootId = new Guid("6cf6c4c5-40bc-4c67-b2a6-5d61959d6b84")
                        },
                        new
                        {
                            CommentId = new Guid("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                            DateComment = new DateTime(2023, 11, 15, 14, 0, 0, 0, DateTimeKind.Unspecified),
                            TextComment = "Dobro napisano.",
                            UserId = new Guid("cbea5366-bf13-40ab-a518-c9b6f81bbfdf"),
                            UserStoryRootId = new Guid("05da16d0-6c28-4206-b770-e458afd0e2d2")
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
