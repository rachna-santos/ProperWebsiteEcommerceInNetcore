using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Theme_Implemenet.Migrations
{
    public partial class gira : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_inventories_statuses_StatusId",
                table: "inventories");

            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                table: "inventories",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_inventories_statuses_StatusId",
                table: "inventories",
                column: "StatusId",
                principalTable: "statuses",
                principalColumn: "StatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_inventories_statuses_StatusId",
                table: "inventories");

            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                table: "inventories",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_inventories_statuses_StatusId",
                table: "inventories",
                column: "StatusId",
                principalTable: "statuses",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
