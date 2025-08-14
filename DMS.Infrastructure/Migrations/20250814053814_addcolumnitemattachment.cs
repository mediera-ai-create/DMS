using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addcolumnitemattachment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Attachments",
                table: "Items",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Attachments",
                table: "Items");
        }
    }
}
