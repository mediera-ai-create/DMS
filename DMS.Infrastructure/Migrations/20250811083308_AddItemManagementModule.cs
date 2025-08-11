using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddItemManagementModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "Dealers",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CompanyName = table.Column<string>(type: "TEXT", nullable: false),
                    Address1 = table.Column<string>(type: "TEXT", nullable: false),
                    Address2 = table.Column<string>(type: "TEXT", nullable: true),
                    City = table.Column<string>(type: "TEXT", nullable: false),
                    State = table.Column<string>(type: "TEXT", nullable: false),
                    Country = table.Column<string>(type: "TEXT", nullable: false),
                    GSTIN = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dimensions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Size = table.Column<string>(type: "TEXT", nullable: false),
                    Thickness = table.Column<decimal>(type: "TEXT", nullable: true),
                    Length = table.Column<decimal>(type: "TEXT", nullable: true),
                    Width = table.Column<decimal>(type: "TEXT", nullable: true),
                    Diameter = table.Column<decimal>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dimensions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItemCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MaterialTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BrandId = table.Column<int>(type: "INTEGER", nullable: true),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: true),
                    MaterialTypeId = table.Column<int>(type: "INTEGER", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Dimension1Id = table.Column<int>(type: "INTEGER", nullable: true),
                    Dimension2Id = table.Column<int>(type: "INTEGER", nullable: true),
                    Dimension3Id = table.Column<int>(type: "INTEGER", nullable: true),
                    ItemCategory = table.Column<string>(type: "TEXT", nullable: false),
                    MaterialTypeName = table.Column<string>(type: "TEXT", nullable: false),
                    Grade = table.Column<string>(type: "TEXT", nullable: false),
                    HasTestCertificate = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Items_Dimensions_Dimension1Id",
                        column: x => x.Dimension1Id,
                        principalTable: "Dimensions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Items_Dimensions_Dimension2Id",
                        column: x => x.Dimension2Id,
                        principalTable: "Dimensions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Items_Dimensions_Dimension3Id",
                        column: x => x.Dimension3Id,
                        principalTable: "Dimensions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Items_ItemCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ItemCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Items_MaterialTypes_MaterialTypeId",
                        column: x => x.MaterialTypeId,
                        principalTable: "MaterialTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ItemAttachments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ItemId = table.Column<int>(type: "INTEGER", nullable: false),
                    FileName = table.Column<string>(type: "TEXT", nullable: false),
                    FilePath = table.Column<string>(type: "TEXT", nullable: false),
                    FileType = table.Column<string>(type: "TEXT", nullable: false),
                    UploadedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemAttachments_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemAttachments_ItemId",
                table: "ItemAttachments",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_BrandId",
                table: "Items",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_CategoryId",
                table: "Items",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_Dimension1Id",
                table: "Items",
                column: "Dimension1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Items_Dimension2Id",
                table: "Items",
                column: "Dimension2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Items_Dimension3Id",
                table: "Items",
                column: "Dimension3Id");

            migrationBuilder.CreateIndex(
                name: "IX_Items_MaterialTypeId",
                table: "Items",
                column: "MaterialTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemAttachments");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Dimensions");

            migrationBuilder.DropTable(
                name: "ItemCategories");

            migrationBuilder.DropTable(
                name: "MaterialTypes");

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "Dealers",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);
        }
    }
}
