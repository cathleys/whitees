using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Whitees.Data.Migrations
{
    /// <inheritdoc />
    public partial class AppUserShoppingCartItemAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "ShoppingCartItems",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "ShoppingCartItems");
        }
    }
}
