using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Himchistka.Api.Migrations
{
    /// <inheritdoc />
    public partial class addPlaces : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Places",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Adress = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Places", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlaceService",
                columns: table => new
                {
                    PlacesId = table.Column<Guid>(type: "uuid", nullable: false),
                    ServicesServiceId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaceService", x => new { x.PlacesId, x.ServicesServiceId });
                    table.ForeignKey(
                        name: "FK_PlaceService_Places_PlacesId",
                        column: x => x.PlacesId,
                        principalTable: "Places",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlaceService_Services_ServicesServiceId",
                        column: x => x.ServicesServiceId,
                        principalTable: "Services",
                        principalColumn: "ServiceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlaceService_ServicesServiceId",
                table: "PlaceService",
                column: "ServicesServiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlaceService");

            migrationBuilder.DropTable(
                name: "Places");
        }
    }
}
