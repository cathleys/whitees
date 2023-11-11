using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Whitees.Data.Migrations
{
    /// <inheritdoc />
    public partial class ShirtonCartAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShirtId",
                table: "ShoppingCartItems",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartItems_ShirtId",
                table: "ShoppingCartItems",
                column: "ShirtId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartItems_Shirts_ShirtId",
                table: "ShoppingCartItems",
                column: "ShirtId",
                principalTable: "Shirts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartItems_Shirts_ShirtId",
                table: "ShoppingCartItems");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCartItems_ShirtId",
                table: "ShoppingCartItems");

            migrationBuilder.DropColumn(
                name: "ShirtId",
                table: "ShoppingCartItems");
        }
    }
}
