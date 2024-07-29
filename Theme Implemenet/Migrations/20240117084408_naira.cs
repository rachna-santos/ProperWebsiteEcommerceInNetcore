using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Theme_Implemenet.Migrations
{
    public partial class naira : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_userproductpermissions_AspNetUsers_Id",
                table: "userproductpermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_userproductpermissions_product_productId",
                table: "userproductpermissions");

            migrationBuilder.DropIndex(
                name: "IX_userproductpermissions_Id",
                table: "userproductpermissions");

            migrationBuilder.DropIndex(
                name: "IX_userproductpermissions_productId",
                table: "userproductpermissions");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "userproductpermissions");

            migrationBuilder.DropColumn(
                name: "productId",
                table: "userproductpermissions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "userproductpermissions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "productId",
                table: "userproductpermissions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_userproductpermissions_Id",
                table: "userproductpermissions",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_userproductpermissions_productId",
                table: "userproductpermissions",
                column: "productId");

            migrationBuilder.AddForeignKey(
                name: "FK_userproductpermissions_AspNetUsers_Id",
                table: "userproductpermissions",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_userproductpermissions_product_productId",
                table: "userproductpermissions",
                column: "productId",
                principalTable: "product",
                principalColumn: "productId",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
