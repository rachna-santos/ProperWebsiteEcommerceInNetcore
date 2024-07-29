using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Theme_Implemenet.Migrations
{
    public partial class addstatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "promotionmappings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_promotionmappings_StatusId",
                table: "promotionmappings",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_promotionmappings_statuses_StatusId",
                table: "promotionmappings",
                column: "StatusId",
                principalTable: "statuses",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_promotionmappings_statuses_StatusId",
                table: "promotionmappings");

            migrationBuilder.DropIndex(
                name: "IX_promotionmappings_StatusId",
                table: "promotionmappings");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "promotionmappings");
        }
    }
}
