namespace Twitcher.Chat.Client.Models;

internal class UserNoticeReceivedTags : IUserNoticeReceivedTags
{
    public Guid Id { get; }
    public string ChannelId { get; }
    public string MessageType { get; }
    public string SystemMessage { get; }
    public string UserType { get; }
    public string UserId { get; }
    public string DisplayName { get; }
    public IReadOnlyCollection<IChatMessageBadge> Badges { get; }
    public IReadOnlyDictionary<string, string> BadgesInfo { get; }
    public int SubscribeMonths => int.Parse(BadgesInfo.FirstOrDefault(b => b.Key == "subscriber" || b.Key == "founder").Value ?? "0");
    public string? Predictions => BadgesInfo.GetValueOrDefault("predictions");
    public string Color { get; }
    public IReadOnlyDictionary<string, IReadOnlyCollection<IChatMessageEmotePosition>> Emotes { get; }
    public bool IsBroadcast => UserId == ChannelId;
    public bool IsMod { get; }
    public bool IsVip => Badges.Any(b => b.Badge == "vip");
    public bool IsSubscriber { get; }
    public bool IsTurbo { get; }
    public DateTime SentTime { get; }

    internal UserNoticeReceivedTags(IReadOnlyDictionary<string,string> tags)
    {
        Id = tags.GetRequiredTagValue("id", s => Guid.Parse(s));
        ChannelId = tags.GetRequiredTagValue("room-id");
        MessageType = tags.GetRequiredTagValue("msg-id");
        SystemMessage = tags.GetRequiredTagValue("system-msg");
        UserType = tags.GetTagValue("user-type", string.Empty);
        UserId = tags.GetRequiredTagValue("user-id");
        DisplayName = tags.GetTagValue("display-name", string.Empty);
        Badges = ParseHelpers.ParseBadges(tags.GetTagValue("badges", string.Empty));
        BadgesInfo = ParseHelpers.ParseBadgesInfo(tags.GetTagValue("badge-info", string.Empty));
        Color = tags.GetTagValue("color", string.Empty);
        Emotes = ParseHelpers.ParseEmotes(tags.GetTagValue("emotes", string.Empty));
        IsMod = tags.GetTagValue("mod", false, s => s == "1");
        IsSubscriber = tags.GetTagValue("subscriber", false, s => s == "1");
        IsTurbo = tags.GetTagValue("turbo", false, s => s == "1");
        SentTime = tags.GetRequiredTagValue("tmi-sent-ts", s => DateTime.UnixEpoch.AddMilliseconds(long.Parse(s)));
    }
}
