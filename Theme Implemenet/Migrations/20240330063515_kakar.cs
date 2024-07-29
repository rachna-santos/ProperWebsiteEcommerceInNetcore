using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Theme_Implemenet.Migrations
{
    public partial class kakar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_customers_statuses_StatusId",
                table: "customers");

            migrationBuilder.DropIndex(
                name: "IX_customers_StatusId",
                table: "customers");

            migrationBuilder.DropColumn(
                name: "type",
                table: "payments");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "customers");

            migrationBuilder.AlterColumn<decimal>(
                name: "bill",
                table: "ShoppingCarts",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "PaymentId",
                table: "payments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "customers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "paymentTypes",
                columns: table => new
                {
                    PaymentId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_paymentTypes", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_paymentTypes_payments_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "payments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "paymentTypes");

            migrationBuilder.DropColumn(
                name: "PaymentId",
                table: "payments");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "customers");

            migrationBuilder.AlterColumn<int>(
                name: "bill",
                table: "ShoppingCarts",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<string>(
                name: "type",
                table: "payments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "customers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_customers_StatusId",
                table: "customers",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_customers_statuses_StatusId",
                table: "customers",
                column: "StatusId",
                principalTable: "statuses",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
