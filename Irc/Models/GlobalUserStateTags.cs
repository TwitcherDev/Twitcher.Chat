namespace Twitcher.Chat.Irc.Models;

internal class GlobalUserStateTags : IGlobalUserStateTags
{
    public string UserType { get; }
    public string UserId { get; }
    public string DisplayName { get; }
    public IReadOnlyCollection<IChatMessageBadge> Badges { get; }
    public IReadOnlyDictionary<string, string> BadgesInfo { get; }
    public string Color { get; }
    public IReadOnlyCollection<string> EmoteSets { get; }
    public bool IsTurbo { get; }

    internal GlobalUserStateTags(IReadOnlyDictionary<string, string> tags)
    {
        UserType = tags.GetTagValue("user-type", string.Empty);
        UserId = tags.GetRequiredTagValue("user-id");
        DisplayName = tags.GetTagValue("display-name", string.Empty);
        Badges = ParseHelpers.ParseBadges(tags.GetTagValue("badges", string.Empty));
        BadgesInfo = ParseHelpers.ParseBadgesInfo(tags.GetTagValue("badge-info", string.Empty));
        Color = tags.GetTagValue("color", string.Empty);
        EmoteSets = tags.GetRequiredTagValue("emote-sets").Split(',').ToList().AsReadOnly();
        IsTurbo = tags.GetTagValue("turbo", false, s => s == "1");
    }
}
