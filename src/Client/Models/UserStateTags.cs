namespace Twitcher.Chat.Client.Models;

internal class UserStateTags : IUserStateTags
{
    public Guid? Id { get; }
    public string UserType { get; }
    public string DisplayName { get; }
    public IReadOnlyCollection<IChatMessageBadge> Badges { get; }
    public IReadOnlyDictionary<string, string> BadgesInfo { get; }
    public int SubscribeMonths => int.Parse(BadgesInfo.FirstOrDefault(b => b.Key == "subscriber" || b.Key == "founder").Value ?? "0");
    public string? Predictions => BadgesInfo.GetValueOrDefault("predictions");
    public string Color { get; }
    public IReadOnlyCollection<string> EmoteSets { get; }
    public bool IsMod { get; }
    public bool IsVip => Badges.Any(b => b.Badge == "vip");
    public bool IsSubscriber { get; }
    public bool IsTurbo { get; }

    internal UserStateTags(IReadOnlyDictionary<string, string> tags)
    {
        Id = tags.GetTagValue("id", null, s => !string.IsNullOrEmpty(s) ? Guid.Parse(s) : (Guid?)null);
        UserType = tags.GetTagValue("user-type", string.Empty);
        DisplayName = tags.GetTagValue("display-name", string.Empty);
        Badges = ParseHelpers.ParseBadges(tags.GetTagValue("badges") ?? string.Empty);
        BadgesInfo = ParseHelpers.ParseBadgesInfo(tags.GetTagValue("badge-info", string.Empty));
        Color = tags.GetTagValue("color", string.Empty);
        EmoteSets = tags.GetRequiredTagValue("emote-sets").Split(',').ToList().AsReadOnly();
        IsMod = tags.GetTagValue("mod", false, s => s == "1");
        IsSubscriber = tags.GetTagValue("subscriber", false, s => s == "1");
        IsTurbo = tags.GetTagValue("turbo", false, s => s == "1");
    }
}
