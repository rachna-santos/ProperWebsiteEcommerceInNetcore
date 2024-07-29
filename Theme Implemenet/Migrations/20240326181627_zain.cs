using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Theme_Implemenet.Migrations
{
    public partial class zain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_inventories_order_Order_TblOrderId",
                table: "inventories");

            migrationBuilder.DropForeignKey(
                name: "FK_payments_order_Order_TblOrderId",
                table: "payments");

            migrationBuilder.DropTable(
                name: "order");

            migrationBuilder.DropIndex(
                name: "IX_payments_Order_TblOrderId",
                table: "payments");

            migrationBuilder.DropIndex(
                name: "IX_inventories_Order_TblOrderId",
                table: "inventories");

            migrationBuilder.DropColumn(
                name: "Order_TblOrderId",
                table: "payments");

            migrationBuilder.DropColumn(
                name: "Order_TblOrderId",
                table: "inventories");

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderNumber = table.Column<int>(type: "int", nullable: false),
                    veriationId = table.Column<int>(type: "int", nullable: false),
                    ProductVeriationveriationId = table.Column<int>(type: "int", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    CustId = table.Column<int>(type: "int", nullable: false),
                    CustomerCustId = table.Column<int>(type: "int", nullable: false),
                    totalamount = table.Column<int>(type: "int", nullable: false),
                    grossamount = table.Column<int>(type: "int", nullable: false),
                    shippingamount = table.Column<int>(type: "int", nullable: false),
                    netamount = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lastmodifield = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_customers_CustomerCustId",
                        column: x => x.CustomerCustId,
                        principalTable: "customers",
                        principalColumn: "CustId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_productVeriations_ProductVeriationveriationId",
                        column: x => x.ProductVeriationveriationId,
                        principalTable: "productVeriations",
                        principalColumn: "veriationId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Orders_statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "statuses",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_payments_OrderId",
                table: "payments",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_inventories_OrderId",
                table: "inventories",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerCustId",
                table: "Orders",
                column: "CustomerCustId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ProductVeriationveriationId",
                table: "Orders",
                column: "ProductVeriationveriationId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_StatusId",
                table: "Orders",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_inventories_Orders_OrderId",
                table: "inventories",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_payments_Orders_OrderId",
                table: "payments",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_inventories_Orders_OrderId",
                table: "inventories");

            migrationBuilder.DropForeignKey(
                name: "FK_payments_Orders_OrderId",
                table: "payments");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_payments_OrderId",
                table: "payments");

            migrationBuilder.DropIndex(
                name: "IX_inventories_OrderId",
                table: "inventories");

            migrationBuilder.AddColumn<int>(
                name: "Order_TblOrderId",
                table: "payments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Order_TblOrderId",
                table: "inventories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "order",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerCustId = table.Column<int>(type: "int", nullable: false),
                    ProductVeriationveriationId = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustId = table.Column<int>(type: "int", nullable: false),
                    Lastmodifield = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderNumber = table.Column<int>(type: "int", nullable: false),
                    grossamount = table.Column<int>(type: "int", nullable: false),
                    netamount = table.Column<int>(type: "int", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    shippingamount = table.Column<int>(type: "int", nullable: false),
                    totalamount = table.Column<int>(type: "int", nullable: false),
                    veriationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_order_customers_CustomerCustId",
                        column: x => x.CustomerCustId,
                        principalTable: "customers",
                        principalColumn: "CustId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_order_productVeriations_ProductVeriationveriationId",
                        column: x => x.ProductVeriationveriationId,
                        principalTable: "productVeriations",
                        principalColumn: "veriationId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_order_statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "statuses",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_payments_Order_TblOrderId",
                table: "payments",
                column: "Order_TblOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_inventories_Order_TblOrderId",
                table: "inventories",
                column: "Order_TblOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_order_CustomerCustId",
                table: "order",
                column: "CustomerCustId");

            migrationBuilder.CreateIndex(
                name: "IX_order_ProductVeriationveriationId",
                table: "order",
                column: "ProductVeriationveriationId");

            migrationBuilder.CreateIndex(
                name: "IX_order_StatusId",
                table: "order",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_inventories_order_Order_TblOrderId",
                table: "inventories",
                column: "Order_TblOrderId",
                principalTable: "order",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_payments_order_Order_TblOrderId",
                table: "payments",
                column: "Order_TblOrderId",
                principalTable: "order",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
