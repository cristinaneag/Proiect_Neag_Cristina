﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Proiect_Neag_Cristina.Data;

namespace Proiect_Neag_Cristina.Migrations
{
    [DbContext(typeof(PerfumeStoreContext))]
    [Migration("20211212095618_ExtendedModel1")]
    partial class ExtendedModel1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Proiect_Neag_Cristina.Models.Customer", b =>
                {
                    b.Property<int>("CustomerID")
                        .HasColumnType("int");

                    b.Property<string>("Adress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CustomerID");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("Proiect_Neag_Cristina.Models.ManufacturedPerfumes", b =>
                {
                    b.Property<int>("PerfumeID")
                        .HasColumnType("int");

                    b.Property<int>("ManufacturerID")
                        .HasColumnType("int");

                    b.HasKey("PerfumeID", "ManufacturerID");

                    b.HasIndex("ManufacturerID");

                    b.ToTable("ManufacturedPerfumes");
                });

            modelBuilder.Entity("Proiect_Neag_Cristina.Models.Manufacturer", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Cui")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ID");

                    b.ToTable("Manufacturer");
                });

            modelBuilder.Entity("Proiect_Neag_Cristina.Models.Order", b =>
                {
                    b.Property<int>("OrderID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CustomerID")
                        .HasColumnType("int");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("PerfumeID")
                        .HasColumnType("int");

                    b.HasKey("OrderID");

                    b.HasIndex("CustomerID");

                    b.HasIndex("PerfumeID");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("Proiect_Neag_Cristina.Models.Perfume", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Brand")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Weight")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ID");

                    b.ToTable("Perfume");
                });

            modelBuilder.Entity("Proiect_Neag_Cristina.Models.ManufacturedPerfumes", b =>
                {
                    b.HasOne("Proiect_Neag_Cristina.Models.Manufacturer", "Manufacturer")
                        .WithMany("ManufacturedPerfumes")
                        .HasForeignKey("ManufacturerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Proiect_Neag_Cristina.Models.Perfume", "Perfume")
                        .WithMany()
                        .HasForeignKey("PerfumeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Manufacturer");

                    b.Navigation("Perfume");
                });

            modelBuilder.Entity("Proiect_Neag_Cristina.Models.Order", b =>
                {
                    b.HasOne("Proiect_Neag_Cristina.Models.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Proiect_Neag_Cristina.Models.Perfume", "Perfume")
                        .WithMany("Orders")
                        .HasForeignKey("PerfumeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Perfume");
                });

            modelBuilder.Entity("Proiect_Neag_Cristina.Models.Customer", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Proiect_Neag_Cristina.Models.Manufacturer", b =>
                {
                    b.Navigation("ManufacturedPerfumes");
                });

            modelBuilder.Entity("Proiect_Neag_Cristina.Models.Perfume", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
