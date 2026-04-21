using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace leaderboardServer.Migrations
{
    /// <inheritdoc />
    public partial class addPlayer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "username",
                schema: "dbo",
                table: "Scores",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<bool>(
                name: "isSuspicious",
                schema: "dbo",
                table: "Scores",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.CreateTable(
                name: "players",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    scoreQ = table.Column<int>(type: "int", nullable: false),
                    avgScore = table.Column<bool>(type: "bit", nullable: false),
                    highScore = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_players", x => x.id);
                    table.UniqueConstraint("AK_players_username", x => x.username);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Scores_username",
                schema: "dbo",
                table: "Scores",
                column: "username");

            migrationBuilder.CreateIndex(
                name: "IX_players_avgScore",
                schema: "dbo",
                table: "players",
                column: "avgScore",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_players_highScore",
                schema: "dbo",
                table: "players",
                column: "highScore",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_players_username",
                schema: "dbo",
                table: "players",
                column: "username",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Scores_players_username",
                schema: "dbo",
                table: "Scores",
                column: "username",
                principalSchema: "dbo",
                principalTable: "players",
                principalColumn: "username",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Scores_players_username",
                schema: "dbo",
                table: "Scores");

            migrationBuilder.DropTable(
                name: "players",
                schema: "dbo");

            migrationBuilder.DropIndex(
                name: "IX_Scores_username",
                schema: "dbo",
                table: "Scores");

            migrationBuilder.AlterColumn<string>(
                name: "username",
                schema: "dbo",
                table: "Scores",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<bool>(
                name: "isSuspicious",
                schema: "dbo",
                table: "Scores",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);
        }
    }
}
