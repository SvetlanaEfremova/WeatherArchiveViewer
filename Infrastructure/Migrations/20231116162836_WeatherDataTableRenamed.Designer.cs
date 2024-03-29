﻿// <auto-generated />
using System;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231116162836_WeatherDataTableRenamed")]
    partial class WeatherDataTableRenamed
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Infrastructure.Models.Weather", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AtmosphericPressure")
                        .HasColumnType("int");

                    b.Property<int?>("Cloudiness")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateAndTime")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("DewPoint")
                        .HasPrecision(3, 1)
                        .HasColumnType("decimal(3,1)");

                    b.Property<int?>("HorizontalVisibility")
                        .HasColumnType("int");

                    b.Property<decimal>("Humidity")
                        .HasPrecision(4, 2)
                        .HasColumnType("decimal(4,2)");

                    b.Property<int>("LowerCloudLimit")
                        .HasColumnType("int");

                    b.Property<decimal>("Temperature")
                        .HasPrecision(3, 1)
                        .HasColumnType("decimal(3,1)");

                    b.Property<string>("WeatherEvents")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WindDirection")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("WindVelocity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("WeatherData");
                });
#pragma warning restore 612, 618
        }
    }
}
