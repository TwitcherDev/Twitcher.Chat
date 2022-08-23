namespace Twitcher.Chat.Client.Events;

internal class OnWhisperReceivedArgs : EventArgs, IWhisperReceivedArgs
{
    public string FromUser { get; }
    public string ToUser { get; }
    public string Message { get; }
    public IWhisperReceivedTags? Tags { get; }

    internal OnWhisperReceivedArgs(string fromUser, string toUser, string message, IReadOnlyDictionary<string,string>? tags)
    {
        FromUser = fromUser;
        ToUser = toUser;
        Message = message;
        if (tags != null)
            Tags = new WhisperReceivedTags(tags);
    }
}
