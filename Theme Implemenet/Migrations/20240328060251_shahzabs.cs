using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Theme_Implemenet.Migrations
{
    public partial class shahzabs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCarts_customers_CustId",
                table: "ShoppingCarts");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCarts_CustId",
                table: "ShoppingCarts");

            migrationBuilder.DropColumn(
                name: "CustId",
                table: "ShoppingCarts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustId",
                table: "ShoppingCarts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_CustId",
                table: "ShoppingCarts",
                column: "CustId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCarts_customers_CustId",
                table: "ShoppingCarts",
                column: "CustId",
                principalTable: "customers",
                principalColumn: "CustId",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
