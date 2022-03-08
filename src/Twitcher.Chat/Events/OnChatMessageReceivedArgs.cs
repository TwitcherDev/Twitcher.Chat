namespace Twitcher.Client
{
    public class OnChatMessageReceivedArgs : EventArgs
    {
        public string Channel { get; set; }
        public string UserName { get; set; }
        public string Message { get; set; }

        public OnChatMessageReceivedArgs(string channel, string userName, string message)
        {
            Channel = channel;
            UserName = userName;
            Message = message;
        }
    }
}