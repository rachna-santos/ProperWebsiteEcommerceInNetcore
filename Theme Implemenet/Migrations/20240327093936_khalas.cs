using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Theme_Implemenet.Migrations
{
    public partial class khalas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_customers_CustomerCustId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCarts_customers_CustomerCustId",
                table: "ShoppingCarts");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCarts_CustomerCustId",
                table: "ShoppingCarts");

            migrationBuilder.DropIndex(
                name: "IX_Orders_CustomerCustId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CustomerCustId",
                table: "ShoppingCarts");

            migrationBuilder.DropColumn(
                name: "CustomerCustId",
                table: "Orders");

            migrationBuilder.AlterColumn<decimal>(
                name: "bill",
                table: "ShoppingCarts",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_CustId",
                table: "ShoppingCarts",
                column: "CustId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustId",
                table: "Orders",
                column: "CustId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_customers_CustId",
                table: "Orders",
                column: "CustId",
                principalTable: "customers",
                principalColumn: "CustId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCarts_customers_CustId",
                table: "ShoppingCarts",
                column: "CustId",
                principalTable: "customers",
                principalColumn: "CustId",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_customers_CustId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCarts_customers_CustId",
                table: "ShoppingCarts");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCarts_CustId",
                table: "ShoppingCarts");

            migrationBuilder.DropIndex(
                name: "IX_Orders_CustId",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "bill",
                table: "ShoppingCarts",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<int>(
                name: "CustomerCustId",
                table: "ShoppingCarts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CustomerCustId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_CustomerCustId",
                table: "ShoppingCarts",
                column: "CustomerCustId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerCustId",
                table: "Orders",
                column: "CustomerCustId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_customers_CustomerCustId",
                table: "Orders",
                column: "CustomerCustId",
                principalTable: "customers",
                principalColumn: "CustId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCarts_customers_CustomerCustId",
                table: "ShoppingCarts",
                column: "CustomerCustId",
                principalTable: "customers",
                principalColumn: "CustId",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
