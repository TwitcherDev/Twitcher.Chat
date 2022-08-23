namespace Twitcher.Chat.Client.Models;

internal class WhisperReceivedTags : IWhisperReceivedTags
{
    public int Id { get; }
    public string ThreadId { get; }
    public string UserType { get; }
    public string UserId { get; }
    public string DisplayName { get; }
    public IReadOnlyCollection<IChatMessageBadge> Badges { get; }
    public IReadOnlyDictionary<string, string> BadgesInfo { get; }
    public string Color { get; }
    public IReadOnlyDictionary<string, IReadOnlyCollection<IChatMessageEmotePosition>> Emotes { get; }
    public bool IsTurbo { get; }

    internal WhisperReceivedTags(IReadOnlyDictionary<string, string> tags)
    {
        Id = tags.GetRequiredTagValue("message-id", s => int.Parse(s));
        ThreadId = tags.GetRequiredTagValue("thread-id");
        UserType = tags.GetTagValue("user-type", string.Empty);
        UserId = tags.GetRequiredTagValue("user-id");
        DisplayName = tags.GetTagValue("display-name", string.Empty);
        Badges = ParseHelpers.ParseBadges(tags.GetTagValue("badges", string.Empty));
        BadgesInfo = ParseHelpers.ParseBadgesInfo(tags.GetTagValue("badge-info", string.Empty));
        Color = tags.GetTagValue("color", string.Empty);
        Emotes = ParseHelpers.ParseEmotes(tags.GetTagValue("emotes", string.Empty));
        IsTurbo = tags.GetTagValue("turbo", false, s => s == "1");
    }
}
