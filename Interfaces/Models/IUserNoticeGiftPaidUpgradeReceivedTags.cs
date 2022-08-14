namespace Twitcher.Chat.Interfaces.Models;

/// <summary></summary>
public interface IUserNoticeGiftPaidUpgradeReceivedTags : IUserNoticeReceivedTags
{
    /// <summary>The number of gifts the gifter has given during the promo indicated by <see cref="PromoName"/></summary>
    int GiftTotal { get; }
    /// <summary>The subscriptions promo, if any, that is ongoing (for example, 'Subtember 2018')</summary>
    string PromoName { get; }
    /// <summary>The login name of the user who gifted the subscription. <see langword="null"/> if the giver is anonymous</summary>
    string? SenderUsername { get; }
    /// <summary>The display name of the user who gifted the subscription. <see langword="null"/> if the giver is anonymous</summary>
    string? SenderDisplayName { get; }
}
