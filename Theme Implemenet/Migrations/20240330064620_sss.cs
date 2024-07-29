using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Theme_Implemenet.Migrations
{
    public partial class sss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_paymentTypes_payments_PaymentId",
                table: "paymentTypes");

            migrationBuilder.RenameColumn(
                name: "PaymentId",
                table: "paymentTypes",
                newName: "PaymentTypeId");

            migrationBuilder.RenameColumn(
                name: "PaymentId",
                table: "payments",
                newName: "PaymentTypeId");

            migrationBuilder.AlterColumn<int>(
                name: "PaymentTypeId",
                table: "paymentTypes",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_payments_PaymentTypeId",
                table: "payments",
                column: "PaymentTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_payments_paymentTypes_PaymentTypeId",
                table: "payments",
                column: "PaymentTypeId",
                principalTable: "paymentTypes",
                principalColumn: "PaymentTypeId",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_payments_paymentTypes_PaymentTypeId",
                table: "payments");

            migrationBuilder.DropIndex(
                name: "IX_payments_PaymentTypeId",
                table: "payments");

            migrationBuilder.RenameColumn(
                name: "PaymentTypeId",
                table: "paymentTypes",
                newName: "PaymentId");

            migrationBuilder.RenameColumn(
                name: "PaymentTypeId",
                table: "payments",
                newName: "PaymentId");

            migrationBuilder.AlterColumn<int>(
                name: "PaymentId",
                table: "paymentTypes",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_paymentTypes_payments_PaymentId",
                table: "paymentTypes",
                column: "PaymentId",
                principalTable: "payments",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
