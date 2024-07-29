using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Theme_Implemenet.Migrations
{
    public partial class imge : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_userproductpermissions_product_productid",
                table: "userproductpermissions");

            migrationBuilder.RenameColumn(
                name: "productid",
                table: "userproductpermissions",
                newName: "productId");

            migrationBuilder.RenameIndex(
                name: "IX_userproductpermissions_productid",
                table: "userproductpermissions",
                newName: "IX_userproductpermissions_productId");

            migrationBuilder.AddForeignKey(
                name: "FK_userproductpermissions_product_productId",
                table: "userproductpermissions",
                column: "productId",
                principalTable: "product",
                principalColumn: "productId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_userproductpermissions_product_productId",
                table: "userproductpermissions");

            migrationBuilder.RenameColumn(
                name: "productId",
                table: "userproductpermissions",
                newName: "productid");

            migrationBuilder.RenameIndex(
                name: "IX_userproductpermissions_productId",
                table: "userproductpermissions",
                newName: "IX_userproductpermissions_productid");

            migrationBuilder.AddForeignKey(
                name: "FK_userproductpermissions_product_productid",
                table: "userproductpermissions",
                column: "productid",
                principalTable: "product",
                principalColumn: "productId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
