namespace Twitcher.Chat.Client.Models;

internal class UserNoticeRaidReceivedTags : UserNoticeReceivedTags, IUserNoticeRaidReceivedTags
{
    public string BroadcasterRaidingDisplayName { get; }
    public string BroadcasterRaidingUsername { get; }
    public int ViewersCount { get; }

    internal UserNoticeRaidReceivedTags(IReadOnlyDictionary<string, string> tags) : base(tags)
    {
        BroadcasterRaidingDisplayName = tags.GetRequiredTagValue("msg-param-displayName");
        BroadcasterRaidingUsername = tags.GetRequiredTagValue("msg-param-login");
        ViewersCount = tags.GetRequiredTagValue("msg-param-viewerCount", s => int.Parse(s));
    }
}
