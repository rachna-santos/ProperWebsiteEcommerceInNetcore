using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Theme_Implemenet.Migrations
{
    public partial class hamda : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_userproductpermissions_AspNetUsers_RoleId",
                table: "userproductpermissions");

            migrationBuilder.DropIndex(
                name: "IX_userproductpermissions_RoleId",
                table: "userproductpermissions");

            migrationBuilder.AlterColumn<string>(
                name: "RoleId",
                table: "userproductpermissions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "userproductpermissions",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_userproductpermissions_Id",
                table: "userproductpermissions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_userproductpermissions_AspNetUsers_Id",
                table: "userproductpermissions",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_userproductpermissions_AspNetUsers_Id",
                table: "userproductpermissions");

            migrationBuilder.DropIndex(
                name: "IX_userproductpermissions_Id",
                table: "userproductpermissions");

            migrationBuilder.AlterColumn<string>(
                name: "RoleId",
                table: "userproductpermissions",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "userproductpermissions",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_userproductpermissions_RoleId",
                table: "userproductpermissions",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_userproductpermissions_AspNetUsers_RoleId",
                table: "userproductpermissions",
                column: "RoleId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
