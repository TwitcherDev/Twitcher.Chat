namespace Twitcher.Chat.Irc.Models;

internal class ChatMessageReceivedTags : IChatMessageReceivedTags
{
    public Guid Id { get; }
    public bool IsReply => ReplyParent != null;
    public IChatMessageReplyParent? ReplyParent { get; }
    public string ChannelId { get; }
    public string UserType { get; }
    public string UserId { get; }
    public string DisplayName { get; }
    public int? Bits { get; }
    public IReadOnlyCollection<IChatMessageBadge> Badges { get; }
    public IReadOnlyDictionary<string, string> BadgesInfo { get; }
    public int SubscribeMonths => int.Parse(BadgesInfo.FirstOrDefault(b => b.Key == "subscriber" || b.Key == "founder").Value ?? "0");
    public string? Predictions => BadgesInfo.GetValueOrDefault("predictions");
    public bool IsHighlighted { get; }
    public string Color { get; }
    public IReadOnlyDictionary<string, IReadOnlyCollection<IChatMessageEmotePosition>> Emotes { get; }
    public bool IsTurbo { get; }
    public bool IsFirstMessage { get; }
    public bool IsBroadcast => UserId == ChannelId;
    public bool IsMod { get; }
    public bool IsVip => Badges.Any(b => b.Badge == "vip");
    public bool IsSubscriber { get; }
    public DateTime SentTime { get; }

    internal ChatMessageReceivedTags(IReadOnlyDictionary<string, string> tags)
    {
        Id = tags.GetRequiredTagValue("id", s => Guid.Parse(s));
        if (tags.ContainsKey("reply-parent-msg-id"))
            ReplyParent = new ChatMessageReplyParent(tags);
        ChannelId = tags.GetRequiredTagValue("room-id");
        UserType = tags.GetTagValue("user-type", string.Empty);
        UserId = tags.GetRequiredTagValue("user-id");
        DisplayName = tags.GetTagValue("display-name", string.Empty);
        Bits = tags.GetTagValue("bits", null, s => !string.IsNullOrEmpty(s) ? int.Parse(s) : (int?)null);
        Badges = ParseHelpers.ParseBadges(tags.GetTagValue("badges", string.Empty));
        BadgesInfo = ParseHelpers.ParseBadgesInfo(tags.GetTagValue("badge-info", string.Empty));
        IsHighlighted = tags.GetTagValue("msg-id") == "highlighted-message";
        Color = tags.GetTagValue("color", string.Empty);
        Emotes = ParseHelpers.ParseEmotes(tags.GetTagValue("emotes", string.Empty));
        IsFirstMessage = tags.GetTagValue("first-msg", false, s => s == "1");
        IsMod = tags.GetTagValue("mod", false, s => s == "1");
        IsSubscriber = tags.GetTagValue("subscriber", false, s => s == "1");
        IsTurbo = tags.GetTagValue("turbo", false, s => s == "1");
        SentTime = tags.GetRequiredTagValue("tmi-sent-ts", s => DateTime.UnixEpoch.AddMilliseconds(long.Parse(s)));
    }
}
