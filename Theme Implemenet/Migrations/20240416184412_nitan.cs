using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Theme_Implemenet.Migrations
{
    public partial class nitan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_inventories_customers_CustomerCustId",
                table: "inventories");

            migrationBuilder.DropIndex(
                name: "IX_inventories_CustomerCustId",
                table: "inventories");

            migrationBuilder.DropColumn(
                name: "CustId",
                table: "inventories");

            migrationBuilder.DropColumn(
                name: "CustomerCustId",
                table: "inventories");

            migrationBuilder.AddColumn<decimal>(
                name: "totalamount",
                table: "inventories",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "totalamount",
                table: "inventories");

            migrationBuilder.AddColumn<int>(
                name: "CustId",
                table: "inventories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CustomerCustId",
                table: "inventories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_inventories_CustomerCustId",
                table: "inventories",
                column: "CustomerCustId");

            migrationBuilder.AddForeignKey(
                name: "FK_inventories_customers_CustomerCustId",
                table: "inventories",
                column: "CustomerCustId",
                principalTable: "customers",
                principalColumn: "CustId",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
