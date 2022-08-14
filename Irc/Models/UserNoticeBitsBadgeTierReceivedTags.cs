namespace Twitcher.Chat.Irc.Models;

internal class UserNoticeBitsBadgeTierReceivedTags : UserNoticeReceivedTags, IUserNoticeBitsBadgeTierReceivedTags
{
    public string Threshold { get; }

    internal UserNoticeBitsBadgeTierReceivedTags(IReadOnlyDictionary<string, string> tags) : base(tags)
    {
        Threshold = tags.GetRequiredTagValue("msg-param-threshold");
    }
}
