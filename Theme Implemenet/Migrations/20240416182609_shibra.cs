using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Theme_Implemenet.Migrations
{
    public partial class shibra : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_inventories_productBrands_productbrandId",
                table: "inventories");

            migrationBuilder.DropIndex(
                name: "IX_inventories_productbrandId",
                table: "inventories");

            migrationBuilder.DropColumn(
                name: "productbrandId",
                table: "inventories");

            migrationBuilder.RenameColumn(
                name: "custId",
                table: "inventories",
                newName: "CustId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CustId",
                table: "inventories",
                newName: "custId");

            migrationBuilder.AddColumn<int>(
                name: "productbrandId",
                table: "inventories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_inventories_productbrandId",
                table: "inventories",
                column: "productbrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_inventories_productBrands_productbrandId",
                table: "inventories",
                column: "productbrandId",
                principalTable: "productBrands",
                principalColumn: "productbrandId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
