namespace Twitcher.Chat.Models
{
    public class ChatMessage : IChatMessage
    {
        public string Id { get; set; }
        public string ChannelId { get; set; }
        public string Channel { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public string Color { get; set; }
        public bool IsFirstMessage { get; set; }
        public bool IsSub { get; set; }
        public bool IsVip { get; set; }
        public bool IsMod { get; set; }
        public bool IsBroadcast => ChannelId == UserId;
        public string Message { get; set; }

        public ChatMessage(string id, string channelId, string channel, string userId, string username, string displayName, string color, bool isFirstMessage, bool isSub, bool isVip, bool isMod, string message)
        {
            Id = id;
            ChannelId = channelId;
            Channel = channel;
            UserId = userId;
            Username = username;
            DisplayName = displayName;
            Color = color;
            IsFirstMessage = isFirstMessage;
            IsSub = isSub;
            IsVip = isVip;
            IsMod = isMod;
            Message = message;
        }
    }
}
