namespace Twitcher.Chat.Client.Models;

internal class UserNoticeSubReceivedTags : UserNoticeReceivedTags, IUserNoticeSubReceivedTags
{
    public int TotalSubscribeMonths { get; }
    public bool ShouldShareStreak { get; }
    public int StreakMonths { get; }
    public string SubscriptionPlan { get; }
    public string SubscriptionPlanName { get; }

    internal UserNoticeSubReceivedTags(IReadOnlyDictionary<string, string> tags) : base(tags)
    {
        TotalSubscribeMonths = tags.GetRequiredTagValue("msg-param-cumulative-months", s => int.Parse(s));
        ShouldShareStreak = tags.GetRequiredTagValue("msg-param-should-share-streak", s => s == "1");
        StreakMonths = tags.GetRequiredTagValue("msg-param-streak-months", s => int.Parse(s));
        SubscriptionPlan = tags.GetRequiredTagValue("msg-param-sub-plan");
        SubscriptionPlanName = tags.GetRequiredTagValue("msg-param-sub-plan-name");
    }
}
