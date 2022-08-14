namespace Twitcher.Chat.Irc.Models;

internal class ChatNoticeReceivedTags : IChatNoticeReceivedTags
{
    public string MessageId { get; }
    public string? TargetUserId { get; }

    internal ChatNoticeReceivedTags(IReadOnlyDictionary<string, string> tags)
    {
        MessageId = tags.GetRequiredTagValue("msg-id");
        TargetUserId = tags.GetTagValue("target-user-id");
    }
}
