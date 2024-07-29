using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Theme_Implemenet.Migrations
{
    public partial class shahzab : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_payments_customers_CustomerCustId",
                table: "payments");

            migrationBuilder.DropIndex(
                name: "IX_payments_CustomerCustId",
                table: "payments");

            migrationBuilder.DropColumn(
                name: "CustomerCustId",
                table: "payments");

            migrationBuilder.CreateIndex(
                name: "IX_payments_CustId",
                table: "payments",
                column: "CustId");

            migrationBuilder.AddForeignKey(
                name: "FK_payments_customers_CustId",
                table: "payments",
                column: "CustId",
                principalTable: "customers",
                principalColumn: "CustId",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_payments_customers_CustId",
                table: "payments");

            migrationBuilder.DropIndex(
                name: "IX_payments_CustId",
                table: "payments");

            migrationBuilder.AddColumn<int>(
                name: "CustomerCustId",
                table: "payments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_payments_CustomerCustId",
                table: "payments",
                column: "CustomerCustId");

            migrationBuilder.AddForeignKey(
                name: "FK_payments_customers_CustomerCustId",
                table: "payments",
                column: "CustomerCustId",
                principalTable: "customers",
                principalColumn: "CustId",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
