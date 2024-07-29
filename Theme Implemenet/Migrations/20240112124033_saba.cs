using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Theme_Implemenet.Migrations
{
    public partial class saba : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Modulepagespermissions_AspNetRoles_RoleId",
                table: "Modulepagespermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Modulepagespermissions_modules_ModuleId",
                table: "Modulepagespermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Modulepagespermissions_pages_PagesId",
                table: "Modulepagespermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Modulepagespermissions_statuses_StatusId",
                table: "Modulepagespermissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Modulepagespermissions",
                table: "Modulepagespermissions");

            migrationBuilder.RenameTable(
                name: "Modulepagespermissions",
                newName: "modulepagespermissions");

            migrationBuilder.RenameIndex(
                name: "IX_Modulepagespermissions_StatusId",
                table: "modulepagespermissions",
                newName: "IX_modulepagespermissions_StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Modulepagespermissions_RoleId",
                table: "modulepagespermissions",
                newName: "IX_modulepagespermissions_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_Modulepagespermissions_PagesId",
                table: "modulepagespermissions",
                newName: "IX_modulepagespermissions_PagesId");

            migrationBuilder.RenameIndex(
                name: "IX_Modulepagespermissions_ModuleId",
                table: "modulepagespermissions",
                newName: "IX_modulepagespermissions_ModuleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_modulepagespermissions",
                table: "modulepagespermissions",
                column: "modulepagespermissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_modulepagespermissions_AspNetRoles_RoleId",
                table: "modulepagespermissions",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_modulepagespermissions_modules_ModuleId",
                table: "modulepagespermissions",
                column: "ModuleId",
                principalTable: "modules",
                principalColumn: "ModuleId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_modulepagespermissions_pages_PagesId",
                table: "modulepagespermissions",
                column: "PagesId",
                principalTable: "pages",
                principalColumn: "PagesId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_modulepagespermissions_statuses_StatusId",
                table: "modulepagespermissions",
                column: "StatusId",
                principalTable: "statuses",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_modulepagespermissions_AspNetRoles_RoleId",
                table: "modulepagespermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_modulepagespermissions_modules_ModuleId",
                table: "modulepagespermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_modulepagespermissions_pages_PagesId",
                table: "modulepagespermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_modulepagespermissions_statuses_StatusId",
                table: "modulepagespermissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_modulepagespermissions",
                table: "modulepagespermissions");

            migrationBuilder.RenameTable(
                name: "modulepagespermissions",
                newName: "Modulepagespermissions");

            migrationBuilder.RenameIndex(
                name: "IX_modulepagespermissions_StatusId",
                table: "Modulepagespermissions",
                newName: "IX_Modulepagespermissions_StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_modulepagespermissions_RoleId",
                table: "Modulepagespermissions",
                newName: "IX_Modulepagespermissions_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_modulepagespermissions_PagesId",
                table: "Modulepagespermissions",
                newName: "IX_Modulepagespermissions_PagesId");

            migrationBuilder.RenameIndex(
                name: "IX_modulepagespermissions_ModuleId",
                table: "Modulepagespermissions",
                newName: "IX_Modulepagespermissions_ModuleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Modulepagespermissions",
                table: "Modulepagespermissions",
                column: "modulepagespermissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Modulepagespermissions_AspNetRoles_RoleId",
                table: "Modulepagespermissions",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Modulepagespermissions_modules_ModuleId",
                table: "Modulepagespermissions",
                column: "ModuleId",
                principalTable: "modules",
                principalColumn: "ModuleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Modulepagespermissions_pages_PagesId",
                table: "Modulepagespermissions",
                column: "PagesId",
                principalTable: "pages",
                principalColumn: "PagesId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Modulepagespermissions_statuses_StatusId",
                table: "Modulepagespermissions",
                column: "StatusId",
                principalTable: "statuses",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
