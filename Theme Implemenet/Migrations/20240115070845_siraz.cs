using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Theme_Implemenet.Migrations
{
    public partial class siraz : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "userproductpermissions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "userproductpermissions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
