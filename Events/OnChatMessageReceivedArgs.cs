using Twitcher.Chat.Events;
using Twitcher.Chat.Models;

namespace Twitcher.Chat
{
    public class OnChatMessageReceivedArgs : EventArgs, IChatMessageReceivedArgs
    {
        public DateTime ReceivedTime { get; set; }
        public IChatMessage Message { get; set; }

        public OnChatMessageReceivedArgs(DateTime time, IChatMessage message)
        {
            ReceivedTime = time;
            Message = message;
        }
    }
}