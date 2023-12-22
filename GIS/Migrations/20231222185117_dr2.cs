using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GIS.Migrations
{
    public partial class dr2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DamageReports_Accounts_Id",
                table: "DamageReports");

            migrationBuilder.DropForeignKey(
                name: "FK_DamageReports_Body_Id",
                table: "DamageReports");

            migrationBuilder.AddColumn<Guid>(
                name: "AccountId",
                table: "DamageReports",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BodyId",
                table: "DamageReports",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_DamageReports_AccountId",
                table: "DamageReports",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_DamageReports_BodyId",
                table: "DamageReports",
                column: "BodyId");

            migrationBuilder.AddForeignKey(
                name: "FK_DamageReports_Accounts_AccountId",
                table: "DamageReports",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DamageReports_Body_BodyId",
                table: "DamageReports",
                column: "BodyId",
                principalTable: "Body",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DamageReports_Accounts_AccountId",
                table: "DamageReports");

            migrationBuilder.DropForeignKey(
                name: "FK_DamageReports_Body_BodyId",
                table: "DamageReports");

            migrationBuilder.DropIndex(
                name: "IX_DamageReports_AccountId",
                table: "DamageReports");

            migrationBuilder.DropIndex(
                name: "IX_DamageReports_BodyId",
                table: "DamageReports");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "DamageReports");

            migrationBuilder.DropColumn(
                name: "BodyId",
                table: "DamageReports");

            migrationBuilder.AddForeignKey(
                name: "FK_DamageReports_Accounts_Id",
                table: "DamageReports",
                column: "Id",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DamageReports_Body_Id",
                table: "DamageReports",
                column: "Id",
                principalTable: "Body",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
