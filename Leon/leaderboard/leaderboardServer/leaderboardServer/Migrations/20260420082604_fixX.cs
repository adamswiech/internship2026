using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace leaderboardServer.Migrations
{
    /// <inheritdoc />
    public partial class fixX : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_snapshotEntries_Top10snapshots_Top10snapshotsId",
                schema: "dbo",
                table: "snapshotEntries");

            migrationBuilder.DropIndex(
                name: "IX_snapshotEntries_Top10snapshotsId",
                schema: "dbo",
                table: "snapshotEntries");

            migrationBuilder.RenameColumn(
                name: "Top10snapshotsId",
                schema: "dbo",
                table: "snapshotEntries",
                newName: "rank");

            migrationBuilder.AddColumn<int>(
                name: "rank",
                schema: "dbo",
                table: "Top10",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Top10snapshotId",
                schema: "dbo",
                table: "snapshotEntries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Top10snapshots_date",
                schema: "dbo",
                table: "Top10snapshots",
                column: "date");

            migrationBuilder.CreateIndex(
                name: "IX_Top10_rank",
                schema: "dbo",
                table: "Top10",
                column: "rank",
                unique: true,
                filter: "[rank] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_snapshotEntries_Top10snapshotId_rank",
                schema: "dbo",
                table: "snapshotEntries",
                columns: new[] { "Top10snapshotId", "rank" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Scores_score",
                schema: "dbo",
                table: "Scores",
                column: "score");

            migrationBuilder.CreateIndex(
                name: "IX_Scores_time",
                schema: "dbo",
                table: "Scores",
                column: "time");

            migrationBuilder.AddForeignKey(
                name: "FK_snapshotEntries_Top10snapshots_Top10snapshotId",
                schema: "dbo",
                table: "snapshotEntries",
                column: "Top10snapshotId",
                principalSchema: "dbo",
                principalTable: "Top10snapshots",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_snapshotEntries_Top10snapshots_Top10snapshotId",
                schema: "dbo",
                table: "snapshotEntries");

            migrationBuilder.DropIndex(
                name: "IX_Top10snapshots_date",
                schema: "dbo",
                table: "Top10snapshots");

            migrationBuilder.DropIndex(
                name: "IX_Top10_rank",
                schema: "dbo",
                table: "Top10");

            migrationBuilder.DropIndex(
                name: "IX_snapshotEntries_Top10snapshotId_rank",
                schema: "dbo",
                table: "snapshotEntries");

            migrationBuilder.DropIndex(
                name: "IX_Scores_score",
                schema: "dbo",
                table: "Scores");

            migrationBuilder.DropIndex(
                name: "IX_Scores_time",
                schema: "dbo",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "rank",
                schema: "dbo",
                table: "Top10");

            migrationBuilder.DropColumn(
                name: "Top10snapshotId",
                schema: "dbo",
                table: "snapshotEntries");

            migrationBuilder.RenameColumn(
                name: "rank",
                schema: "dbo",
                table: "snapshotEntries",
                newName: "Top10snapshotsId");

            migrationBuilder.CreateIndex(
                name: "IX_snapshotEntries_Top10snapshotsId",
                schema: "dbo",
                table: "snapshotEntries",
                column: "Top10snapshotsId");

            migrationBuilder.AddForeignKey(
                name: "FK_snapshotEntries_Top10snapshots_Top10snapshotsId",
                schema: "dbo",
                table: "snapshotEntries",
                column: "Top10snapshotsId",
                principalSchema: "dbo",
                principalTable: "Top10snapshots",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
