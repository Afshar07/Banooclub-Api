using Microsoft.EntityFrameworkCore.Migrations;

namespace BanooClub.Migrations
{
    public partial class addOrderIdToUSerSchedule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "OrderId",
                schema: "Consulting",
                table: "ConsultantUserSchedules",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_ConsultantUserSchedules_OrderId",
                schema: "Consulting",
                table: "ConsultantUserSchedules",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConsultantUserSchedules_Orders_OrderId",
                schema: "Consulting",
                table: "ConsultantUserSchedules",
                column: "OrderId",
                principalSchema: "Order",
                principalTable: "Orders",
                principalColumn: "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConsultantUserSchedules_Orders_OrderId",
                schema: "Consulting",
                table: "ConsultantUserSchedules");

            migrationBuilder.DropIndex(
                name: "IX_ConsultantUserSchedules_OrderId",
                schema: "Consulting",
                table: "ConsultantUserSchedules");

            migrationBuilder.DropColumn(
                name: "OrderId",
                schema: "Consulting",
                table: "ConsultantUserSchedules");
        }
    }
}
