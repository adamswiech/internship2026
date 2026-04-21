import type { Player } from "../src/interfaces/Player";

export default class Api {
  public static async uploadScore(
    playerId: string,
    score: number,
    gameMode: string,
  ): Promise<any> {
    const response = await fetch(
      "https://server-leaderboardapp.dev.localhost:7457/api/LeaderBoard/uploadScore",
      {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: `{
            "playerId" : "${playerId}",
            "score" : ${score},
            "gameMode" : "${gameMode}",
            "status" : "verifing"
        }`,
      },
    );
    const jsonResponse: any = await response.text();

    if (!response.ok) {
      throw new Error("HTTP error! status: " + response.status);
    }

    return jsonResponse;
  }
  public static async getAllScores(): Promise<Player[]> {
    const response = await fetch(
      `https://localhost:7457/api/LeaderBoard/getAllScores`,
    );
    const jsonResponse: Player[] = await response.json();

    if (!response.ok) {
      throw new Error("HTTP error! status: " + response.status);
    }

    return jsonResponse;
  }
}
