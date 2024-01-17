using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BanooClub.Migrations
{
    public partial class createConsultantTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConsultantUserSchedules",
                schema: "Consulting",
                columns: table => new
                {
                    ConsultantUserScheduleId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    ConsultantId = table.Column<long>(type: "bigint", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReserveTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsultantUserSchedules", x => x.ConsultantUserScheduleId);
                    table.ForeignKey(
                        name: "FK_ConsultantUserSchedules_Consultants_ConsultantId",
                        column: x => x.ConsultantId,
                        principalSchema: "Consulting",
                        principalTable: "Consultants",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConsultantUserSchedules_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "User",
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConsultantUserSchedules_ConsultantId",
                schema: "Consulting",
                table: "ConsultantUserSchedules",
                column: "ConsultantId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsultantUserSchedules_UserId",
                schema: "Consulting",
                table: "ConsultantUserSchedules",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConsultantUserSchedules",
                schema: "Consulting");
        }
    }
}
