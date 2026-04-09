using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspireApp1.Server.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Podmioty",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    kodWaluty = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    p_1 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    p_2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    p_6Od = table.Column<DateTime>(type: "datetime2", nullable: true),
                    p_6Do = table.Column<DateTime>(type: "datetime2", nullable: true),
                    p_13_1 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    p_14_1 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    p_14W = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    p_15 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    podmiot1Id = table.Column<int>(type: "int", nullable: true),
                    podmiot2Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Podmioty", x => x.id);
                    table.ForeignKey(
                        name: "FK_Podmioty_Podmioty_podmiot1Id",
                        column: x => x.podmiot1Id,
                        principalTable: "Podmioty",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Podmioty_Podmioty_podmiot2Id",
                        column: x => x.podmiot2Id,
                        principalTable: "Podmioty",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "faktura",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    podmiot1Id = table.Column<int>(type: "int", nullable: false),
                    podmiot2Id = table.Column<int>(type: "int", nullable: false),
                    kodWaluty = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    p_1 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    p_2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    p_6Od = table.Column<DateTime>(type: "datetime2", nullable: true),
                    p_6Do = table.Column<DateTime>(type: "datetime2", nullable: true),
                    p_13_1 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    p_14_1 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    p_14W = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    p_15 = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_faktura", x => x.id);
                    table.ForeignKey(
                        name: "FK_faktura_Podmioty_podmiot1Id",
                        column: x => x.podmiot1Id,
                        principalTable: "Podmioty",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_faktura_Podmioty_podmiot2Id",
                        column: x => x.podmiot2Id,
                        principalTable: "Podmioty",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FakturaWiersze",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fakturaId = table.Column<int>(type: "int", nullable: false),
                    nrWiersza = table.Column<int>(type: "int", nullable: false),
                    p_7 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    p_8A = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    p_8B = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    p_9A = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    p_11 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    p_12 = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    kursWaluty = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    podmiotid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FakturaWiersze", x => x.id);
                    table.ForeignKey(
                        name: "FK_FakturaWiersze_Podmioty_podmiotid",
                        column: x => x.podmiotid,
                        principalTable: "Podmioty",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_FakturaWiersze_faktura_fakturaId",
                        column: x => x.fakturaId,
                        principalTable: "faktura",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_faktura_podmiot1Id",
                table: "faktura",
                column: "podmiot1Id");

            migrationBuilder.CreateIndex(
                name: "IX_faktura_podmiot2Id",
                table: "faktura",
                column: "podmiot2Id");

            migrationBuilder.CreateIndex(
                name: "IX_FakturaWiersze_fakturaId",
                table: "FakturaWiersze",
                column: "fakturaId");

            migrationBuilder.CreateIndex(
                name: "IX_FakturaWiersze_podmiotid",
                table: "FakturaWiersze",
                column: "podmiotid");

            migrationBuilder.CreateIndex(
                name: "IX_Podmioty_podmiot1Id",
                table: "Podmioty",
                column: "podmiot1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Podmioty_podmiot2Id",
                table: "Podmioty",
                column: "podmiot2Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FakturaWiersze");

            migrationBuilder.DropTable(
                name: "faktura");

            migrationBuilder.DropTable(
                name: "Podmioty");
        }
    }
}
