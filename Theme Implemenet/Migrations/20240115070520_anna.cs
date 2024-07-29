using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Theme_Implemenet.Migrations
{
    public partial class anna : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_userproductpermissions_AspNetUsers_RegisterId",
                table: "userproductpermissions");

            migrationBuilder.DropIndex(
                name: "IX_userproductpermissions_RegisterId",
                table: "userproductpermissions");

            migrationBuilder.DropColumn(
                name: "RegisterId",
                table: "userproductpermissions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RegisterId",
                table: "userproductpermissions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_userproductpermissions_RegisterId",
                table: "userproductpermissions",
                column: "RegisterId");

            migrationBuilder.AddForeignKey(
                name: "FK_userproductpermissions_AspNetUsers_RegisterId",
                table: "userproductpermissions",
                column: "RegisterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
