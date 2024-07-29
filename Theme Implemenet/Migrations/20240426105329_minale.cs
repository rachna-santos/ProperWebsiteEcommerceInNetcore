using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Theme_Implemenet.Migrations
{
    public partial class minale : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "rewies");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "rewies",
                columns: table => new
                {
                    ReviewId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    productId = table.Column<int>(type: "int", nullable: false),
                    SignupId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Id = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_rewies_signups_SignupId",
                        column: x => x.SignupId,
                        principalTable: "signups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_rewies_productId",
                table: "rewies",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_rewies_SignupId",
                table: "rewies",
                column: "SignupId");
        }
    }
}
