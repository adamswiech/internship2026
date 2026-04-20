namespace LeaderBoardApp.Server.Models
{
    public class Player
    {
        public int id { get; set; }
        public string playerId { get; set; }
        public int score { get; set; }
        public string gameMode { get; set; }
    }
}