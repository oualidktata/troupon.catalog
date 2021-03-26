﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Troupon.Catalog.Infra.Persistence;

namespace relese_notes_server_dot_net.Migrations
{
    [DbContext(typeof(CatalogDbContext))]
    partial class CatalogDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.7");

            modelBuilder.Entity("relese_notes_server_dot_net.ApplicationEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Applications");

                    b.HasData(
                        new
                        {
                            Id = new Guid("7d61861a-bf95-459f-b870-02d9a223e377"),
                            Name = "AGATE"
                        },
                        new
                        {
                            Id = new Guid("bf8d617a-436f-4797-be07-366a010ff853"),
                            Name = "ADP"
                        },
                        new
                        {
                            Id = new Guid("816e440e-a26f-4a52-b2d3-58d597b11b10"),
                            Name = "Facturation"
                        });
                });

            modelBuilder.Entity("Troupon.Catalog.Infra.Persistence.Entities.DealEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Major")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Minor")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Patch")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Deals");

                    b.HasData(
                        new
                        {
                            Id = "0d53c6ce-6181-42a8-8616-03f86f883112",
                            Major = "1",
                            Minor = "0",
                            Patch = "0"
                        },
                        new
                        {
                            Id = "322a8677-ab65-412e-a9f4-3e9c1bd9126e",
                            Major = "1",
                            Minor = "1",
                            Patch = "0"
                        },
                        new
                        {
                            Id = "5772fa5d-4038-49f6-aad9-04bf198f2b97",
                            Major = "1",
                            Minor = "1",
                            Patch = "1"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
