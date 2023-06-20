using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Himchistka.Api.Migrations
{
    /// <inheritdoc />
    public partial class addColordsToSpendingsAndServices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Spendings",
                type: "text",
                nullable: false,
                defaultValue: "#F44336");

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Services",
                type: "text",
                nullable: false,
                defaultValue: "#4CAF50");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "Spendings");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "Services");
        }
    }
}
