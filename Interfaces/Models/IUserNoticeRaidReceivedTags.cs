namespace Twitcher.Chat.Interfaces.Models;

/// <summary></summary>
public interface IUserNoticeRaidReceivedTags : IUserNoticeReceivedTags
{
    /// <summary>The display name of the broadcaster raiding this channel</summary>
    string BroadcasterRaidingDisplayName { get; }
    /// <summary>The login name of the broadcaster raiding this channel</summary>
    string BroadcasterRaidingUsername { get; }
    /// <summary>The number of viewers raiding this channel from the broadcaster’s channel</summary>
    int ViewersCount { get; }
}
