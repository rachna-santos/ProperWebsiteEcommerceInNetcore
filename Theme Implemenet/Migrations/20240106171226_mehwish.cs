using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Theme_Implemenet.Migrations
{
    public partial class mehwish : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoles_companies_CreateRole_CompanyId",
                table: "AspNetRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoles_statuses_CompanyId",
                table: "AspNetRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoles_statuses_CreateRole_StatusId",
                table: "AspNetRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetRoles_CreateRole_CompanyId",
                table: "AspNetRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetRoles_CreateRole_StatusId",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "RoleName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CreateRole_CompanyId",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "CreateRole_CreateDate",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "CreateRole_Lastmodifield",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "CreateRole_StatusId",
                table: "AspNetRoles");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoles_StatusId",
                table: "AspNetRoles",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoles_statuses_StatusId",
                table: "AspNetRoles",
                column: "StatusId",
                principalTable: "statuses",
                principalColumn: "StatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoles_statuses_StatusId",
                table: "AspNetRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetRoles_StatusId",
                table: "AspNetRoles");

            migrationBuilder.AddColumn<string>(
                name: "RoleName",
                table: "AspNetUsers",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreateRole_CompanyId",
                table: "AspNetRoles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateRole_CreateDate",
                table: "AspNetRoles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateRole_Lastmodifield",
                table: "AspNetRoles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreateRole_StatusId",
                table: "AspNetRoles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoles_CreateRole_CompanyId",
                table: "AspNetRoles",
                column: "CreateRole_CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoles_CreateRole_StatusId",
                table: "AspNetRoles",
                column: "CreateRole_StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoles_companies_CreateRole_CompanyId",
                table: "AspNetRoles",
                column: "CreateRole_CompanyId",
                principalTable: "companies",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoles_statuses_CompanyId",
                table: "AspNetRoles",
                column: "CompanyId",
                principalTable: "statuses",
                principalColumn: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoles_statuses_CreateRole_StatusId",
                table: "AspNetRoles",
                column: "CreateRole_StatusId",
                principalTable: "statuses",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
