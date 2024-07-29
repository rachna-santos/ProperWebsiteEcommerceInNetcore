using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Theme_Implemenet.Migrations
{
    public partial class humaima : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "price",
                table: "product",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "customers",
                columns: table => new
                {
                    CustId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cityId = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customers", x => x.CustId);
                    table.ForeignKey(
                        name: "FK_customers_cities_cityId",
                        column: x => x.cityId,
                        principalTable: "cities",
                        principalColumn: "cityId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_customers_companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "companies",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_customers_statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "statuses",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingCarts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    veriationId = table.Column<int>(type: "int", nullable: false),
                    ProductVeriationveriationId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    bill = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCarts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingCarts_productVeriations_ProductVeriationveriationId",
                        column: x => x.ProductVeriationveriationId,
                        principalTable: "productVeriations",
                        principalColumn: "veriationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "order",
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

            migrationBuilder.CreateTable(
                name: "inventories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    custId = table.Column<int>(type: "int", nullable: false),
                    CustomerCustId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    Order_TblOrderId = table.Column<int>(type: "int", nullable: false),
                    productbrandId = table.Column<int>(type: "int", nullable: false),
                    productId = table.Column<int>(type: "int", nullable: false),
                    productcolorId = table.Column<int>(type: "int", nullable: false),
                    productsizeId = table.Column<int>(type: "int", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lastmodifield = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inventories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_inventories_customers_CustomerCustId",
                        column: x => x.CustomerCustId,
                        principalTable: "customers",
                        principalColumn: "CustId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_inventories_order_Order_TblOrderId",
                        column: x => x.Order_TblOrderId,
                        principalTable: "order",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_inventories_product_productId",
                        column: x => x.productId,
                        principalTable: "product",
                        principalColumn: "productId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_inventories_productBrands_productbrandId",
                        column: x => x.productbrandId,
                        principalTable: "productBrands",
                        principalColumn: "productbrandId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_inventories_productColors_productcolorId",
                        column: x => x.productcolorId,
                        principalTable: "productColors",
                        principalColumn: "productcolorId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_inventories_productSizes_productsizeId",
                        column: x => x.productsizeId,
                        principalTable: "productSizes",
                        principalColumn: "productsizeId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_inventories_statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "statuses",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    Order_TblOrderId = table.Column<int>(type: "int", nullable: false),
                    custId = table.Column<int>(type: "int", nullable: false),
                    CustomerCustId = table.Column<int>(type: "int", nullable: false),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    totalamount = table.Column<float>(type: "real", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lastmodifield = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_payments_customers_CustomerCustId",
                        column: x => x.CustomerCustId,
                        principalTable: "customers",
                        principalColumn: "CustId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_payments_order_Order_TblOrderId",
                        column: x => x.Order_TblOrderId,
                        principalTable: "order",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_customers_cityId",
                table: "customers",
                column: "cityId");

            migrationBuilder.CreateIndex(
                name: "IX_customers_CompanyId",
                table: "customers",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_customers_StatusId",
                table: "customers",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_inventories_CustomerCustId",
                table: "inventories",
                column: "CustomerCustId");

            migrationBuilder.CreateIndex(
                name: "IX_inventories_Order_TblOrderId",
                table: "inventories",
                column: "Order_TblOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_inventories_productbrandId",
                table: "inventories",
                column: "productbrandId");

            migrationBuilder.CreateIndex(
                name: "IX_inventories_productcolorId",
                table: "inventories",
                column: "productcolorId");

            migrationBuilder.CreateIndex(
                name: "IX_inventories_productId",
                table: "inventories",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_inventories_productsizeId",
                table: "inventories",
                column: "productsizeId");

            migrationBuilder.CreateIndex(
                name: "IX_inventories_StatusId",
                table: "inventories",
                column: "StatusId");

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

            migrationBuilder.CreateIndex(
                name: "IX_payments_CustomerCustId",
                table: "payments",
                column: "CustomerCustId");

            migrationBuilder.CreateIndex(
                name: "IX_payments_Order_TblOrderId",
                table: "payments",
                column: "Order_TblOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_ProductVeriationveriationId",
                table: "ShoppingCarts",
                column: "ProductVeriationveriationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "inventories");

            migrationBuilder.DropTable(
                name: "payments");

            migrationBuilder.DropTable(
                name: "ShoppingCarts");

            migrationBuilder.DropTable(
                name: "order");

            migrationBuilder.DropTable(
                name: "customers");

            migrationBuilder.DropColumn(
                name: "price",
                table: "product");
        }
    }
}
