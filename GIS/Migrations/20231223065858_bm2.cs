using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GIS.Migrations
{
    public partial class bm2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BodyMaterial",
                table: "BodyMaterial");

            migrationBuilder.DropIndex(
                name: "IX_BodyMaterial_BodyId",
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

            migrationBuilder.AddPrimaryKey(
                name: "PK_BodyMaterial",
                table: "BodyMaterial",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_BodyMaterial_BodyId",
                table: "BodyMaterial",
                column: "BodyId");
        }
    }
}
