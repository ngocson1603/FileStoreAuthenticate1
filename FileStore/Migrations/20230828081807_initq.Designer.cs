﻿// <auto-generated />
using System;
using FileStore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FileStore.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20230828081807_initq")]
    partial class initq
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FileStore.Models.Quote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Change")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("change");

                    b.Property<decimal>("PercentChange")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("percent_change");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("price");

                    b.Property<int>("StockId")
                        .HasColumnType("int")
                        .HasColumnName("stock_id");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("datetime2")
                        .HasColumnName("time_stamp");

                    b.Property<int>("Volume")
                        .HasColumnType("int")
                        .HasColumnName("volume");

                    b.HasKey("Id");

                    b.ToTable("quotes");
                });

            modelBuilder.Entity("FileStore.Models.RealtimeQuote", b =>
                {
                    b.Property<decimal>("Change")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("change");

                    b.Property<string>("CompanyName")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("company_name");

                    b.Property<string>("IndexName")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("index_name");

                    b.Property<string>("IndexSymbol")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("index_symbol");

                    b.Property<string>("Industry")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("industry");

                    b.Property<string>("IndustryEn")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("industry_en");

                    b.Property<decimal>("MarketCap")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("market_cap");

                    b.Property<decimal>("PercentChange")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("percent_change");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("price");

                    b.Property<string>("Sector")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("sector");

                    b.Property<string>("SectorEn")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("sector_en");

                    b.Property<string>("StockType")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("stock_type");

                    b.Property<string>("Symbol")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("symbol");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("datetime2")
                        .HasColumnName("time_stamp");

                    b.Property<int>("Volume")
                        .HasColumnType("int")
                        .HasColumnName("volume");

                    b.Property<int>("quoteId")
                        .HasColumnType("int")
                        .HasColumnName("quote_id");

                    b.ToTable("view_quotes_realtime");
                });

            modelBuilder.Entity("FileStore.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("User", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
