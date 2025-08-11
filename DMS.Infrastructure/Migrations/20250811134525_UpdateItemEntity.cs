using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateItemEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Dimensions_Dimension1Id",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Dimensions_Dimension2Id",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Dimensions_Dimension3Id",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_Dimension1Id",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_Dimension2Id",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_Dimension3Id",
                table: "Items");

            migrationBuilder.AddColumn<string>(
                name: "Dimension1Value",
                table: "Items",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Dimension2Value",
                table: "Items",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Dimension3Value",
                table: "Items",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dimension1Value",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Dimension2Value",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Dimension3Value",
                table: "Items");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Dimensions_Dimension1Id",
                table: "Items",
                column: "Dimension1Id",
                principalTable: "Dimensions",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Dimensions_Dimension2Id",
                table: "Items",
                column: "Dimension2Id",
                principalTable: "Dimensions",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Dimensions_Dimension3Id",
                table: "Items",
                column: "Dimension3Id",
                principalTable: "Dimensions",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
