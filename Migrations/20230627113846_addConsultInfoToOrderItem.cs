using Microsoft.EntityFrameworkCore.Migrations;

namespace BanooClub.Migrations
{
    public partial class addConsultInfoToOrderItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ConsultType",
                schema: "Order",
                table: "OrderItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ConsultantId",
                schema: "Order",
                table: "OrderItems",
                type: "bigint",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConsultType",
                schema: "Order",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "ConsultantId",
                schema: "Order",
                table: "OrderItems");
        }
    }
}
