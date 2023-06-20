using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Himchistka.Api.Migrations
{
    /// <inheritdoc />
    public partial class updateOrdersAndClients : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PlaceId",
                table: "Orders",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Birthday",
                table: "Clients",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PlaceId",
                table: "Orders",
                column: "PlaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Places_PlaceId",
                table: "Orders",
                column: "PlaceId",
                principalTable: "Places",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Places_PlaceId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_PlaceId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PlaceId",
                table: "Orders");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Birthday",
                table: "Clients",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");
        }
    }
}
