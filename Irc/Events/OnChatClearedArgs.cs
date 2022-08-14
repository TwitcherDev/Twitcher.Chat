namespace Twitcher.Chat.Irc.Events;

internal class OnChatClearedArgs : EventArgs, IChatClearedArgs
{
    public string Channel { get; }
    public string? Username { get; }
    public IChatClearedTags? Tags { get; }

    internal OnChatClearedArgs(string channel, string? username, IReadOnlyDictionary<string, string>? tags)
    {
        Channel = channel;
        Username = username;
        if (tags != null)
            Tags = new ChatClearedTags(tags);
    }
}
