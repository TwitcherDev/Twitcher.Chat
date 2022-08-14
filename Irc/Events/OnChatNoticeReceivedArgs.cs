namespace Twitcher.Chat.Irc.Events;

internal class OnChatNoticeReceivedArgs : EventArgs, IChatNoticeReceivedArgs
{
    public string Channel { get; }
    public string Message { get; }
    public IChatNoticeReceivedTags? Tags { get; }

    internal OnChatNoticeReceivedArgs(string channel, string message, IReadOnlyDictionary<string, string>? tags)
    {
        Channel = channel;
        Message = message;
        if (tags != null)
            Tags = new ChatNoticeReceivedTags(tags);
    }
}
