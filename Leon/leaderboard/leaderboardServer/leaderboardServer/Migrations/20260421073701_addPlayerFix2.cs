using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace leaderboardServer.Migrations
{
    /// <inheritdoc />
    public partial class addPlayerFix2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Scores_Slayers_username",
                schema: "dbo",
                table: "Scores");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Slayers_username",
                schema: "dbo",
                table: "Slayers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Slayers",
                schema: "dbo",
                table: "Slayers");

            migrationBuilder.RenameTable(
                name: "Slayers",
                schema: "dbo",
                newName: "Players",
                newSchema: "dbo");

            migrationBuilder.RenameIndex(
                name: "IX_Slayers_username",
                schema: "dbo",
                table: "Players",
                newName: "IX_Players_username");

            migrationBuilder.RenameIndex(
                name: "IX_Slayers_highScore",
                schema: "dbo",
                table: "Players",
                newName: "IX_Players_highScore");

            migrationBuilder.RenameIndex(
                name: "IX_Slayers_avgScore",
                schema: "dbo",
                table: "Players",
                newName: "IX_Players_avgScore");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Players_username",
                schema: "dbo",
                table: "Players",
                column: "username");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Players",
                schema: "dbo",
                table: "Players",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Scores_Players_username",
                schema: "dbo",
                table: "Scores",
                column: "username",
                principalSchema: "dbo",
                principalTable: "Players",
                principalColumn: "username",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Scores_Players_username",
                schema: "dbo",
                table: "Scores");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Players_username",
                schema: "dbo",
                table: "Players");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Players",
                schema: "dbo",
                table: "Players");

            migrationBuilder.RenameTable(
                name: "Players",
                schema: "dbo",
                newName: "Slayers",
                newSchema: "dbo");

            migrationBuilder.RenameIndex(
                name: "IX_Players_username",
                schema: "dbo",
                table: "Slayers",
                newName: "IX_Slayers_username");

            migrationBuilder.RenameIndex(
                name: "IX_Players_highScore",
                schema: "dbo",
                table: "Slayers",
                newName: "IX_Slayers_highScore");

            migrationBuilder.RenameIndex(
                name: "IX_Players_avgScore",
                schema: "dbo",
                table: "Slayers",
                newName: "IX_Slayers_avgScore");

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
        }
    }
}
