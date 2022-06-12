namespace Twitcher.Chat
{
    public class TwitcherChatChannel
    {
        public string Channel { get; set; }
        public bool IsConnected { get; set; }

        public TwitcherChatChannel(string channel)
        {
            Channel = channel;
            IsConnected = false;
        }
    }
}
