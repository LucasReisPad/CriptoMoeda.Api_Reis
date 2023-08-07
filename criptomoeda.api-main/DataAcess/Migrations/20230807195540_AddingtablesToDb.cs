using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAcess.Migrations
{
    /// <inheritdoc />
    public partial class AddingtablesToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CoinDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Coin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaiorPreco = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MenorPreco = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QuantidadeNegociada = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PrecoUnitario = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaiorPrecoOfertado = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MenorPrecoOfertado = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DataHora = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoinDatas", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoinDatas");
        }
    }
}
