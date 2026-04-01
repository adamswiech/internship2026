using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ksefe.Migrations
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
                    nip = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    kod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    adres = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Podmioty", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Faktury",
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
                    table.PrimaryKey("PK_Faktury", x => x.id);
                    table.ForeignKey(
                        name: "FK_Faktury_Podmioty_podmiot1Id",
                        column: x => x.podmiot1Id,
                        principalTable: "Podmioty",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Faktury_Podmioty_podmiot2Id",
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
                    kursWaluty = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FakturaWiersze", x => x.id);
                    table.ForeignKey(
                        name: "FK_FakturaWiersze_Faktury_fakturaId",
                        column: x => x.fakturaId,
                        principalTable: "Faktury",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FakturaWiersze_fakturaId",
                table: "FakturaWiersze",
                column: "fakturaId");

            migrationBuilder.CreateIndex(
                name: "IX_Faktury_podmiot1Id",
                table: "Faktury",
                column: "podmiot1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Faktury_podmiot2Id",
                table: "Faktury",
                column: "podmiot2Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FakturaWiersze");

            migrationBuilder.DropTable(
                name: "Faktury");

            migrationBuilder.DropTable(
                name: "Podmioty");
        }
    }
}
