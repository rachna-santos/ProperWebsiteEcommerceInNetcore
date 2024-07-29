using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Theme_Implemenet.Migrations
{
    public partial class khala : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_customers_cities_cityId",
                table: "customers");

            migrationBuilder.DropForeignKey(
                name: "FK_customers_companies_CompanyId",
                table: "customers");

            migrationBuilder.DropIndex(
                name: "IX_customers_cityId",
                table: "customers");

            migrationBuilder.DropIndex(
                name: "IX_customers_CompanyId",
                table: "customers");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "customers");

            migrationBuilder.DropColumn(
                name: "cityId",
                table: "customers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "customers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "cityId",
                table: "customers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_customers_cityId",
                table: "customers",
                column: "cityId");

            migrationBuilder.CreateIndex(
                name: "IX_customers_CompanyId",
                table: "customers",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_customers_cities_cityId",
                table: "customers",
                column: "cityId",
                principalTable: "cities",
                principalColumn: "cityId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_customers_companies_CompanyId",
                table: "customers",
                column: "CompanyId",
                principalTable: "companies",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
