namespace Twitcher.Chat.Irc.Models;

internal class ChatMessageClearedTags : IChatMessageClearedTags
{
    public string Username { get; }
    public string? ChannelId { get; }
    public Guid TargetMessageId { get; }
    public DateTime SentTime { get; }

    internal ChatMessageClearedTags(IReadOnlyDictionary<string, string> tags)
    {
        Username = tags.GetRequiredTagValue("login");
        ChannelId = tags.GetTagValue("room-id");
        TargetMessageId = tags.GetRequiredTagValue("target-msg-id", s => Guid.Parse(s));
        SentTime = tags.GetRequiredTagValue("tmi-sent-ts", s => DateTime.UnixEpoch.AddMilliseconds(long.Parse(s)));
    }
}
