using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Theme_Implemenet.Migrations
{
    public partial class sara : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoles_AspNetRoles_ApplicationRoleId",
                table: "AspNetRoles");

            migrationBuilder.RenameColumn(
                name: "ApplicationRoleId",
                table: "AspNetRoles",
                newName: "CreateRoleRoleId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetRoles_ApplicationRoleId",
                table: "AspNetRoles",
                newName: "IX_AspNetRoles_CreateRoleRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoles_CreateRole_CreateRoleRoleId",
                table: "AspNetRoles",
                column: "CreateRoleRoleId",
                principalTable: "CreateRole",
                principalColumn: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoles_CreateRole_CreateRoleRoleId",
                table: "AspNetRoles");

            migrationBuilder.RenameColumn(
                name: "CreateRoleRoleId",
                table: "AspNetRoles",
                newName: "ApplicationRoleId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetRoles_CreateRoleRoleId",
                table: "AspNetRoles",
                newName: "IX_AspNetRoles_ApplicationRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoles_AspNetRoles_ApplicationRoleId",
                table: "AspNetRoles",
                column: "ApplicationRoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id");
        }
    }
}
