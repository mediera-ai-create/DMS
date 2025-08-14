using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatedActivity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DealerName",
                table: "Activities",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ItemName",
                table: "Activities",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RequestName",
                table: "Activities",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DealerName",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "ItemName",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "RequestName",
                table: "Activities");
        }
    }
}
