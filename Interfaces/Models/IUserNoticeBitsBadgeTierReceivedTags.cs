namespace Twitcher.Chat.Interfaces.Models;

/// <summary></summary>
public interface IUserNoticeBitsBadgeTierReceivedTags : IUserNoticeReceivedTags
{
    /// <summary>The tier of the Bits badge the user just earned. For example, 100, 1000, or 10000</summary>
    string Threshold { get; }
}
