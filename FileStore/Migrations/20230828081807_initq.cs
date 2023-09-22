using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FileStore.Migrations
{
    /// <inheritdoc />
    public partial class initq : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "quotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    stock_id = table.Column<int>(type: "int", nullable: false),
                    price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    change = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    percent_change = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    volume = table.Column<int>(type: "int", nullable: false),
                    time_stamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_quotes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "view_quotes_realtime",
                columns: table => new
                {
                    quote_id = table.Column<int>(type: "int", nullable: false),
                    symbol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    company_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    index_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    index_symbol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    market_cap = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    sector_en = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    industry_en = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sector = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    industry = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    stock_type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    change = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    percent_change = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    volume = table.Column<int>(type: "int", nullable: false),
                    time_stamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "quotes");

            migrationBuilder.DropTable(
                name: "view_quotes_realtime");
        }
    }
}
