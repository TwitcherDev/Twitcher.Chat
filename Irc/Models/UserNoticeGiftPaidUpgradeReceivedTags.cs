namespace Twitcher.Chat.Irc.Models;

internal class UserNoticeGiftPaidUpgradeReceivedTags : UserNoticeReceivedTags, IUserNoticeGiftPaidUpgradeReceivedTags
{
    public int GiftTotal { get; }
    public string PromoName { get; }
    public string? SenderUsername { get; }
    public string? SenderDisplayName { get; }

    internal UserNoticeGiftPaidUpgradeReceivedTags(IReadOnlyDictionary<string, string> tags) : base(tags)
    {
        GiftTotal = tags.GetRequiredTagValue("msg-param-promo-gift-total", s => int.Parse(s));
        PromoName = tags.GetRequiredTagValue("msg-param-promo-name");
        SenderUsername = tags.GetTagValue("msg-param-sender-login");
        SenderDisplayName = tags.GetTagValue("msg-param-sender-name");
    }
}
