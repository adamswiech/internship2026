namespace leaderboardServer.Models
{
    public class Score
    {
        public int id { get; set; }
        public string username { get; set; }
        public int score { get; set; }
        public TimeSpan time { get; set; }
        public string gameMode { get; set; }
        public bool? isSuspicious { get; set; } = false;
    }
}
