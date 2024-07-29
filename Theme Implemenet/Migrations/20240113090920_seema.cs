using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Theme_Implemenet.Migrations
{
    public partial class seema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_productVeriations_product_productid",
                table: "productVeriations");

            migrationBuilder.RenameColumn(
                name: "productid",
                table: "productVeriations",
                newName: "productId");

            migrationBuilder.RenameIndex(
                name: "IX_productVeriations_productid",
                table: "productVeriations",
                newName: "IX_productVeriations_productId");

            migrationBuilder.AddForeignKey(
                name: "FK_productVeriations_product_productId",
                table: "productVeriations",
                column: "productId",
                principalTable: "product",
                principalColumn: "productId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_productVeriations_product_productId",
                table: "productVeriations");

            migrationBuilder.RenameColumn(
                name: "productId",
                table: "productVeriations",
                newName: "productid");

            migrationBuilder.RenameIndex(
                name: "IX_productVeriations_productId",
                table: "productVeriations",
                newName: "IX_productVeriations_productid");

            migrationBuilder.AddForeignKey(
                name: "FK_productVeriations_product_productid",
                table: "productVeriations",
                column: "productid",
                principalTable: "product",
                principalColumn: "productId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
