﻿// <auto-generated />
using System;
using BeachTenisAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BeachTenisAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210911185238_addedSpreadSheet")]
    partial class addedSpreadSheet
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BeachTenisAPI.Models.Spreadsheet", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Coach")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Distance")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Pace")
                        .HasColumnType("int");

                    b.Property<string>("User")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Spreadsheets");
                });

            modelBuilder.Entity("BeachTenisAPI.Models.User", b =>
                {
                    b.Property<string>("CPF")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("ID")
                        .HasColumnType("int");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CPF", "ID");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
