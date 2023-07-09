using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BanooClub.Migrations
{
    public partial class AddBecomeConsultantRequestConsultCategoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Consulting");

            migrationBuilder.RenameTable(
                name: "ConsultCategories",
                newName: "ConsultCategories",
                newSchema: "Consulting");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                schema: "Consulting",
                table: "ConsultCategories",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "BecomeConsultantRequests",
                schema: "Consulting",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    NationalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MedicalSystemNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StateId = table.Column<long>(type: "bigint", nullable: false),
                    CityId = table.Column<long>(type: "bigint", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShabaNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CartNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BecomeConsultantRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BecomeConsultantRequests_Cities_CityId",
                        column: x => x.CityId,
                        principalSchema: "dbo",
                        principalTable: "Cities",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BecomeConsultantRequests_States_StateId",
                        column: x => x.StateId,
                        principalSchema: "dbo",
                        principalTable: "States",
                        principalColumn: "StateId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Consultants",
                schema: "Consulting",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    NationalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MedicalSystemNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StateId = table.Column<long>(type: "bigint", nullable: false),
                    CityId = table.Column<long>(type: "bigint", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShabaNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CartNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consultants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Consultants_Cities_CityId",
                        column: x => x.CityId,
                        principalSchema: "dbo",
                        principalTable: "Cities",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Consultants_States_StateId",
                        column: x => x.StateId,
                        principalSchema: "dbo",
                        principalTable: "States",
                        principalColumn: "StateId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BecomeConsultantRequestConsultCategories",
                schema: "Consulting",
                columns: table => new
                {
                    RequestId = table.Column<long>(type: "bigint", nullable: false),
                    CategoryId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BecomeConsultantRequestConsultCategories", x => new { x.RequestId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_BecomeConsultantRequestConsultCategories_BecomeConsultantRequests_RequestId",
                        column: x => x.RequestId,
                        principalSchema: "Consulting",
                        principalTable: "BecomeConsultantRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BecomeConsultantRequestConsultCategories_ConsultCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "Consulting",
                        principalTable: "ConsultCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConsultantConsultCategories",
                schema: "Consulting",
                columns: table => new
                {
                    ConsultantId = table.Column<long>(type: "bigint", nullable: false),
                    ConsultCategoryId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsultantConsultCategories", x => new { x.ConsultantId, x.ConsultCategoryId });
                    table.ForeignKey(
                        name: "FK_ConsultantConsultCategories_Consultants_ConsultantId",
                        column: x => x.ConsultantId,
                        principalSchema: "Consulting",
                        principalTable: "Consultants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConsultantConsultCategories_ConsultCategories_ConsultCategoryId",
                        column: x => x.ConsultCategoryId,
                        principalSchema: "Consulting",
                        principalTable: "ConsultCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BecomeConsultantRequestConsultCategories_CategoryId",
                schema: "Consulting",
                table: "BecomeConsultantRequestConsultCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_BecomeConsultantRequests_CityId",
                schema: "Consulting",
                table: "BecomeConsultantRequests",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_BecomeConsultantRequests_StateId",
                schema: "Consulting",
                table: "BecomeConsultantRequests",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsultantConsultCategories_ConsultCategoryId",
                schema: "Consulting",
                table: "ConsultantConsultCategories",
                column: "ConsultCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Consultants_CityId",
                schema: "Consulting",
                table: "Consultants",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Consultants_StateId",
                schema: "Consulting",
                table: "Consultants",
                column: "StateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BecomeConsultantRequestConsultCategories",
                schema: "Consulting");

            migrationBuilder.DropTable(
                name: "ConsultantConsultCategories",
                schema: "Consulting");

            migrationBuilder.DropTable(
                name: "BecomeConsultantRequests",
                schema: "Consulting");

            migrationBuilder.DropTable(
                name: "Consultants",
                schema: "Consulting");

            migrationBuilder.RenameTable(
                name: "ConsultCategories",
                schema: "Consulting",
                newName: "ConsultCategories");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "ConsultCategories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);
        }
    }
}
