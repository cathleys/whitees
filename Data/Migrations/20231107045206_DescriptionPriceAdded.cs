using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Whitees.Data.Migrations
{
    /// <inheritdoc />
    public partial class DescriptionPriceAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Models");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Shirts",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Shirts",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Shirts");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Shirts");

            migrationBuilder.CreateTable(
                name: "Models",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ShirtId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Models", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Models_Shirts_ShirtId",
                        column: x => x.ShirtId,
                        principalTable: "Shirts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Models_ShirtId",
                table: "Models",
                column: "ShirtId");
        }
    }
}
