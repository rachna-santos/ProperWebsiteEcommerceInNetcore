using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Theme_Implemenet.Migrations
{
    public partial class sar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_inventories_product_productId",
                table: "inventories");

            migrationBuilder.RenameColumn(
                name: "productId",
                table: "inventories",
                newName: "veriationId");

            migrationBuilder.RenameIndex(
                name: "IX_inventories_productId",
                table: "inventories",
                newName: "IX_inventories_veriationId");

            migrationBuilder.AddForeignKey(
                name: "FK_inventories_productVeriations_veriationId",
                table: "inventories",
                column: "veriationId",
                principalTable: "productVeriations",
                principalColumn: "veriationId",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_inventories_productVeriations_veriationId",
                table: "inventories");

            migrationBuilder.RenameColumn(
                name: "veriationId",
                table: "inventories",
                newName: "productId");

            migrationBuilder.RenameIndex(
                name: "IX_inventories_veriationId",
                table: "inventories",
                newName: "IX_inventories_productId");

            migrationBuilder.AddForeignKey(
                name: "FK_inventories_product_productId",
                table: "inventories",
                column: "productId",
                principalTable: "product",
                principalColumn: "productId",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
