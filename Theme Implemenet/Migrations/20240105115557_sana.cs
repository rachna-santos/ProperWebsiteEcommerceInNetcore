using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Theme_Implemenet.Migrations
{
    public partial class sana : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_userproductpermissions_AspNetRoles_ApplicationRoleId",
                table: "userproductpermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_userproductpermissions_AspNetUsers_ApplicationUserId",
                table: "userproductpermissions");

            migrationBuilder.DropIndex(
                name: "IX_userproductpermissions_ApplicationRoleId",
                table: "userproductpermissions");

            migrationBuilder.DropIndex(
                name: "IX_userproductpermissions_ApplicationUserId",
                table: "userproductpermissions");

            migrationBuilder.DropColumn(
                name: "ApplicationRoleId",
                table: "userproductpermissions");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "userproductpermissions");

            migrationBuilder.AlterColumn<string>(
                name: "RoleId",
                table: "userproductpermissions",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "CreateRoleRoleId",
                table: "userproductpermissions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CreateRoleRoleId",
                table: "AspNetRoles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CreateRole",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreateRole", x => x.RoleId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_userproductpermissions_CreateRoleRoleId",
                table: "userproductpermissions",
                column: "CreateRoleRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_userproductpermissions_RoleId",
                table: "userproductpermissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoles_CreateRoleRoleId",
                table: "AspNetRoles",
                column: "CreateRoleRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoles_CreateRole_CreateRoleRoleId",
                table: "AspNetRoles",
                column: "CreateRoleRoleId",
                principalTable: "CreateRole",
                principalColumn: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_userproductpermissions_AspNetUsers_RoleId",
                table: "userproductpermissions",
                column: "RoleId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

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
                name: "FK_AspNetRoles_CreateRole_CreateRoleRoleId",
                table: "AspNetRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_userproductpermissions_AspNetUsers_RoleId",
                table: "userproductpermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_userproductpermissions_CreateRole_CreateRoleRoleId",
                table: "userproductpermissions");

            migrationBuilder.DropTable(
                name: "CreateRole");

            migrationBuilder.DropIndex(
                name: "IX_userproductpermissions_CreateRoleRoleId",
                table: "userproductpermissions");

            migrationBuilder.DropIndex(
                name: "IX_userproductpermissions_RoleId",
                table: "userproductpermissions");

            migrationBuilder.DropIndex(
                name: "IX_AspNetRoles_CreateRoleRoleId",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "CreateRoleRoleId",
                table: "userproductpermissions");

            migrationBuilder.DropColumn(
                name: "CreateRoleRoleId",
                table: "AspNetRoles");

            migrationBuilder.AlterColumn<string>(
                name: "RoleId",
                table: "userproductpermissions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationRoleId",
                table: "userproductpermissions",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "userproductpermissions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_userproductpermissions_ApplicationRoleId",
                table: "userproductpermissions",
                column: "ApplicationRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_userproductpermissions_ApplicationUserId",
                table: "userproductpermissions",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_userproductpermissions_AspNetRoles_ApplicationRoleId",
                table: "userproductpermissions",
                column: "ApplicationRoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_userproductpermissions_AspNetUsers_ApplicationUserId",
                table: "userproductpermissions",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
