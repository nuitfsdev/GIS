using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GIS.Migrations
{
    public partial class bm7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BodyMaterial",
                table: "BodyMaterial");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "BodyMaterial");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BodyMaterial",
                table: "BodyMaterial",
                columns: new[] { "BodyId", "MaterialId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BodyMaterial",
                table: "BodyMaterial");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "BodyMaterial",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_BodyMaterial",
                table: "BodyMaterial",
                column: "Id");
        }
    }
}
