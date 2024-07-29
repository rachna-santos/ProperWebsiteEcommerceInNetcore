using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Theme_Implemenet.Migrations
{
    public partial class sanarosh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "rewies");

            migrationBuilder.DropTable(
                name: "ShoppingCarts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "rewies",
                columns: table => new
                {
                    ReviewId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id = table.Column<int>(type: "int", nullable: false),
                    productId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lastmodifield = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    comment = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rewies", x => x.ReviewId);
                    table.ForeignKey(
                        name: "FK_rewies_product_productId",
                        column: x => x.productId,
                        principalTable: "product",
                        principalColumn: "productId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_rewies_signups_Id",
                        column: x => x.Id,
                        principalTable: "signups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingCarts",
                columns: table => new
                {
                    cartId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id = table.Column<int>(type: "int", nullable: false),
                    veriationId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Discount = table.Column<int>(type: "int", nullable: false),
                    Lastmodifield = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    bill = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    image = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCarts", x => x.cartId);
                    table.ForeignKey(
                        name: "FK_ShoppingCarts_productVeriations_veriationId",
                        column: x => x.veriationId,
                        principalTable: "productVeriations",
                        principalColumn: "veriationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShoppingCarts_signups_Id",
                        column: x => x.Id,
                        principalTable: "signups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_rewies_Id",
                table: "rewies",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_rewies_productId",
                table: "rewies",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_Id",
                table: "ShoppingCarts",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_veriationId",
                table: "ShoppingCarts",
                column: "veriationId");
        }
    }
}
