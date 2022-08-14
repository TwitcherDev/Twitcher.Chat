namespace Twitcher.Chat.Interfaces.Models;

/// <summary></summary>
public interface IUserNoticeSubReceivedTags : IUserNoticeReceivedTags
{
    /// <summary>The total number of months the user has subscribed</summary>
    int TotalSubscribeMonths { get; }
    /// <summary>Whether the user wants their streaks shared</summary>
    bool ShouldShareStreak { get; }
    /// <summary>The number of consecutive months the user has subscribed. This is <see langword="0"/> if <see cref="ShouldShareStreak"/> is <see langword="false"/>.</summary>
    int StreakMonths { get; }
    /// <summary>The type of subscription plan being used. Possible values are: 'Prime' — Amazon Prime subscription; '1000', '2000', '3000' - First, second and third levels of paid subscription, respectively</summary>
    string SubscriptionPlan { get; }
    /// <summary>The display name of the subscription plan. This may be a default name or one created by the channel owner</summary>
    string SubscriptionPlanName { get; }
}
