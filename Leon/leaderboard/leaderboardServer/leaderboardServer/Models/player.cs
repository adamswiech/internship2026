namespace leaderboardServer.Models
{
    public class player
    {
        public int id { get; set; }
        public string username { get; set; }
        public int scoreQ { get; set; }
        public double avgScore { get; set; }
        public int highScore { get; set; }

        public List<Score> Scores { get; set; } = new();

    }
}
