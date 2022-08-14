namespace Twitcher.Chat.Irc.Models;

internal class ChatMessageBadge : IChatMessageBadge
{
    public string Badge { get; }
    public string Version { get; }

    internal ChatMessageBadge(string badge, string version)
    {
        Badge = badge;
        Version = version;
    }
}
