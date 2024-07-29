using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Theme_Implemenet.Migrations
{
    public partial class haris : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Modulepagespermissions",
                columns: table => new
                {
                    modulepagespermissionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PagesId = table.Column<int>(type: "int", nullable: false),
                    ModuleId = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lastmodifield = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modulepagespermissions", x => x.modulepagespermissionId);
                    table.ForeignKey(
                        name: "FK_Modulepagespermissions_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Modulepagespermissions_modules_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "modules",
                        principalColumn: "ModuleId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Modulepagespermissions_pages_PagesId",
                        column: x => x.PagesId,
                        principalTable: "pages",
                        principalColumn: "PagesId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Modulepagespermissions_statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "statuses",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Modulepagespermissions_ModuleId",
                table: "Modulepagespermissions",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_Modulepagespermissions_PagesId",
                table: "Modulepagespermissions",
                column: "PagesId");

            migrationBuilder.CreateIndex(
                name: "IX_Modulepagespermissions_RoleId",
                table: "Modulepagespermissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Modulepagespermissions_StatusId",
                table: "Modulepagespermissions",
                column: "StatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Modulepagespermissions");
        }
    }
}
