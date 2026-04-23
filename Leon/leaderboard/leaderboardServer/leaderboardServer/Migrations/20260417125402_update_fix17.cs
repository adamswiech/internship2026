using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace leaderboardServer.Migrations
{
    /// <inheritdoc />
    public partial class update_fix17 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Scores",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    score = table.Column<int>(type: "int", nullable: false),
                    time = table.Column<TimeSpan>(type: "time", nullable: false),
                    gameMode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isSuspicious = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scores", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Top10legacy",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Top10legacy", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Top10",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    scoreId = table.Column<int>(type: "int", nullable: true),
                    Top10legacyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Top10", x => x.id);
                    table.ForeignKey(
                        name: "FK_Top10_Scores_scoreId",
                        column: x => x.scoreId,
                        principalSchema: "dbo",
                        principalTable: "Scores",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Top10_Top10legacy_Top10legacyId",
                        column: x => x.Top10legacyId,
                        principalSchema: "dbo",
                        principalTable: "Top10legacy",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Top10_scoreId",
                schema: "dbo",
                table: "Top10",
                column: "scoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Top10_Top10legacyId",
                schema: "dbo",
                table: "Top10",
                column: "Top10legacyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Top10",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Scores",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Top10legacy",
                schema: "dbo");
        }
    }
}
