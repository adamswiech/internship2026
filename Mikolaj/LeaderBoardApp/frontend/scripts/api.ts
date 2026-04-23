import type { Player } from "../src/interfaces/Player";

export default class Api {
  public static async uploadScore(
    playerId: string,
    score: number,
    gameMode: string,
  ): Promise<string> {
    const response = await fetch(
      "https://server-leaderboardapp.dev.localhost:7457/api/LeaderBoard/uploadScore",
      {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          playerId,
          score,
          gameMode,
          status: "verifying",
        }),
      },
    );

    if (!response.ok) {
      const err = await response.json();
      throw new Error(err.message);
    }

    return await response.text();
  }

  public static async getAllScores(): Promise<Player[]> {
    const response = await fetch(
      "https://server-leaderboardapp.dev.localhost:7457/api/LeaderBoard/getAllScores",
    );

    if (!response.ok) {
      throw new Error("HTTP error! status: " + response.status);
    }

    return await response.json();
  }

  public static async leaderboard(gameMode: string): Promise<Player[]> {
    const response = await fetch(
      `https://localhost:7457/api/LeaderBoard/leaderboard?gameMode=${gameMode}`,
    );
    const jsonResponse: Player[] = await response.json();

    if (!response.ok) {
      throw new Error("HTTP error! status: " + response.status);
    }

    return jsonResponse;
  }
}
