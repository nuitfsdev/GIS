using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GIS.Migrations
{
    public partial class brs1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BodyId",
                table: "BodyRepairStatus",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_BodyRepairStatus_BodyId",
                table: "BodyRepairStatus",
                column: "BodyId");

            migrationBuilder.AddForeignKey(
                name: "FK_BodyRepairStatus_Body_BodyId",
                table: "BodyRepairStatus",
                column: "BodyId",
                principalTable: "Body",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BodyRepairStatus_Body_BodyId",
                table: "BodyRepairStatus");

            migrationBuilder.DropIndex(
                name: "IX_BodyRepairStatus_BodyId",
                table: "BodyRepairStatus");

            migrationBuilder.DropColumn(
                name: "BodyId",
                table: "BodyRepairStatus");
        }
    }
}
