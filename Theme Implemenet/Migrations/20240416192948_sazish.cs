using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Theme_Implemenet.Migrations
{
    public partial class sazish : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_inventories_customers_CustomerCustId",
                table: "inventories");

            migrationBuilder.DropForeignKey(
                name: "FK_inventories_productColors_productcolorId",
                table: "inventories");

            migrationBuilder.DropForeignKey(
                name: "FK_inventories_productSizes_productsizeId",
                table: "inventories");

            migrationBuilder.DropIndex(
                name: "IX_inventories_CustomerCustId",
                table: "inventories");

            migrationBuilder.DropIndex(
                name: "IX_inventories_productcolorId",
                table: "inventories");

            migrationBuilder.DropIndex(
                name: "IX_inventories_productsizeId",
                table: "inventories");

            migrationBuilder.DropColumn(
                name: "CustomerCustId",
                table: "inventories");

            migrationBuilder.DropColumn(
                name: "productcolorId",
                table: "inventories");

            migrationBuilder.DropColumn(
                name: "productsizeId",
                table: "inventories");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomerCustId",
                table: "inventories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "productcolorId",
                table: "inventories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "productsizeId",
                table: "inventories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_inventories_CustomerCustId",
                table: "inventories",
                column: "CustomerCustId");

            migrationBuilder.CreateIndex(
                name: "IX_inventories_productcolorId",
                table: "inventories",
                column: "productcolorId");

            migrationBuilder.CreateIndex(
                name: "IX_inventories_productsizeId",
                table: "inventories",
                column: "productsizeId");

            migrationBuilder.AddForeignKey(
                name: "FK_inventories_customers-CustId",
                table: "inventories",
                column: "CustId",
                principalTable: "customers",
                principalColumn: "CustId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_inventories_productColors_productcolorId",
                table: "inventories",
                column: "productcolorId",
                principalTable: "productColors",
                principalColumn: "productcolorId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_inventories_productSizes_productsizeId",
                table: "inventories",
                column: "productsizeId",
                principalTable: "productSizes",
                principalColumn: "productsizeId",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
