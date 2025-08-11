using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRequestAndUpdateDimension : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Diameter",
                table: "Dimensions");

            migrationBuilder.DropColumn(
                name: "Length",
                table: "Dimensions");

            migrationBuilder.DropColumn(
                name: "Thickness",
                table: "Dimensions");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "Dimensions");

            migrationBuilder.RenameColumn(
                name: "Size",
                table: "Dimensions",
                newName: "Name");

            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Requests");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Dimensions",
                newName: "Size");

            migrationBuilder.AddColumn<decimal>(
                name: "Diameter",
                table: "Dimensions",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Length",
                table: "Dimensions",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Thickness",
                table: "Dimensions",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Width",
                table: "Dimensions",
                type: "TEXT",
                nullable: true);
        }
    }
}
