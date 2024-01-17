using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BanooClub.Migrations
{
    public partial class addSchelualTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConsultantSchedule",
                schema: "Consulting",
                columns: table => new
                {
                    ConsultantScheduleId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConsultantId = table.Column<long>(type: "bigint", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EntTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsultantSchedule", x => x.ConsultantScheduleId);
                    table.ForeignKey(
                        name: "FK_ConsultantSchedule_Consultants_ConsultantId",
                        column: x => x.ConsultantId,
                        principalSchema: "Consulting",
                        principalTable: "Consultants",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConsultantSchedule_ConsultantId",
                schema: "Consulting",
                table: "ConsultantSchedule",
                column: "ConsultantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConsultantSchedule",
                schema: "Consulting");
        }
    }
}
