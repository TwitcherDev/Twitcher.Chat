using Twitcher.Chat.Models;

namespace Twitcher.Chat.Events
{
    public interface IChatMessageReceivedArgs
    {
        DateTime ReceivedTime { get; set; }
        IChatMessage Message { get; set; }
    }
}
