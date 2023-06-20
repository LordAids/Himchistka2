using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Himchistka.Api.Migrations
{
    /// <inheritdoc />
    public partial class changeIdService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderService_Services_ServicesServiceId",
                table: "OrderService");

            migrationBuilder.DropForeignKey(
                name: "FK_PlaceService_Services_ServicesServiceId",
                table: "PlaceService");

            migrationBuilder.RenameColumn(
                name: "ServiceId",
                table: "Services",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ServicesServiceId",
                table: "PlaceService",
                newName: "ServicesId");

            migrationBuilder.RenameIndex(
                name: "IX_PlaceService_ServicesServiceId",
                table: "PlaceService",
                newName: "IX_PlaceService_ServicesId");

            migrationBuilder.RenameColumn(
                name: "ServicesServiceId",
                table: "OrderService",
                newName: "ServicesId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderService_ServicesServiceId",
                table: "OrderService",
                newName: "IX_OrderService_ServicesId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderService_Services_ServicesId",
                table: "OrderService",
                column: "ServicesId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlaceService_Services_ServicesId",
                table: "PlaceService",
                column: "ServicesId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderService_Services_ServicesId",
                table: "OrderService");

            migrationBuilder.DropForeignKey(
                name: "FK_PlaceService_Services_ServicesId",
                table: "PlaceService");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Services",
                newName: "ServiceId");

            migrationBuilder.RenameColumn(
                name: "ServicesId",
                table: "PlaceService",
                newName: "ServicesServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_PlaceService_ServicesId",
                table: "PlaceService",
                newName: "IX_PlaceService_ServicesServiceId");

            migrationBuilder.RenameColumn(
                name: "ServicesId",
                table: "OrderService",
                newName: "ServicesServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderService_ServicesId",
                table: "OrderService",
                newName: "IX_OrderService_ServicesServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderService_Services_ServicesServiceId",
                table: "OrderService",
                column: "ServicesServiceId",
                principalTable: "Services",
                principalColumn: "ServiceId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlaceService_Services_ServicesServiceId",
                table: "PlaceService",
                column: "ServicesServiceId",
                principalTable: "Services",
                principalColumn: "ServiceId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
