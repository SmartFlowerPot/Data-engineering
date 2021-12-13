﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebAPI.DataAccess;

namespace WebAPI.Migrations
{
    [DbContext(typeof(Database))]
    [Migration("20211213101021_update-planttype-datatype-size")]
    partial class updateplanttypedatatypesize
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebAPI.Models.Account", b =>
                {
                    b.Property<string>("Username")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Gender")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Region")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Username");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("WebAPI.Models.Measurement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("CO2")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Humidity")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Light")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("PlantEUI")
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("Temperature")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("PlantEUI");

                    b.ToTable("Measurements");
                });

            modelBuilder.Entity("WebAPI.Models.Plant", b =>
                {
                    b.Property<string>("EUI")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("AccountUsername")
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("DOB")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nickname")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PlantType")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("EUI");

                    b.HasIndex("AccountUsername");

                    b.ToTable("Plants");
                });

            modelBuilder.Entity("WebAPI.Models.Measurement", b =>
                {
                    b.HasOne("WebAPI.Models.Plant", null)
                        .WithMany("Measurements")
                        .HasForeignKey("PlantEUI");
                });

            modelBuilder.Entity("WebAPI.Models.Plant", b =>
                {
                    b.HasOne("WebAPI.Models.Account", null)
                        .WithMany("Plants")
                        .HasForeignKey("AccountUsername");
                });

            modelBuilder.Entity("WebAPI.Models.Account", b =>
                {
                    b.Navigation("Plants");
                });

            modelBuilder.Entity("WebAPI.Models.Plant", b =>
                {
                    b.Navigation("Measurements");
                });
#pragma warning restore 612, 618
        }
    }
}
