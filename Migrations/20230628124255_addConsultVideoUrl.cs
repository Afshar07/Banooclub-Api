using Microsoft.EntityFrameworkCore.Migrations;

namespace BanooClub.Migrations
{
    public partial class addConsultVideoUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConsultantVideoConferanceUrls",
                schema: "Consulting",
                columns: table => new
                {
                    ConsultantId = table.Column<long>(type: "bigint", nullable: false),
                    OrderId = table.Column<long>(type: "bigint", nullable: false),
                    ConferanceUrl = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsultantVideoConferanceUrls", x => new { x.OrderId, x.ConsultantId });
                    table.ForeignKey(
                        name: "FK_ConsultantVideoConferanceUrls_Consultants_ConsultantId",
                        column: x => x.ConsultantId,
                        principalSchema: "Consulting",
                        principalTable: "Consultants",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConsultantVideoConferanceUrls_Orders_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "Order",
                        principalTable: "Orders",
                        principalColumn: "OrderId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConsultantVideoConferanceUrls_ConsultantId",
                schema: "Consulting",
                table: "ConsultantVideoConferanceUrls",
                column: "ConsultantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConsultantVideoConferanceUrls",
                schema: "Consulting");
        }
    }
}
