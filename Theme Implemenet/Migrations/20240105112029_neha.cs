using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Theme_Implemenet.Migrations
{
    public partial class neha : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_modulepagespermissions_CreateRole_CreateRoleRoleId",
                table: "modulepagespermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_userproductpermissions_AspNetUsers_RegisterId",
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
                name: "IX_modulepagespermissions_CreateRoleRoleId",
                table: "modulepagespermissions");

            migrationBuilder.DropColumn(
                name: "CreateRoleRoleId",
                table: "userproductpermissions");

            migrationBuilder.DropColumn(
                name: "CreateRoleRoleId",
                table: "modulepagespermissions");

            migrationBuilder.RenameColumn(
                name: "RegisterId",
                table: "userproductpermissions",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_userproductpermissions_RegisterId",
                table: "userproductpermissions",
                newName: "IX_userproductpermissions_ApplicationUserId");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationRoleId",
                table: "userproductpermissions",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationRoleId",
                table: "modulepagespermissions",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_userproductpermissions_ApplicationRoleId",
                table: "userproductpermissions",
                column: "ApplicationRoleId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_modulepagespermissions_AspNetRoles_ApplicationRoleId",
                table: "modulepagespermissions");

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
                name: "IX_modulepagespermissions_ApplicationRoleId",
                table: "modulepagespermissions");

            migrationBuilder.DropColumn(
                name: "ApplicationRoleId",
                table: "userproductpermissions");

            migrationBuilder.DropColumn(
                name: "ApplicationRoleId",
                table: "modulepagespermissions");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "userproductpermissions",
                newName: "RegisterId");

            migrationBuilder.RenameIndex(
                name: "IX_userproductpermissions_ApplicationUserId",
                table: "userproductpermissions",
                newName: "IX_userproductpermissions_RegisterId");

            migrationBuilder.AddColumn<int>(
                name: "CreateRoleRoleId",
                table: "userproductpermissions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CreateRoleRoleId",
                table: "modulepagespermissions",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
                name: "IX_modulepagespermissions_CreateRoleRoleId",
                table: "modulepagespermissions",
                column: "CreateRoleRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_modulepagespermissions_CreateRole_CreateRoleRoleId",
                table: "modulepagespermissions",
                column: "CreateRoleRoleId",
                principalTable: "CreateRole",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_userproductpermissions_AspNetUsers_RegisterId",
                table: "userproductpermissions",
                column: "RegisterId",
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
    }
}
