using Microsoft.EntityFrameworkCore.Migrations;

namespace BanooClub.Migrations
{
    public partial class addDayOfWeekToSchadualTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DayOfWeek",
                schema: "Consulting",
                table: "ConsultantSchedule",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DayOfWeek",
                schema: "Consulting",
                table: "BecomeConsultantRequestSchedules",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DayOfWeek",
                schema: "Consulting",
                table: "ConsultantSchedule");

            migrationBuilder.DropColumn(
                name: "DayOfWeek",
                schema: "Consulting",
                table: "BecomeConsultantRequestSchedules");
        }
    }
}
