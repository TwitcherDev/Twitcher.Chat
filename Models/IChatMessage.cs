namespace Twitcher.Chat.Models
{
    public interface IChatMessage
    {
        public string Id { get; set; }
        public string ChannelId { get; set; }
        public string Channel { get; }
        public string UserId { get; set; }
        public string Username { get; }
        public string DisplayName { get; }
        public string Color { get; }
        public bool IsFirstMessage { get; }
        public bool IsSub { get; }
        public bool IsVip { get; }
        public bool IsMod { get; }
        public bool IsBroadcast { get; }
        public string Message { get; }
    }
}
