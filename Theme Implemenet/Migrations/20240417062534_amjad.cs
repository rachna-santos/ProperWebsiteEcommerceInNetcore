using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Theme_Implemenet.Migrations
{
    public partial class amjad : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "inventories");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "inventories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    veriationId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustId = table.Column<int>(type: "int", nullable: false),
                    Lastmodifield = table.Column<DateTime>(type: "datetime2", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    totalamount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inventories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_inventories_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_inventories_productVeriations_veriationId",
                        column: x => x.veriationId,
                        principalTable: "productVeriations",
                        principalColumn: "veriationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_inventories_statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "statuses",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_inventories_OrderId",
                table: "inventories",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_inventories_StatusId",
                table: "inventories",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_inventories_veriationId",
                table: "inventories",
                column: "veriationId");
        }
    }
}
