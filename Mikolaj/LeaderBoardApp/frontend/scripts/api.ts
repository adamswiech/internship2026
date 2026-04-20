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
        headers: { 'Content-Type': 'application/json' },
        body: `{
            "playerId" : "${playerId}",
            "score" : ${score},
            "gameMode" : "${gameMode}"
        }`,
      },
    );
    const jsonResponse: any = await response.json();

    if (!response.ok) {
      throw new Error("HTTP error! status: " + response.status);
    }

    return jsonResponse;
  }
}
