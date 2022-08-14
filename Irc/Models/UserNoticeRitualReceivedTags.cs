namespace Twitcher.Chat.Irc.Models;

internal class UserNoticeRitualReceivedTags : UserNoticeReceivedTags, IUserNoticeRitualReceivedTags
{
    public string RitualName { get; }

    internal UserNoticeRitualReceivedTags(IReadOnlyDictionary<string, string> tags) : base(tags)
    {
        RitualName = tags.GetRequiredTagValue("msg-param-ritual-name");
    }
}
