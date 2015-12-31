using BotBits.Permissions;
using SQLite;

namespace BotBitsExt.SQLite
{
    [Table("Permissions")]
    public class Permissions
    {
        public string Username { get; set; }
        public Group Group { get; set; }
        public string BanReason { get; set; }
        public long BanTimeout { get; set; }
    }
}