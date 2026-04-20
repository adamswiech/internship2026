using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace leaderboardServer.Migrations
{
    /// <inheritdoc />
    public partial class fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Top10_Top10legacy_Top10legacyId",
                schema: "dbo",
                table: "Top10");

            migrationBuilder.DropTable(
                name: "Top10legacy",
                schema: "dbo");

            migrationBuilder.DropIndex(
                name: "IX_Top10_Top10legacyId",
                schema: "dbo",
                table: "Top10");

            migrationBuilder.DropColumn(
                name: "Top10legacyId",
                schema: "dbo",
                table: "Top10");

            migrationBuilder.CreateTable(
                name: "Top10snapshots",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Top10snapshots", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "snapshotEntries",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    score = table.Column<int>(type: "int", nullable: false),
                    time = table.Column<TimeSpan>(type: "time", nullable: false),
                    gameMode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isSuspicious = table.Column<bool>(type: "bit", nullable: false),
                    Top10snapshotsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_snapshotEntries", x => x.id);
                    table.ForeignKey(
                        name: "FK_snapshotEntries_Top10snapshots_Top10snapshotsId",
                        column: x => x.Top10snapshotsId,
                        principalSchema: "dbo",
                        principalTable: "Top10snapshots",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_snapshotEntries_Top10snapshotsId",
                schema: "dbo",
                table: "snapshotEntries",
                column: "Top10snapshotsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "snapshotEntries",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Top10snapshots",
                schema: "dbo");

            migrationBuilder.AddColumn<int>(
                name: "Top10legacyId",
                schema: "dbo",
                table: "Top10",
                type: "int",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Top10_Top10legacyId",
                schema: "dbo",
                table: "Top10",
                column: "Top10legacyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Top10_Top10legacy_Top10legacyId",
                schema: "dbo",
                table: "Top10",
                column: "Top10legacyId",
                principalSchema: "dbo",
                principalTable: "Top10legacy",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
