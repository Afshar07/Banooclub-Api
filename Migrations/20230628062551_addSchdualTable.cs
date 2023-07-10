using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BanooClub.Migrations
{
    public partial class addSchdualTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BecomeConsultantRequestSchedules",
                schema: "Consulting",
                columns: table => new
                {
                    BecomeConsultantRequestScheduleId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BecomeConsultantRequestId = table.Column<long>(type: "bigint", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EntTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BecomeConsultantRequestSchedules", x => x.BecomeConsultantRequestScheduleId);
                    table.ForeignKey(
                        name: "FK_BecomeConsultantRequestSchedules_BecomeConsultantRequests_BecomeConsultantRequestId",
                        column: x => x.BecomeConsultantRequestId,
                        principalSchema: "Consulting",
                        principalTable: "BecomeConsultantRequests",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                schema: "Order",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ConsultantId",
                schema: "Order",
                table: "OrderItems",
                column: "ConsultantId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                schema: "Order",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_BecomeConsultantRequestSchedules_BecomeConsultantRequestId",
                schema: "Consulting",
                table: "BecomeConsultantRequestSchedules",
                column: "BecomeConsultantRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Consultants_ConsultantId",
                schema: "Order",
                table: "OrderItems",
                column: "ConsultantId",
                principalSchema: "Consulting",
                principalTable: "Consultants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Orders_OrderId",
                schema: "Order",
                table: "OrderItems",
                column: "OrderId",
                principalSchema: "Order",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_UserId",
                schema: "Order",
                table: "Orders",
                column: "UserId",
                principalSchema: "User",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Consultants_ConsultantId",
                schema: "Order",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Orders_OrderId",
                schema: "Order",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_UserId",
                schema: "Order",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "BecomeConsultantRequestSchedules",
                schema: "Consulting");

            migrationBuilder.DropIndex(
                name: "IX_Orders_UserId",
                schema: "Order",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_ConsultantId",
                schema: "Order",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_OrderId",
                schema: "Order",
                table: "OrderItems");
        }
    }
}
