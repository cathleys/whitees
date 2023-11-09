using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Whitees.Data.Migrations
{
    /// <inheritdoc />
    public partial class ShirtSaleSizeAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShirtSale",
                table: "Shirts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ShirtSize",
                table: "Shirts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShirtSale",
                table: "Shirts");

            migrationBuilder.DropColumn(
                name: "ShirtSize",
                table: "Shirts");
        }
    }
}
