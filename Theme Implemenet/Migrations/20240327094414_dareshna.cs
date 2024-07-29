using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Theme_Implemenet.Migrations
{
    public partial class dareshna : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_productVeriations_ProductVeriationveriationId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ProductVeriationveriationId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ProductVeriationveriationId",
                table: "Orders");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_veriationId",
                table: "Orders",
                column: "veriationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_productVeriations_veriationId",
                table: "Orders",
                column: "veriationId",
                principalTable: "productVeriations",
                principalColumn: "veriationId",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_productVeriations_veriationId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_veriationId",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "ProductVeriationveriationId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ProductVeriationveriationId",
                table: "Orders",
                column: "ProductVeriationveriationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_productVeriations_ProductVeriationveriationId",
                table: "Orders",
                column: "ProductVeriationveriationId",
                principalTable: "productVeriations",
                principalColumn: "veriationId",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
