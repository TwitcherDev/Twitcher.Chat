namespace Twitcher.Chat.Irc.Models;

internal class UserNoticeSubGiftReceivedTags : UserNoticeReceivedTags, IUserNoticeSubGiftReceivedTags
{
    public int TotalSubscribeMonths { get; }
    public string RecipientDisplayName { get; }
    public string RecipientId { get; }
    public string RecipientUsername { get; }
    public string SubscriptionPlan { get; }
    public string SubscriptionPlanName { get; }
    public int GiftMonths { get; }

    internal UserNoticeSubGiftReceivedTags(IReadOnlyDictionary<string, string> tags) : base(tags)
    {
        TotalSubscribeMonths = tags.GetRequiredTagValue("msg-param-months", s => int.Parse(s));
        RecipientDisplayName = tags.GetRequiredTagValue("msg-param-recipient-display-name");
        RecipientId = tags.GetRequiredTagValue("msg-param-recipient-id");
        RecipientUsername = tags.GetRequiredTagValue("msg-param-recipient-user-name");
        SubscriptionPlan = tags.GetRequiredTagValue("msg-param-sub-plan");
        SubscriptionPlanName = tags.GetRequiredTagValue("msg-param-sub-plan-name");
    }
}
