using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Sales.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Discounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DiscountPercentage = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    ProductItemId = table.Column<long>(type: "bigint", nullable: true),
                    TargetType = table.Column<int>(type: "integer", nullable: false),
                    DiscountType = table.Column<string>(type: "text", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentTransactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SessionId = table.Column<long>(type: "bigint", nullable: false),
                    TransactionStartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TransactionEndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PaymentGatewayUrl = table.Column<string>(type: "text", nullable: true),
                    BaseAmount_Amount = table.Column<decimal>(type: "numeric", nullable: true),
                    BaseAmount_Currency = table.Column<string>(type: "text", nullable: true),
                    Amount_Amount = table.Column<decimal>(type: "numeric", nullable: true),
                    Amount_Currency = table.Column<string>(type: "text", nullable: true),
                    SessionStatus = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTransactions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductItemPrices",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductItemId = table.Column<long>(type: "bigint", nullable: true),
                    CreationDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Price_Amount = table.Column<decimal>(type: "numeric", nullable: true),
                    Price_Currency = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductItemPrices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingCarts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCarts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bills",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    PaymentSessionId = table.Column<string>(type: "text", nullable: false),
                    ShippingInformation_ReceiverFirstName = table.Column<string>(type: "text", nullable: true),
                    ShippingInformation_ReceiverLastName = table.Column<string>(type: "text", nullable: true),
                    ShippingInformation_ShippingType = table.Column<int>(type: "integer", nullable: false),
                    ShippingInformation_Country = table.Column<string>(type: "text", nullable: true),
                    ShippingInformation_Province = table.Column<string>(type: "text", nullable: true),
                    ShippingInformation_City = table.Column<string>(type: "text", nullable: true),
                    ShippingInformation_Address = table.Column<string>(type: "text", nullable: true),
                    ShippingInformation_PostalCode = table.Column<string>(type: "text", nullable: true),
                    ShippingInformation_ContactPhoneNumber = table.Column<string>(type: "text", nullable: true),
                    BillStatus = table.Column<int>(type: "integer", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ExpirationDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PaidAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TotalItemPricesBase_Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalItemPricesBase_Currency = table.Column<string>(type: "text", nullable: true),
                    TotalItemsDiscountedAmount_Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalItemsDiscountedAmount_Currency = table.Column<string>(type: "text", nullable: true),
                    TotalItemsPriceWithDiscount_Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalItemsPriceWithDiscount_Currency = table.Column<string>(type: "text", nullable: true),
                    TotalBillItemsDiscount_Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalBillItemsDiscount_Currency = table.Column<string>(type: "text", nullable: true),
                    TotalTax_Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalTax_Currency = table.Column<string>(type: "text", nullable: true),
                    ShippingCost_Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    ShippingCost_Currency = table.Column<string>(type: "text", nullable: true),
                    TotalBilling_Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalBilling_Currency = table.Column<string>(type: "text", nullable: true),
                    TotalBillingDiscountedAmount_Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalBillingDiscountedAmount_Currency = table.Column<string>(type: "text", nullable: true),
                    BillDiscountId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bills_Discounts_BillDiscountId",
                        column: x => x.BillDiscountId,
                        principalTable: "Discounts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ShoppingCartItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ShoppingCartId = table.Column<Guid>(type: "uuid", nullable: true),
                    ProductItemId = table.Column<long>(type: "bigint", nullable: true),
                    Quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingCartItems_ShoppingCarts_ShoppingCartId",
                        column: x => x.ShoppingCartId,
                        principalTable: "ShoppingCarts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BillItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BillId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductItemId = table.Column<long>(type: "bigint", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    UnitPriceBase_Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    UnitPriceBase_Currency = table.Column<string>(type: "text", nullable: true),
                    DiscountedAmount_Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    DiscountedAmount_Currency = table.Column<string>(type: "text", nullable: true),
                    UnitPriceWithDiscount_Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    UnitPriceWithDiscount_Currency = table.Column<string>(type: "text", nullable: true),
                    DiscountId = table.Column<Guid>(type: "uuid", nullable: true),
                    DiscountPercentage = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillItems_Bills_BillId",
                        column: x => x.BillId,
                        principalTable: "Bills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BillItems_Discounts_DiscountId",
                        column: x => x.DiscountId,
                        principalTable: "Discounts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: true),
                    OrderTotal_Amount = table.Column<decimal>(type: "numeric", nullable: true),
                    OrderTotal_Currency = table.Column<string>(type: "text", nullable: true),
                    IsInStock = table.Column<bool>(type: "boolean", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    OrderStatus = table.Column<string>(type: "text", nullable: false),
                    BillId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Bills_BillId",
                        column: x => x.BillId,
                        principalTable: "Bills",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductItemId = table.Column<long>(type: "bigint", nullable: true),
                    Quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => new { x.OrderId, x.Id });
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BillItems_BillId",
                table: "BillItems",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_BillItems_DiscountId",
                table: "BillItems",
                column: "DiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_BillDiscountId",
                table: "Bills",
                column: "BillDiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_BillId",
                table: "Orders",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartItems_ShoppingCartId",
                table: "ShoppingCartItems",
                column: "ShoppingCartId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillItems");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "PaymentTransactions");

            migrationBuilder.DropTable(
                name: "ProductItemPrices");

            migrationBuilder.DropTable(
                name: "ShoppingCartItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "ShoppingCarts");

            migrationBuilder.DropTable(
                name: "Bills");

            migrationBuilder.DropTable(
                name: "Discounts");
        }
    }
}
