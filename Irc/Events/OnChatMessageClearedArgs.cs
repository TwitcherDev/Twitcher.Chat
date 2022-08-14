namespace Twitcher.Chat.Irc.Events;

internal class OnChatMessageClearedArgs : EventArgs, IChatMessageClearedArgs
{
    public string Channel { get; }
    public string? Message { get; }
    public IChatMessageClearedTags? Tags { get; }

    internal OnChatMessageClearedArgs(string channel, string? message, IReadOnlyDictionary<string, string>? tags)
    {
        Channel = channel;
        Message = message;
        if (tags != null)
            Tags = new ChatMessageClearedTags(tags);
    }
}
