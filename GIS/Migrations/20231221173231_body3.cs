using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GIS.Migrations
{
    public partial class body3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BodyComp");

            migrationBuilder.DropTable(
                name: "Prism");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Body",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "Height",
                table: "Body",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "width",
                table: "Body",
                type: "double precision",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Body");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "Body");

            migrationBuilder.DropColumn(
                name: "width",
                table: "Body");

            migrationBuilder.CreateTable(
                name: "BodyComp",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    width = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BodyComp", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BodyComp_Body_Id",
                        column: x => x.Id,
                        principalTable: "Body",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prism",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Height = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prism", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prism_Body_Id",
                        column: x => x.Id,
                        principalTable: "Body",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}
