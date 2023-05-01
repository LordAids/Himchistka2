using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Himchistka.Api.Migrations
{
    /// <inheritdoc />
    public partial class fixmanyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderServices");

            migrationBuilder.CreateTable(
                name: "OrderService",
                columns: table => new
                {
                    OrdersId = table.Column<Guid>(type: "uuid", nullable: false),
                    ServicesServiceId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderService", x => new { x.OrdersId, x.ServicesServiceId });
                    table.ForeignKey(
                        name: "FK_OrderService_Orders_OrdersId",
                        column: x => x.OrdersId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderService_Services_ServicesServiceId",
                        column: x => x.ServicesServiceId,
                        principalTable: "Services",
                        principalColumn: "ServiceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderService_ServicesServiceId",
                table: "OrderService",
                column: "ServicesServiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderService");

            migrationBuilder.CreateTable(
                name: "OrderServices",
                columns: table => new
                {
                    OrderServiceId = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderServices", x => x.OrderServiceId);
                    table.ForeignKey(
                        name: "FK_OrderServices_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderServices_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "ServiceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderServices_OrderId",
                table: "OrderServices",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderServices_ServiceId",
                table: "OrderServices",
                column: "ServiceId");
        }
    }
}
