using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Himchistka.Api.Migrations
{
    /// <inheritdoc />
    public partial class addPriceForPlaces : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "MounthPrice",
                table: "Places",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MounthPrice",
                table: "Places");
        }
    }
}
