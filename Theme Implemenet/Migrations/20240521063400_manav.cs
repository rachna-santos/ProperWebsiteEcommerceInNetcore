using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Theme_Implemenet.Migrations
{
    public partial class manav : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_statuses_StatusId",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                table: "Orders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "OderStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OderStatus", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_statuses_StatusId",
                table: "Orders",
                column: "StatusId",
                principalTable: "statuses",
                principalColumn: "StatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_statuses_StatusId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "OderStatus");

            migrationBuilder.AlterColumn<int>(
                name: "StatusId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_statuses_StatusId",
                table: "Orders",
                column: "StatusId",
                principalTable: "statuses",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
