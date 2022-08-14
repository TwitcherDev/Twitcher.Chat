namespace Twitcher.Chat.Irc.Events;

internal class OnChatMessageReceivedArgs : EventArgs, IChatMessageReceivedArgs
{
    public string Channel { get; }
    public string Username { get; }
    public string Message { get; }
    public IChatMessageReceivedTags? Tags { get; }

    internal OnChatMessageReceivedArgs(string channel, string username, string message, IReadOnlyDictionary<string, string>? tags)
    {
        Channel = channel;
        Username = username;
        Message = message;
        if (tags != null)
            Tags = new ChatMessageReceivedTags(tags);
    }
}