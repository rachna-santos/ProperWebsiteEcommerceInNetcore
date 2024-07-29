using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Theme_Implemenet.Migrations
{
    public partial class football : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_rewies_signups_SignupId",
                table: "rewies");

            migrationBuilder.DropIndex(
                name: "IX_rewies_SignupId",
                table: "rewies");

            migrationBuilder.DropColumn(
                name: "SignupId",
                table: "rewies");

            migrationBuilder.CreateIndex(
                name: "IX_rewies_Id",
                table: "rewies",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_rewies_signups_Id",
                table: "rewies",
                column: "Id",
                principalTable: "signups",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_rewies_signups_Id",
                table: "rewies");

            migrationBuilder.DropIndex(
                name: "IX_rewies_Id",
                table: "rewies");

            migrationBuilder.AddColumn<int>(
                name: "SignupId",
                table: "rewies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_rewies_SignupId",
                table: "rewies",
                column: "SignupId");

            migrationBuilder.AddForeignKey(
                name: "FK_rewies_signups_SignupId",
                table: "rewies",
                column: "SignupId",
                principalTable: "signups",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
