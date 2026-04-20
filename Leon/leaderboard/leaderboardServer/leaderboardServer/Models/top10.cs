using leaderboardServer.Models;
using System.Text.Json.Serialization;

namespace leaderboardServer.Models
{
    public class Top10
    {
        public int id { get; set; }
        public int? rank { get; set; }
        public int? scoreId { get; set; }
        public Score? Score { get; set; }
    }
    public class Top10snapshot
    {
        public int id { get; set; }
        public DateTime date { get; set; }
        public List<snapshotEntry> Entries { get; set; } = new();
    }
    public class snapshotEntry
    {
        public int id { get; set; }

        public int rank { get; set; }
        public string username { get; set; }
        public int score { get; set; }
        public TimeSpan time { get; set; }
        public string gameMode { get; set; }
        public bool isSuspicious { get; set; } = false;

        public int Top10snapshotId { get; set; }

        [JsonIgnore]
        public Top10snapshot Top10snapshot { get; set; }
    }
}
