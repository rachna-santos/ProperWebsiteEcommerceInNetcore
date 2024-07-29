using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Theme_Implemenet.Migrations
{
    public partial class rekha : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoles_AspNetRoles_CreateRoleId",
                table: "AspNetRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_modulepagespermissions_AspNetRoles_RoleId",
                table: "modulepagespermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_userproductpermissions_AspNetRoles_RoleId",
                table: "userproductpermissions");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "RoleName",
                table: "AspNetRoles");

            migrationBuilder.RenameColumn(
                name: "CreateRoleId",
                table: "AspNetRoles",
                newName: "CreateRoleRoleId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetRoles_CreateRoleId",
                table: "AspNetRoles",
                newName: "IX_AspNetRoles_CreateRoleRoleId");

            migrationBuilder.CreateTable(
                name: "CreateRole",
                columns: table => new
                {
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreateRole", x => x.RoleId);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoles_CreateRole_CreateRoleRoleId",
                table: "AspNetRoles",
                column: "CreateRoleRoleId",
                principalTable: "CreateRole",
                principalColumn: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_modulepagespermissions_CreateRole_RoleId",
                table: "modulepagespermissions",
                column: "RoleId",
                principalTable: "CreateRole",
                principalColumn: "RoleId");

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
                name: "FK_AspNetRoles_CreateRole_CreateRoleRoleId",
                table: "AspNetRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_modulepagespermissions_CreateRole_RoleId",
                table: "modulepagespermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_userproductpermissions_CreateRole_RoleId",
                table: "userproductpermissions");

            migrationBuilder.DropTable(
                name: "CreateRole");

            migrationBuilder.RenameColumn(
                name: "CreateRoleRoleId",
                table: "AspNetRoles",
                newName: "CreateRoleId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetRoles_CreateRoleRoleId",
                table: "AspNetRoles",
                newName: "IX_AspNetRoles_CreateRoleId");

            migrationBuilder.AddColumn<string>(
                name: "RoleId",
                table: "AspNetRoles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RoleName",
                table: "AspNetRoles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoles_AspNetRoles_CreateRoleId",
                table: "AspNetRoles",
                column: "CreateRoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_modulepagespermissions_AspNetRoles_RoleId",
                table: "modulepagespermissions",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_userproductpermissions_AspNetRoles_RoleId",
                table: "userproductpermissions",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id");
        }
    }
}
