using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Theme_Implemenet.Migrations
{
    public partial class aliamran : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoles_CreateRole_CreateRoleRoleId",
                table: "AspNetRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_userproductpermissions_CreateRole_CreateRoleRoleId",
                table: "userproductpermissions");

            migrationBuilder.DropTable(
                name: "CreateRole");

            migrationBuilder.DropIndex(
                name: "IX_userproductpermissions_CreateRoleRoleId",
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "FK_userproductpermissions_CreateRole_CreateRoleRoleId",
                table: "userproductpermissions",
                column: "CreateRoleRoleId",
                principalTable: "CreateRole",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
