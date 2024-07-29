using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Theme_Implemenet.Migrations
{
    public partial class pinky : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "userproductpermissions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RegisterId",
                table: "userproductpermissions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "userproductpermissions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_userproductpermissions_Id",
                table: "userproductpermissions",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_userproductpermissions_RegisterId",
                table: "userproductpermissions",
                column: "RegisterId");

            migrationBuilder.AddForeignKey(
                name: "FK_userproductpermissions_AspNetUsers_Id",
                table: "userproductpermissions",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_userproductpermissions_AspNetUsers_RegisterId",
                table: "userproductpermissions",
                column: "RegisterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_userproductpermissions_AspNetUsers_Id",
                table: "userproductpermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_userproductpermissions_AspNetUsers_RegisterId",
                table: "userproductpermissions");

            migrationBuilder.DropIndex(
                name: "IX_userproductpermissions_Id",
                table: "userproductpermissions");

            migrationBuilder.DropIndex(
                name: "IX_userproductpermissions_RegisterId",
                table: "userproductpermissions");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "userproductpermissions");

            migrationBuilder.DropColumn(
                name: "RegisterId",
                table: "userproductpermissions");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "userproductpermissions");
        }
    }
}
