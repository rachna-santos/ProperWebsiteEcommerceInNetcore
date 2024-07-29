using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Theme_Implemenet.Migrations
{
    public partial class muneef : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCarts_productVeriations_ProductVeriationveriationId",
                table: "ShoppingCarts");

            migrationBuilder.DropColumn(
                name: "OrderNumber",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "ProductVeriationveriationId",
                table: "ShoppingCarts",
                newName: "CustomerCustId");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingCarts_ProductVeriationveriationId",
                table: "ShoppingCarts",
                newName: "IX_ShoppingCarts_CustomerCustId");

            migrationBuilder.AddColumn<int>(
                name: "CustId",
                table: "ShoppingCarts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_veriationId",
                table: "ShoppingCarts",
                column: "veriationId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCarts_customers_CustomerCustId",
                table: "ShoppingCarts",
                column: "CustomerCustId",
                principalTable: "customers",
                principalColumn: "CustId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCarts_productVeriations_veriationId",
                table: "ShoppingCarts",
                column: "veriationId",
                principalTable: "productVeriations",
                principalColumn: "veriationId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCarts_customers_CustomerCustId",
                table: "ShoppingCarts");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCarts_productVeriations_veriationId",
                table: "ShoppingCarts");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCarts_veriationId",
                table: "ShoppingCarts");

            migrationBuilder.DropColumn(
                name: "CustId",
                table: "ShoppingCarts");

            migrationBuilder.RenameColumn(
                name: "CustomerCustId",
                table: "ShoppingCarts",
                newName: "ProductVeriationveriationId");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingCarts_CustomerCustId",
                table: "ShoppingCarts",
                newName: "IX_ShoppingCarts_ProductVeriationveriationId");

            migrationBuilder.AddColumn<int>(
                name: "OrderNumber",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCarts_productVeriations_ProductVeriationveriationId",
                table: "ShoppingCarts",
                column: "ProductVeriationveriationId",
                principalTable: "productVeriations",
                principalColumn: "veriationId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
