using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace leaderboardServer.Migrations
{
    /// <inheritdoc />
    public partial class addPlayerFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Scores_players_username",
                schema: "dbo",
                table: "Scores");

            migrationBuilder.DropForeignKey(
                name: "FK_snapshotEntries_Top10snapshots_Top10snapshotId",
                schema: "dbo",
                table: "snapshotEntries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_snapshotEntries",
                schema: "dbo",
                table: "snapshotEntries");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_players_username",
                schema: "dbo",
                table: "players");

            migrationBuilder.DropPrimaryKey(
                name: "PK_players",
                schema: "dbo",
                table: "players");

            migrationBuilder.RenameTable(
                name: "snapshotEntries",
                schema: "dbo",
                newName: "SnapshotEntries",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "players",
                schema: "dbo",
                newName: "Slayers",
                newSchema: "dbo");

            migrationBuilder.RenameIndex(
                name: "IX_snapshotEntries_Top10snapshotId_rank",
                schema: "dbo",
                table: "SnapshotEntries",
                newName: "IX_SnapshotEntries_Top10snapshotId_rank");

            migrationBuilder.RenameIndex(
                name: "IX_players_username",
                schema: "dbo",
                table: "Slayers",
                newName: "IX_Slayers_username");

            migrationBuilder.RenameIndex(
                name: "IX_players_highScore",
                schema: "dbo",
                table: "Slayers",
                newName: "IX_Slayers_highScore");

            migrationBuilder.RenameIndex(
                name: "IX_players_avgScore",
                schema: "dbo",
                table: "Slayers",
                newName: "IX_Slayers_avgScore");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SnapshotEntries",
                schema: "dbo",
                table: "SnapshotEntries",
                column: "id");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Slayers_username",
                schema: "dbo",
                table: "Slayers",
                column: "username");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Slayers",
                schema: "dbo",
                table: "Slayers",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Scores_Slayers_username",
                schema: "dbo",
                table: "Scores",
                column: "username",
                principalSchema: "dbo",
                principalTable: "Slayers",
                principalColumn: "username",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SnapshotEntries_Top10snapshots_Top10snapshotId",
                schema: "dbo",
                table: "SnapshotEntries",
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
                name: "FK_Scores_Slayers_username",
                schema: "dbo",
                table: "Scores");

            migrationBuilder.DropForeignKey(
                name: "FK_SnapshotEntries_Top10snapshots_Top10snapshotId",
                schema: "dbo",
                table: "SnapshotEntries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SnapshotEntries",
                schema: "dbo",
                table: "SnapshotEntries");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Slayers_username",
                schema: "dbo",
                table: "Slayers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Slayers",
                schema: "dbo",
                table: "Slayers");

            migrationBuilder.RenameTable(
                name: "SnapshotEntries",
                schema: "dbo",
                newName: "snapshotEntries",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "Slayers",
                schema: "dbo",
                newName: "players",
                newSchema: "dbo");

            migrationBuilder.RenameIndex(
                name: "IX_SnapshotEntries_Top10snapshotId_rank",
                schema: "dbo",
                table: "snapshotEntries",
                newName: "IX_snapshotEntries_Top10snapshotId_rank");

            migrationBuilder.RenameIndex(
                name: "IX_Slayers_username",
                schema: "dbo",
                table: "players",
                newName: "IX_players_username");

            migrationBuilder.RenameIndex(
                name: "IX_Slayers_highScore",
                schema: "dbo",
                table: "players",
                newName: "IX_players_highScore");

            migrationBuilder.RenameIndex(
                name: "IX_Slayers_avgScore",
                schema: "dbo",
                table: "players",
                newName: "IX_players_avgScore");

            migrationBuilder.AddPrimaryKey(
                name: "PK_snapshotEntries",
                schema: "dbo",
                table: "snapshotEntries",
                column: "id");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_players_username",
                schema: "dbo",
                table: "players",
                column: "username");

            migrationBuilder.AddPrimaryKey(
                name: "PK_players",
                schema: "dbo",
                table: "players",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Scores_players_username",
                schema: "dbo",
                table: "Scores",
                column: "username",
                principalSchema: "dbo",
                principalTable: "players",
                principalColumn: "username",
                onDelete: ReferentialAction.Cascade);

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
    }
}
