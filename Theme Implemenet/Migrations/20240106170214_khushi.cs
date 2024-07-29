using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Theme_Implemenet.Migrations
{
    public partial class khushi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoles_CreateRole_CreateRoleRoleId",
                table: "AspNetRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoles_statuses_StatusId",
                table: "AspNetRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_modulepagespermissions_CreateRole_RoleId",
                table: "modulepagespermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_userproductpermissions_CreateRole_RoleId",
                table: "userproductpermissions");

            migrationBuilder.DropTable(
                name: "CreateRole");

            migrationBuilder.DropIndex(
                name: "IX_AspNetRoles_StatusId",
                table: "AspNetRoles");

            migrationBuilder.RenameColumn(
                name: "CreateRoleRoleId",
                table: "AspNetRoles",
                newName: "CreateRole_StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetRoles_CreateRoleRoleId",
                table: "AspNetRoles",
                newName: "IX_AspNetRoles_CreateRole_StatusId");

            migrationBuilder.AlterColumn<string>(
                name: "RoleId",
                table: "userproductpermissions",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "RoleId",
                table: "modulepagespermissions",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "RoleName",
                table: "AspNetUsers",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreateRoleId",
                table: "AspNetRoles",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreateRole_CompanyId",
                table: "AspNetRoles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateRole_CreateDate",
                table: "AspNetRoles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateRole_Lastmodifield",
                table: "AspNetRoles",
                type: "datetime2",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoles_CreateRole_CompanyId",
                table: "AspNetRoles",
                column: "CreateRole_CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoles_CreateRoleId",
                table: "AspNetRoles",
                column: "CreateRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoles_AspNetRoles_CreateRoleId",
                table: "AspNetRoles",
                column: "CreateRoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoles_companies_CreateRole_CompanyId",
                table: "AspNetRoles",
                column: "CreateRole_CompanyId",
                principalTable: "companies",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoles_statuses_CompanyId",
                table: "AspNetRoles",
                column: "CompanyId",
                principalTable: "statuses",
                principalColumn: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoles_statuses_CreateRole_StatusId",
                table: "AspNetRoles",
                column: "CreateRole_StatusId",
                principalTable: "statuses",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.NoAction);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoles_AspNetRoles_CreateRoleId",
                table: "AspNetRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoles_companies_CreateRole_CompanyId",
                table: "AspNetRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoles_statuses_CompanyId",
                table: "AspNetRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoles_statuses_CreateRole_StatusId",
                table: "AspNetRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_modulepagespermissions_AspNetRoles_RoleId",
                table: "modulepagespermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_userproductpermissions_AspNetRoles_RoleId",
                table: "userproductpermissions");

            migrationBuilder.DropIndex(
                name: "IX_AspNetRoles_CreateRole_CompanyId",
                table: "AspNetRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetRoles_CreateRoleId",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "RoleName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CreateRoleId",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "CreateRole_CompanyId",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "CreateRole_CreateDate",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "CreateRole_Lastmodifield",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "RoleName",
                table: "AspNetRoles");

            migrationBuilder.RenameColumn(
                name: "CreateRole_StatusId",
                table: "AspNetRoles",
                newName: "CreateRoleRoleId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetRoles_CreateRole_StatusId",
                table: "AspNetRoles",
                newName: "IX_AspNetRoles_CreateRoleRoleId");

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "userproductpermissions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "modulepagespermissions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

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
                name: "IX_AspNetRoles_StatusId",
                table: "AspNetRoles",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoles_CreateRole_CreateRoleRoleId",
                table: "AspNetRoles",
                column: "CreateRoleRoleId",
                principalTable: "CreateRole",
                principalColumn: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoles_statuses_StatusId",
                table: "AspNetRoles",
                column: "StatusId",
                principalTable: "statuses",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.NoAction);

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
    }
}
