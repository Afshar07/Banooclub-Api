using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BanooClub.Migrations
{
    public partial class addRatingTableForConsulting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConsultantUserScheduleRatings",
                schema: "Consulting",
                columns: table => new
                {
                    ConsultantUserScheduleRatingId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rate = table.Column<byte>(type: "tinyint", nullable: false),
                    ConsultantUserScheduleId = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsConfirm = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsultantUserScheduleRatings", x => x.ConsultantUserScheduleRatingId);
                    table.ForeignKey(
                        name: "FK_ConsultantUserScheduleRatings_ConsultantUserSchedules_ConsultantUserScheduleId",
                        column: x => x.ConsultantUserScheduleId,
                        principalSchema: "Consulting",
                        principalTable: "ConsultantUserSchedules",
                        principalColumn: "ConsultantUserScheduleId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activities_UserId",
                schema: "Social",
                table: "Activities",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsultantUserScheduleRatings_ConsultantUserScheduleId",
                schema: "Consulting",
                table: "ConsultantUserScheduleRatings",
                column: "ConsultantUserScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Users_UserId",
                schema: "Social",
                table: "Activities",
                column: "UserId",
                principalSchema: "User",
                principalTable: "Users",
                principalColumn: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Users_UserId",
                schema: "Social",
                table: "Activities");

            migrationBuilder.DropTable(
                name: "ConsultantUserScheduleRatings",
                schema: "Consulting");

            migrationBuilder.DropIndex(
                name: "IX_Activities_UserId",
                schema: "Social",
                table: "Activities");
        }
    }
}
