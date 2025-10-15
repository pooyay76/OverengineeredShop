using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sales_API.Migrations
{
    /// <inheritdoc />
    public partial class AddedPrices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductItemPrices",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductItemId = table.Column<long>(type: "bigint", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductItemPrices", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductItemPrices");
        }
    }
}
