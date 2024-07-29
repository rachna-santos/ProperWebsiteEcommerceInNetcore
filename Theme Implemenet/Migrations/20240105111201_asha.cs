using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Theme_Implemenet.Migrations
{
    public partial class asha : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreateRoleRoleId",
                table: "userproductpermissions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "RoleId",
                table: "userproductpermissions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_userproductpermissions_CreateRoleRoleId",
                table: "userproductpermissions",
                column: "CreateRoleRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_userproductpermissions_CreateRole_CreateRoleRoleId",
                table: "userproductpermissions",
                column: "CreateRoleRoleId",
                principalTable: "CreateRole",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_userproductpermissions_CreateRole_CreateRoleRoleId",
                table: "userproductpermissions");

            migrationBuilder.DropIndex(
                name: "IX_userproductpermissions_CreateRoleRoleId",
                table: "userproductpermissions");

            migrationBuilder.DropColumn(
                name: "CreateRoleRoleId",
                table: "userproductpermissions");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "userproductpermissions");
        }
    }
}
