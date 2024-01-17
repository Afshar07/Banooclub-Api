using Microsoft.EntityFrameworkCore.Migrations;

namespace BanooClub.Migrations
{
    public partial class addTableToConsultationRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ShabaNo",
                schema: "Consulting",
                table: "BecomeConsultantRequests",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                schema: "Consulting",
                table: "BecomeConsultantRequests",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NationalCode",
                schema: "Consulting",
                table: "BecomeConsultantRequests",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MedicalSystemNumber",
                schema: "Consulting",
                table: "BecomeConsultantRequests",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CartNo",
                schema: "Consulting",
                table: "BecomeConsultantRequests",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BankName",
                schema: "Consulting",
                table: "BecomeConsultantRequests",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ConsultingDurationMinut",
                schema: "Consulting",
                table: "BecomeConsultantRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "Consulting",
                table: "BecomeConsultantRequests",
                type: "nvarchar(4000)",
                maxLength: 4000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "BecomeConsultantRequestConsultPrices",
                schema: "Consulting",
                columns: table => new
                {
                    BecomeConsultantRequestId = table.Column<long>(type: "bigint", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BecomeConsultantRequestConsultPrices", x => new { x.BecomeConsultantRequestId, x.Type });
                    table.ForeignKey(
                        name: "FK_BecomeConsultantRequestConsultPrices_BecomeConsultantRequests_BecomeConsultantRequestId",
                        column: x => x.BecomeConsultantRequestId,
                        principalSchema: "Consulting",
                        principalTable: "BecomeConsultantRequests",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BecomeConsultantRequests_UserId",
                schema: "Consulting",
                table: "BecomeConsultantRequests",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BecomeConsultantRequests_Users_UserId",
                schema: "Consulting",
                table: "BecomeConsultantRequests",
                column: "UserId",
                principalSchema: "User",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BecomeConsultantRequests_Users_UserId",
                schema: "Consulting",
                table: "BecomeConsultantRequests");

            migrationBuilder.DropTable(
                name: "BecomeConsultantRequestConsultPrices",
                schema: "Consulting");

            migrationBuilder.DropIndex(
                name: "IX_BecomeConsultantRequests_UserId",
                schema: "Consulting",
                table: "BecomeConsultantRequests");

            migrationBuilder.DropColumn(
                name: "ConsultingDurationMinut",
                schema: "Consulting",
                table: "BecomeConsultantRequests");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "Consulting",
                table: "BecomeConsultantRequests");

            migrationBuilder.AlterColumn<string>(
                name: "ShabaNo",
                schema: "Consulting",
                table: "BecomeConsultantRequests",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                schema: "Consulting",
                table: "BecomeConsultantRequests",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "NationalCode",
                schema: "Consulting",
                table: "BecomeConsultantRequests",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "MedicalSystemNumber",
                schema: "Consulting",
                table: "BecomeConsultantRequests",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "CartNo",
                schema: "Consulting",
                table: "BecomeConsultantRequests",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "BankName",
                schema: "Consulting",
                table: "BecomeConsultantRequests",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);
        }
    }
}
