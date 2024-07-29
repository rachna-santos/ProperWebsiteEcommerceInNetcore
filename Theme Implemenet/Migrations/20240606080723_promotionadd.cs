using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Theme_Implemenet.Migrations
{
    public partial class promotionadd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "promotions",
                columns: table => new
                {
                    proId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_promotions", x => x.proId);
                    table.ForeignKey(
                        name: "FK_promotions_statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "statuses",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "promotionmappings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    productId = table.Column<int>(type: "int", nullable: false),
                    proId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_promotionmappings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_promotionmappings_product_productId",
                        column: x => x.productId,
                        principalTable: "product",
                        principalColumn: "productId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_promotionmappings_promotions_proId",
                        column: x => x.proId,
                        principalTable: "promotions",
                        principalColumn: "proId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_promotionmappings_productId",
                table: "promotionmappings",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_promotionmappings_proId",
                table: "promotionmappings",
                column: "proId");

            migrationBuilder.CreateIndex(
                name: "IX_promotions_StatusId",
                table: "promotions",
                column: "StatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "promotionmappings");

            migrationBuilder.DropTable(
                name: "promotions");
        }
    }
}
