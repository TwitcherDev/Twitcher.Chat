namespace Twitcher.Chat.Irc.Events;

internal class OnUserNoticeReceivedArgs<TTag> : EventArgs, IUserNoticeReceivedArgs<TTag>
{
    public string Channel { get; }
    public string? Message { get; }
    public TTag? Tags { get; }

    internal OnUserNoticeReceivedArgs(string channel, string? message, TTag? tags)
    {
        Channel = channel;
        Message = message;
        Tags = tags;
    }
}
