using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Theme_Implemenet.Migrations
{
    public partial class arsalan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "productImages",
                columns: table => new
                {
                    productimageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    productId = table.Column<int>(type: "int", nullable: false),
                    iamgepath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    imagepath_thumb = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lastmodifield = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productImages", x => x.productimageId);
                    table.ForeignKey(
                        name: "FK_productImages_product_productId",
                        column: x => x.productId,
                        principalTable: "product",
                        principalColumn: "productId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_productImages_statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "statuses",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_productImages_productId",
                table: "productImages",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_productImages_StatusId",
                table: "productImages",
                column: "StatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "productImages");
        }
    }
}
