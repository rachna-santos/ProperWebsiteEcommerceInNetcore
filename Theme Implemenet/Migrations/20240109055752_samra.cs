using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Theme_Implemenet.Migrations
{
    public partial class samra : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoles_CreateRole_CreateRoleRoleId",
                table: "AspNetRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoles_statuses_StatusId",
                table: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "modulepagespermissions");

            migrationBuilder.DropTable(
                name: "userproductpermissions");

            migrationBuilder.DropTable(
                name: "CreateRole");

            migrationBuilder.DropIndex(
                name: "IX_AspNetRoles_CreateRoleRoleId",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "CreateRoleRoleId",
                table: "AspNetRoles");

            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RoleName",
                table: "AspNetRoles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoles_statuses_StatusId",
                table: "AspNetRoles",
                column: "StatusId",
                principalTable: "statuses",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoles_statuses_StatusId",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "RoleName",
                table: "AspNetRoles");

            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                table: "AspNetUsers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "CreateRoleRoleId",
                table: "AspNetRoles",
                type: "nvarchar(450)",
                nullable: true);

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

            migrationBuilder.CreateTable(
                name: "modulepagespermissions",
                columns: table => new
                {
                    modulepagespermissionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModuleId = table.Column<int>(type: "int", nullable: false),
                    PagesId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lastmodifield = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_modulepagespermissions", x => x.modulepagespermissionId);
                    table.ForeignKey(
                        name: "FK_modulepagespermissions_CreateRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "CreateRole",
                        principalColumn: "RoleId");
                    table.ForeignKey(
                        name: "FK_modulepagespermissions_modules_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "modules",
                        principalColumn: "ModuleId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_modulepagespermissions_pages_PagesId",
                        column: x => x.PagesId,
                        principalTable: "pages",
                        principalColumn: "PagesId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_modulepagespermissions_statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "statuses",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "userproductpermissions",
                columns: table => new
                {
                    userproductid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    productid = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lastmodifield = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userproductpermissions", x => x.userproductid);
                    table.ForeignKey(
                        name: "FK_userproductpermissions_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_userproductpermissions_CreateRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "CreateRole",
                        principalColumn: "RoleId");
                    table.ForeignKey(
                        name: "FK_userproductpermissions_product_productid",
                        column: x => x.productid,
                        principalTable: "product",
                        principalColumn: "productId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_userproductpermissions_statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "statuses",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoles_CreateRoleRoleId",
                table: "AspNetRoles",
                column: "CreateRoleRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_modulepagespermissions_ModuleId",
                table: "modulepagespermissions",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_modulepagespermissions_PagesId",
                table: "modulepagespermissions",
                column: "PagesId");

            migrationBuilder.CreateIndex(
                name: "IX_modulepagespermissions_RoleId",
                table: "modulepagespermissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_modulepagespermissions_StatusId",
                table: "modulepagespermissions",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_userproductpermissions_Id",
                table: "userproductpermissions",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_userproductpermissions_productid",
                table: "userproductpermissions",
                column: "productid");

            migrationBuilder.CreateIndex(
                name: "IX_userproductpermissions_RoleId",
                table: "userproductpermissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_userproductpermissions_StatusId",
                table: "userproductpermissions",
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
                principalColumn: "StatusId");
        }
    }
}
