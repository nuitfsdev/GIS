using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GIS.Migrations
{
    public partial class brs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BodyRepairStatus",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FinishDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RepairReason = table.Column<string>(type: "text", nullable: false),
                    AccountId = table.Column<Guid>(type: "uuid", nullable: false),
                    DamageReportId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BodyRepairStatus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BodyRepairStatus_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BodyRepairStatus_DamageReports_DamageReportId",
                        column: x => x.DamageReportId,
                        principalTable: "DamageReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BodyRepairStatus_AccountId",
                table: "BodyRepairStatus",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_BodyRepairStatus_DamageReportId",
                table: "BodyRepairStatus",
                column: "DamageReportId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BodyRepairStatus");
        }
    }
}
