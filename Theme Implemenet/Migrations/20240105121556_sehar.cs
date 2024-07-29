using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Theme_Implemenet.Migrations
{
    public partial class sehar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_userproductpermissions_AspNetUsers_Id",
                table: "userproductpermissions");

            migrationBuilder.DropIndex(
                name: "IX_userproductpermissions_Id",
                table: "userproductpermissions");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "userproductpermissions",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
