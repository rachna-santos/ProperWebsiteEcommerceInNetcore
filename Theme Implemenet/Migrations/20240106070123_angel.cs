using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Theme_Implemenet.Migrations
{
    public partial class angel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "userproductpermissions",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_userproductpermissions_RoleId",
                table: "userproductpermissions",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_userproductpermissions_CreateRole_RoleId",
                table: "userproductpermissions",
                column: "RoleId",
                principalTable: "CreateRole",
                principalColumn: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_userproductpermissions_CreateRole_RoleId",
                table: "userproductpermissions");

            migrationBuilder.DropIndex(
                name: "IX_userproductpermissions_RoleId",
                table: "userproductpermissions");

            migrationBuilder.AlterColumn<string>(
                name: "RoleId",
                table: "userproductpermissions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CreateRoleRoleId",
                table: "userproductpermissions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_userproductpermissions_CreateRoleRoleId",
                table: "userproductpermissions",
                column: "CreateRoleRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_userproductpermissions_CreateRole_CreateRoleRoleId",
                table: "userproductpermissions",
                column: "CreateRoleRoleId",
                principalTable: "CreateRole",
                principalColumn: "RoleId");
        }
    }
}
