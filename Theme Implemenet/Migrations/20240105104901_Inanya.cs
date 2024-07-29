using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Theme_Implemenet.Migrations
{
    public partial class Inanya : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    categoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    categoryName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    tag = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Keyword = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    imagepath = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lastmodifield = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.categoryId);
                    table.ForeignKey(
                        name: "FK_categories_statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "statuses",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "countries",
                columns: table => new
                {
                    countryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    countryName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_countries", x => x.countryId);
                });

            migrationBuilder.CreateTable(
                name: "CreateRole",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreateRole", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "modules",
                columns: table => new
                {
                    ModuleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModuleName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lastmodifield = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_modules", x => x.ModuleId);
                    table.ForeignKey(
                        name: "FK_modules_statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "statuses",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "productBrands",
                columns: table => new
                {
                    productbrandId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    productbrandName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    productbrandphoneNo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    tag = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    imagepath = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lastmodifield = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productBrands", x => x.productbrandId);
                    table.ForeignKey(
                        name: "FK_productBrands_statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "statuses",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "productColors",
                columns: table => new
                {
                    productcolorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    productcolorName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    colorcode1 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    colorcode2 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    imagepath = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lastmodifield = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productColors", x => x.productcolorId);
                    table.ForeignKey(
                        name: "FK_productColors_statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "statuses",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "productgenders",
                columns: table => new
                {
                    productgenderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    productgenderName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    tag = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Keyword = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    imagepath = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lastmodifield = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productgenders", x => x.productgenderId);
                    table.ForeignKey(
                        name: "FK_productgenders_statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "statuses",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "productMaterials",
                columns: table => new
                {
                    productmaterialId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    productmaterialname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    tag = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Keyword = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    imagepath = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lastmodifield = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productMaterials", x => x.productmaterialId);
                    table.ForeignKey(
                        name: "FK_productMaterials_statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "statuses",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "productSeasons",
                columns: table => new
                {
                    productseasonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    productseasonName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    tag = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Keyword = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    imagepath = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lastmodifield = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productSeasons", x => x.productseasonId);
                    table.ForeignKey(
                        name: "FK_productSeasons_statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "statuses",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "productSizes",
                columns: table => new
                {
                    productsizeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    productsizeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lastmodifield = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productSizes", x => x.productsizeId);
                    table.ForeignKey(
                        name: "FK_productSizes_statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "statuses",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "subcategories",
                columns: table => new
                {
                    subcategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    subcategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    tag = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Keyword = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    imagepath = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    categoryId = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lastmodifield = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subcategories", x => x.subcategoryId);
                    table.ForeignKey(
                        name: "FK_subcategories_categories_categoryId",
                        column: x => x.categoryId,
                        principalTable: "categories",
                        principalColumn: "categoryId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_subcategories_statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "statuses",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "cities",
                columns: table => new
                {
                    cityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cityName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    countryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cities", x => x.cityId);
                    table.ForeignKey(
                        name: "FK_cities_countries_countryId",
                        column: x => x.countryId,
                        principalTable: "countries",
                        principalColumn: "countryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pages",
                columns: table => new
                {
                    PagesId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PagesName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lastmodifield = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModuleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pages", x => x.PagesId);
                    table.ForeignKey(
                        name: "FK_pages_modules_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "modules",
                        principalColumn: "ModuleId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_pages_statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "statuses",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "categoriestyle",
                columns: table => new
                {
                    categorystyleid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    categorystyleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    tag = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Keyword = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    imagepath = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    categoryId = table.Column<int>(type: "int", nullable: false),
                    subcategoryId = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lastmodifield = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categoriestyle", x => x.categorystyleid);
                    table.ForeignKey(
                        name: "FK_categoriestyle_categories_categoryId",
                        column: x => x.categoryId,
                        principalTable: "categories",
                        principalColumn: "categoryId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_categoriestyle_statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "statuses",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_categoriestyle_subcategories_subcategoryId",
                        column: x => x.subcategoryId,
                        principalTable: "subcategories",
                        principalColumn: "subcategoryId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "modulepagespermissions",
                columns: table => new
                {
                    modulepagespermissionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateRoleRoleId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PagesId = table.Column<int>(type: "int", nullable: false),
                    ModuleId = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lastmodifield = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_modulepagespermissions", x => x.modulepagespermissionId);
                    table.ForeignKey(
                        name: "FK_modulepagespermissions_CreateRole_CreateRoleRoleId",
                        column: x => x.CreateRoleRoleId,
                        principalTable: "CreateRole",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_modulepagespermissions_modules_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "modules",
                        principalColumn: "ModuleId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_modulepagespermissions_pages_PagesId",
                        column: x => x.PagesId,
                        principalTable: "pages",
                        principalColumn: "PagesId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_modulepagespermissions_statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "statuses",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "product",
                columns: table => new
                {
                    productId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    productName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    productcod = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    productimage = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    sizeguideimage = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Keyword = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    categoryId = table.Column<int>(type: "int", nullable: false),
                    subcategoryId = table.Column<int>(type: "int", nullable: false),
                    categorystyleid = table.Column<int>(type: "int", nullable: false),
                    productseasonId = table.Column<int>(type: "int", nullable: false),
                    productgenderId = table.Column<int>(type: "int", nullable: false),
                    productmaterialId = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lastmodifield = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product", x => x.productId);
                    table.ForeignKey(
                        name: "FK_product_categories_categoryId",
                        column: x => x.categoryId,
                        principalTable: "categories",
                        principalColumn: "categoryId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_product_categoriestyle_categorystyleid",
                        column: x => x.categorystyleid,
                        principalTable: "categoriestyle",
                        principalColumn: "categorystyleid",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_product_productgenders_productgenderId",
                        column: x => x.productgenderId,
                        principalTable: "productgenders",
                        principalColumn: "productgenderId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_product_productMaterials_productmaterialId",
                        column: x => x.productmaterialId,
                        principalTable: "productMaterials",
                        principalColumn: "productmaterialId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_product_productSeasons_productseasonId",
                        column: x => x.productseasonId,
                        principalTable: "productSeasons",
                        principalColumn: "productseasonId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_product_statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "statuses",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_product_subcategories_subcategoryId",
                        column: x => x.subcategoryId,
                        principalTable: "subcategories",
                        principalColumn: "subcategoryId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "productVeriations",
                columns: table => new
                {
                    veriationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    veriationName = table.Column<int>(type: "int", maxLength: 200, nullable: false),
                    productid = table.Column<int>(type: "int", nullable: false),
                    productcolorId = table.Column<int>(type: "int", nullable: false),
                    productsizeId = table.Column<int>(type: "int", nullable: false),
                    categoryId = table.Column<int>(type: "int", nullable: false),
                    subcategoryId = table.Column<int>(type: "int", nullable: false),
                    categorystyleid = table.Column<int>(type: "int", nullable: false),
                    productseasonId = table.Column<int>(type: "int", nullable: false),
                    productgenderId = table.Column<int>(type: "int", nullable: false),
                    productmaterialId = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lastmodifield = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productVeriations", x => x.veriationId);
                    table.ForeignKey(
                        name: "FK_productVeriations_categories_categoryId",
                        column: x => x.categoryId,
                        principalTable: "categories",
                        principalColumn: "categoryId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_productVeriations_categoriestyle_categorystyleid",
                        column: x => x.categorystyleid,
                        principalTable: "categoriestyle",
                        principalColumn: "categorystyleid",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_productVeriations_product_productid",
                        column: x => x.productid,
                        principalTable: "product",
                        principalColumn: "productId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_productVeriations_productColors_productcolorId",
                        column: x => x.productcolorId,
                        principalTable: "productColors",
                        principalColumn: "productcolorId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_productVeriations_productgenders_productgenderId",
                        column: x => x.productgenderId,
                        principalTable: "productgenders",
                        principalColumn: "productgenderId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_productVeriations_productMaterials_productmaterialId",
                        column: x => x.productmaterialId,
                        principalTable: "productMaterials",
                        principalColumn: "productmaterialId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_productVeriations_productSeasons_productseasonId",
                        column: x => x.productseasonId,
                        principalTable: "productSeasons",
                        principalColumn: "productseasonId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_productVeriations_productSizes_productsizeId",
                        column: x => x.productsizeId,
                        principalTable: "productSizes",
                        principalColumn: "productsizeId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_productVeriations_statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "statuses",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_productVeriations_subcategories_subcategoryId",
                        column: x => x.subcategoryId,
                        principalTable: "subcategories",
                        principalColumn: "subcategoryId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "userproductpermissions",
                columns: table => new
                {
                    userproductid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegisterId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Id = table.Column<int>(type: "int", nullable: false),
                    productid = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lastmodifield = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userproductpermissions", x => x.userproductid);
                    table.ForeignKey(
                        name: "FK_userproductpermissions_AspNetUsers_RegisterId",
                        column: x => x.RegisterId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_userproductpermissions_product_productid",
                        column: x => x.productid,
                        principalTable: "product",
                        principalColumn: "productId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_userproductpermissions_statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "statuses",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_categories_StatusId",
                table: "categories",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_categoriestyle_categoryId",
                table: "categoriestyle",
                column: "categoryId");

            migrationBuilder.CreateIndex(
                name: "IX_categoriestyle_StatusId",
                table: "categoriestyle",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_categoriestyle_subcategoryId",
                table: "categoriestyle",
                column: "subcategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_cities_countryId",
                table: "cities",
                column: "countryId");

            migrationBuilder.CreateIndex(
                name: "IX_modulepagespermissions_CreateRoleRoleId",
                table: "modulepagespermissions",
                column: "CreateRoleRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_modulepagespermissions_ModuleId",
                table: "modulepagespermissions",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_modulepagespermissions_PagesId",
                table: "modulepagespermissions",
                column: "PagesId");

            migrationBuilder.CreateIndex(
                name: "IX_modulepagespermissions_StatusId",
                table: "modulepagespermissions",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_modules_StatusId",
                table: "modules",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_pages_ModuleId",
                table: "pages",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_pages_StatusId",
                table: "pages",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_product_categoryId",
                table: "product",
                column: "categoryId");

            migrationBuilder.CreateIndex(
                name: "IX_product_categorystyleid",
                table: "product",
                column: "categorystyleid");

            migrationBuilder.CreateIndex(
                name: "IX_product_productgenderId",
                table: "product",
                column: "productgenderId");

            migrationBuilder.CreateIndex(
                name: "IX_product_productmaterialId",
                table: "product",
                column: "productmaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_product_productseasonId",
                table: "product",
                column: "productseasonId");

            migrationBuilder.CreateIndex(
                name: "IX_product_StatusId",
                table: "product",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_product_subcategoryId",
                table: "product",
                column: "subcategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_productBrands_StatusId",
                table: "productBrands",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_productColors_StatusId",
                table: "productColors",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_productgenders_StatusId",
                table: "productgenders",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_productMaterials_StatusId",
                table: "productMaterials",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_productSeasons_StatusId",
                table: "productSeasons",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_productSizes_StatusId",
                table: "productSizes",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_productVeriations_categoryId",
                table: "productVeriations",
                column: "categoryId");

            migrationBuilder.CreateIndex(
                name: "IX_productVeriations_categorystyleid",
                table: "productVeriations",
                column: "categorystyleid");

            migrationBuilder.CreateIndex(
                name: "IX_productVeriations_productcolorId",
                table: "productVeriations",
                column: "productcolorId");

            migrationBuilder.CreateIndex(
                name: "IX_productVeriations_productgenderId",
                table: "productVeriations",
                column: "productgenderId");

            migrationBuilder.CreateIndex(
                name: "IX_productVeriations_productid",
                table: "productVeriations",
                column: "productid");

            migrationBuilder.CreateIndex(
                name: "IX_productVeriations_productmaterialId",
                table: "productVeriations",
                column: "productmaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_productVeriations_productseasonId",
                table: "productVeriations",
                column: "productseasonId");

            migrationBuilder.CreateIndex(
                name: "IX_productVeriations_productsizeId",
                table: "productVeriations",
                column: "productsizeId");

            migrationBuilder.CreateIndex(
                name: "IX_productVeriations_StatusId",
                table: "productVeriations",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_productVeriations_subcategoryId",
                table: "productVeriations",
                column: "subcategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_subcategories_categoryId",
                table: "subcategories",
                column: "categoryId");

            migrationBuilder.CreateIndex(
                name: "IX_subcategories_StatusId",
                table: "subcategories",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_userproductpermissions_productid",
                table: "userproductpermissions",
                column: "productid");

            migrationBuilder.CreateIndex(
                name: "IX_userproductpermissions_RegisterId",
                table: "userproductpermissions",
                column: "RegisterId");

            migrationBuilder.CreateIndex(
                name: "IX_userproductpermissions_StatusId",
                table: "userproductpermissions",
                column: "StatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cities");

            migrationBuilder.DropTable(
                name: "modulepagespermissions");

            migrationBuilder.DropTable(
                name: "productBrands");

            migrationBuilder.DropTable(
                name: "productVeriations");

            migrationBuilder.DropTable(
                name: "userproductpermissions");

            migrationBuilder.DropTable(
                name: "countries");

            migrationBuilder.DropTable(
                name: "CreateRole");

            migrationBuilder.DropTable(
                name: "pages");

            migrationBuilder.DropTable(
                name: "productColors");

            migrationBuilder.DropTable(
                name: "productSizes");

            migrationBuilder.DropTable(
                name: "product");

            migrationBuilder.DropTable(
                name: "modules");

            migrationBuilder.DropTable(
                name: "categoriestyle");

            migrationBuilder.DropTable(
                name: "productgenders");

            migrationBuilder.DropTable(
                name: "productMaterials");

            migrationBuilder.DropTable(
                name: "productSeasons");

            migrationBuilder.DropTable(
                name: "subcategories");

            migrationBuilder.DropTable(
                name: "categories");
        }
    }
}
