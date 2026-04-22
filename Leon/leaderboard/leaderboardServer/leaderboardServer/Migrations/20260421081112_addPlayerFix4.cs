using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace leaderboardServer.Migrations
{
    /// <inheritdoc />
    public partial class addPlayerFix4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Players_avgScore",
                schema: "dbo",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_highScore",
                schema: "dbo",
                table: "Players");

            migrationBuilder.CreateIndex(
                name: "IX_Players_avgScore",
                schema: "dbo",
                table: "Players",
                column: "avgScore");

            migrationBuilder.CreateIndex(
                name: "IX_Players_highScore",
                schema: "dbo",
                table: "Players",
                column: "highScore");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Players_avgScore",
                schema: "dbo",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_highScore",
                schema: "dbo",
                table: "Players");

            migrationBuilder.CreateIndex(
                name: "IX_Players_avgScore",
                schema: "dbo",
                table: "Players",
                column: "avgScore",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Players_highScore",
                schema: "dbo",
                table: "Players",
                column: "highScore",
                unique: true);
        }
    }
}
