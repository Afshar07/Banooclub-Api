using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BanooClub.Migrations
{
    public partial class addtargetDateToUserSchedualTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "TargetDate",
                schema: "Consulting",
                table: "ConsultantUserSchedules",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TargetDate",
                schema: "Consulting",
                table: "ConsultantUserSchedules");
        }
    }
}
