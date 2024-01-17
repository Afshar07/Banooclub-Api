using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BanooClub.Migrations
{
    public partial class addPriceToConsultantTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConsultantConsultCategories_Consultants_ConsultantId",
                schema: "Consulting",
                table: "ConsultantConsultCategories");

            migrationBuilder.AddColumn<int>(
                name: "ConsultingDurationMinut",
                schema: "Consulting",
                table: "Consultants",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                schema: "Consulting",
                table: "Consultants",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "Consulting",
                table: "Consultants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Consulting",
                table: "ConsultantConsultCategories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "ConsultantPrice",
                schema: "Consulting",
                columns: table => new
                {
                    ConsultantId = table.Column<long>(type: "bigint", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsultantPrice", x => new { x.ConsultantId, x.Type });
                    table.ForeignKey(
                        name: "FK_ConsultantPrice_Consultants_ConsultantId",
                        column: x => x.ConsultantId,
                        principalSchema: "Consulting",
                        principalTable: "Consultants",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Consultants_UserId",
                schema: "Consulting",
                table: "Consultants",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConsultantConsultCategories_Consultants_ConsultantId",
                schema: "Consulting",
                table: "ConsultantConsultCategories",
                column: "ConsultantId",
                principalSchema: "Consulting",
                principalTable: "Consultants",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Consultants_Users_UserId",
                schema: "Consulting",
                table: "Consultants",
                column: "UserId",
                principalSchema: "User",
                principalTable: "Users",
                principalColumn: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConsultantConsultCategories_Consultants_ConsultantId",
                schema: "Consulting",
                table: "ConsultantConsultCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_Consultants_Users_UserId",
                schema: "Consulting",
                table: "Consultants");

            migrationBuilder.DropTable(
                name: "ConsultantPrice",
                schema: "Consulting");

            migrationBuilder.DropIndex(
                name: "IX_Consultants_UserId",
                schema: "Consulting",
                table: "Consultants");

            migrationBuilder.DropColumn(
                name: "ConsultingDurationMinut",
                schema: "Consulting",
                table: "Consultants");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                schema: "Consulting",
                table: "Consultants");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "Consulting",
                table: "Consultants");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Consulting",
                table: "ConsultantConsultCategories");

            migrationBuilder.AddForeignKey(
                name: "FK_ConsultantConsultCategories_Consultants_ConsultantId",
                schema: "Consulting",
                table: "ConsultantConsultCategories",
                column: "ConsultantId",
                principalSchema: "Consulting",
                principalTable: "Consultants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
