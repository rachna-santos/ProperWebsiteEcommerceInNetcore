using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Theme_Implemenet.Migrations
{
    public partial class removedata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "shippingCharges",
                table: "ShoppingCarts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "shippingCharges",
                table: "ShoppingCarts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
