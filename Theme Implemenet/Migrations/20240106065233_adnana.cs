using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Theme_Implemenet.Migrations
{
    public partial class adnana : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_modulepagespermissions_AspNetRoles_ApplicationRoleId",
                table: "modulepagespermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_userproductpermissions_AspNetUsers_RegisterId",
                table: "userproductpermissions");

            migrationBuilder.DropIndex(
                name: "IX_userproductpermissions_RegisterId",
                table: "userproductpermissions");

            migrationBuilder.DropIndex(
                name: "IX_modulepagespermissions_ApplicationRoleId",
                table: "modulepagespermissions");

            migrationBuilder.DropColumn(
                name: "RegisterId",
                table: "userproductpermissions");

            migrationBuilder.DropColumn(
                name: "ApplicationRoleId",
                table: "modulepagespermissions");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "userproductpermissions",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CreateRoleRoleId",
                table: "userproductpermissions",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "modulepagespermissions",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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
                name: "IX_userproductpermissions_Id",
                table: "userproductpermissions",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_modulepagespermissions_RoleId",
                table: "modulepagespermissions",
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
                name: "FK_modulepagespermissions_CreateRole_RoleId",
                table: "modulepagespermissions",
                column: "RoleId",
                principalTable: "CreateRole",
                principalColumn: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_userproductpermissions_AspNetUsers_Id",
                table: "userproductpermissions",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_userproductpermissions_CreateRole_CreateRoleRoleId",
                table: "userproductpermissions",
                column: "CreateRoleRoleId",
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
                name: "FK_userproductpermissions_AspNetUsers_Id",
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
                name: "IX_userproductpermissions_Id",
                table: "userproductpermissions");

            migrationBuilder.DropIndex(
                name: "IX_modulepagespermissions_RoleId",
                table: "modulepagespermissions");

            migrationBuilder.DropIndex(
                name: "IX_AspNetRoles_CreateRoleRoleId",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "CreateRoleRoleId",
                table: "userproductpermissions");

            migrationBuilder.DropColumn(
                name: "CreateRoleRoleId",
                table: "AspNetRoles");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "userproductpermissions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RegisterId",
                table: "userproductpermissions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RoleId",
                table: "modulepagespermissions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationRoleId",
                table: "modulepagespermissions",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_userproductpermissions_RegisterId",
                table: "userproductpermissions",
                column: "RegisterId");

            migrationBuilder.CreateIndex(
                name: "IX_modulepagespermissions_ApplicationRoleId",
                table: "modulepagespermissions",
                column: "ApplicationRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_modulepagespermissions_AspNetRoles_ApplicationRoleId",
                table: "modulepagespermissions",
                column: "ApplicationRoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_userproductpermissions_AspNetUsers_RegisterId",
                table: "userproductpermissions",
                column: "RegisterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
