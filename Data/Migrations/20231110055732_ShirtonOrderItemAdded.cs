using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Whitees.Data.Migrations
{
    /// <inheritdoc />
    public partial class ShirtonOrderItemAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShirtId",
                table: "OrderItems",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ShirtId",
                table: "OrderItems",
                column: "ShirtId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Shirts_ShirtId",
                table: "OrderItems",
                column: "ShirtId",
                principalTable: "Shirts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Shirts_ShirtId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_ShirtId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "ShirtId",
                table: "OrderItems");
        }
    }
}
