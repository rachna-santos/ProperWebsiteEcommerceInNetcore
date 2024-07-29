using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Theme_Implemenet.Migrations
{
    public partial class faiza : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_inventories_Orders_OrderId",
                table: "inventories");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_customers_CustId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_productVeriations_veriationId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_statuses_StatusId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_payments_Orders_OrderId",
                table: "payments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "Order");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_veriationId",
                table: "Order",
                newName: "IX_Order_veriationId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_StatusId",
                table: "Order",
                newName: "IX_Order_StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_CustId",
                table: "Order",
                newName: "IX_Order_CustId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order",
                table: "Order",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_inventories_Order_OrderId",
                table: "inventories",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_customers_CustId",
                table: "Order",
                column: "CustId",
                principalTable: "customers",
                principalColumn: "CustId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_productVeriations_veriationId",
                table: "Order",
                column: "veriationId",
                principalTable: "productVeriations",
                principalColumn: "veriationId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_statuses_StatusId",
                table: "Order",
                column: "StatusId",
                principalTable: "statuses",
                principalColumn: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_payments_Order_OrderId",
                table: "payments",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_inventories_Order_OrderId",
                table: "inventories");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_customers_CustId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_productVeriations_veriationId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_statuses_StatusId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_payments_Order_OrderId",
                table: "payments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order",
                table: "Order");

            migrationBuilder.RenameTable(
                name: "Order",
                newName: "Orders");

            migrationBuilder.RenameIndex(
                name: "IX_Order_veriationId",
                table: "Orders",
                newName: "IX_Orders_veriationId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_StatusId",
                table: "Orders",
                newName: "IX_Orders_StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_CustId",
                table: "Orders",
                newName: "IX_Orders_CustId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_inventories_Orders_OrderId",
                table: "inventories",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_customers_CustId",
                table: "Orders",
                column: "CustId",
                principalTable: "customers",
                principalColumn: "CustId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_productVeriations_veriationId",
                table: "Orders",
                column: "veriationId",
                principalTable: "productVeriations",
                principalColumn: "veriationId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_statuses_StatusId",
                table: "Orders",
                column: "StatusId",
                principalTable: "statuses",
                principalColumn: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_payments_Orders_OrderId",
                table: "payments",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
