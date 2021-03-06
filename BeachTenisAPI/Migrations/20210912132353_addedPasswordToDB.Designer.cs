// <auto-generated />
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
    [Migration("20210912132353_addedPasswordToDB")]
    partial class addedPasswordToDB
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
                    b.Property<string>("User")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Day")
                        .HasColumnType("datetime2");

                    b.Property<string>("Coach")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Distance")
                        .HasColumnType("float");

                    b.Property<double>("Pace")
                        .HasColumnType("float");

                    b.HasKey("User", "Day");

                    b.ToTable("Spreadsheets");
                });

            modelBuilder.Entity("BeachTenisAPI.Models.User", b =>
                {
                    b.Property<string>("CPF")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CPF");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
