namespace Twitcher.Chat.Interfaces.Models;

/// <summary></summary>
public interface IUserNoticeSubGiftReceivedTags : IUserNoticeReceivedTags
{
    /// <summary>The total number of months the user has subscribed</summary>
    int TotalSubscribeMonths { get; }
    /// <summary>The display name of the subscription gift recipient</summary>
    string RecipientDisplayName { get; }
    /// <summary>The user ID of the subscription gift recipient</summary>
    string RecipientId { get; }
    /// <summary>The user name of the subscription gift recipient</summary>
    string RecipientUsername { get; }
    /// <summary>The type of subscription plan being used. Possible values are: 'Prime' — Amazon Prime subscription; '1000', '2000', '3000' - First, second and third levels of paid subscription, respectively</summary>
    string SubscriptionPlan { get; }
    /// <summary>The display name of the subscription plan. This may be a default name or one created by the channel owner</summary>
    string SubscriptionPlanName { get; }
    /// <summary>The number of months gifted as part of a single, multi-month gift</summary>
    int GiftMonths { get; }
}
