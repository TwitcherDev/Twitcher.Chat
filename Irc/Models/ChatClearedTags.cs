namespace Twitcher.Chat.Irc.Models;

internal class ChatClearedTags : IChatClearedTags
{
    public TimeSpan? BanDuration { get; }
    public string ChannelId { get; }
    public string? TargetUserId { get; }
    public DateTime SentTime { get; }

    internal ChatClearedTags(IReadOnlyDictionary<string, string> tags)
    {
        ChannelId = tags.GetRequiredTagValue("room-id");
        TargetUserId = tags.GetTagValue("target-user-id");
        BanDuration = tags.GetTagValue("ban-duration", null, s => !string.IsNullOrEmpty(s) ? TimeSpan.FromSeconds(int.Parse(s)) : (TimeSpan?)null);
        SentTime = tags.GetRequiredTagValue("tmi-sent-ts", s => DateTime.UnixEpoch.AddMilliseconds(long.Parse(s)));
    }
}
