using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Theme_Implemenet.Migrations
{
    public partial class numan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RememberMe",
                table: "logins");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "productVeriations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RetailerPrice",
                table: "productVeriations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "costprice",
                table: "productVeriations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "productVeriations");

            migrationBuilder.DropColumn(
                name: "RetailerPrice",
                table: "productVeriations");

            migrationBuilder.DropColumn(
                name: "costprice",
                table: "productVeriations");

            migrationBuilder.AddColumn<bool>(
                name: "RememberMe",
                table: "logins",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
