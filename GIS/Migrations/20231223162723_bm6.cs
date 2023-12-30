using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GIS.Migrations
{
    public partial class bm6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BodyMaterial",
                table: "BodyMaterial");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BodyMaterial",
                table: "BodyMaterial",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Face",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Path = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Face", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Node",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    X = table.Column<double>(type: "double precision", nullable: false),
                    Y = table.Column<double>(type: "double precision", nullable: false),
                    Z = table.Column<double>(type: "double precision", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Node", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FaceNode",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FaceId = table.Column<Guid>(type: "uuid", nullable: false),
                    NodeId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FaceNode", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FaceNode_Face_FaceId",
                        column: x => x.FaceId,
                        principalTable: "Face",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FaceNode_Node_NodeId",
                        column: x => x.NodeId,
                        principalTable: "Node",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BodyMaterial_BodyId",
                table: "BodyMaterial",
                column: "BodyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FaceNode_FaceId",
                table: "FaceNode",
                column: "FaceId");

            migrationBuilder.CreateIndex(
                name: "IX_FaceNode_NodeId",
                table: "FaceNode",
                column: "NodeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FaceNode");

            migrationBuilder.DropTable(
                name: "Face");

            migrationBuilder.DropTable(
                name: "Node");

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
    }
}
