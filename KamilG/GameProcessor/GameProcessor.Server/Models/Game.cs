namespace GameProcessor.Server.Models
{
    public class Game
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public int Score { get; set; }
        public string GameMode { get; set; } = string.Empty;
    }
}
