using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Theme_Implemenet.Migrations
{
    public partial class santosh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "grossamount",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "netamount",
                table: "Orders",
                newName: "subtotal");

            migrationBuilder.AlterColumn<int>(
                name: "bill",
                table: "ShoppingCarts",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "ShoppingCarts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "color",
                table: "ShoppingCarts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "image",
                table: "ShoppingCarts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Size",
                table: "ShoppingCarts");

            migrationBuilder.DropColumn(
                name: "color",
                table: "ShoppingCarts");

            migrationBuilder.DropColumn(
                name: "image",
                table: "ShoppingCarts");

            migrationBuilder.RenameColumn(
                name: "subtotal",
                table: "Orders",
                newName: "netamount");

            migrationBuilder.AlterColumn<decimal>(
                name: "bill",
                table: "ShoppingCarts",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "grossamount",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
