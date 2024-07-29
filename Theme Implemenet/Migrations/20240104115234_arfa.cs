using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Theme_Implemenet.Migrations
{
    public partial class arfa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CreateRole");

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "AspNetRoles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "AspNetRoles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetRoles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "Lastmodifield",
                table: "AspNetRoles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "AspNetRoles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoles_CompanyId",
                table: "AspNetRoles",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoles_StatusId",
                table: "AspNetRoles",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoles_companies_CompanyId",
                table: "AspNetRoles",
                column: "CompanyId",
                principalTable: "companies",
                principalColumn: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoles_statuses_StatusId",
                table: "AspNetRoles",
                column: "StatusId",
                principalTable: "statuses",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoles_companies_CompanyId",
                table: "AspNetRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoles_statuses_StatusId",
                table: "AspNetRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetRoles_CompanyId",
                table: "AspNetRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetRoles_StatusId",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "Lastmodifield",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "AspNetRoles");

            migrationBuilder.CreateTable(
                name: "CreateRole",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lastmodifield = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreateRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CreateRole_companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "companies",
                        principalColumn: "CompanyId");
                    table.ForeignKey(
                        name: "FK_CreateRole_statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "statuses",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CreateRole_CompanyId",
                table: "CreateRole",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CreateRole_StatusId",
                table: "CreateRole",
                column: "StatusId");
        }
    }
}
