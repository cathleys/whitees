using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Whitees.Data.Migrations
{
    /// <inheritdoc />
    public partial class ShirtAppUserAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Shirts",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Shirts_AppUserId",
                table: "Shirts",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shirts_AspNetUsers_AppUserId",
                table: "Shirts",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shirts_AspNetUsers_AppUserId",
                table: "Shirts");

            migrationBuilder.DropIndex(
                name: "IX_Shirts_AppUserId",
                table: "Shirts");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Shirts");
        }
    }
}
