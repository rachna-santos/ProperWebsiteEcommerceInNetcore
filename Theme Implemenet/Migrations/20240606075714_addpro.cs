using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Theme_Implemenet.Migrations
{
    public partial class addpro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "promotionmappings");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "promotionmappings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    productId = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    Endate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Startdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    proName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    values = table.Column<int>(type: "int", nullable: false)
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
                        name: "FK_promotionmappings_statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "statuses",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_promotionmappings_productId",
                table: "promotionmappings",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_promotionmappings_StatusId",
                table: "promotionmappings",
                column: "StatusId");
        }
    }
}
