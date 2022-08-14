namespace Twitcher.Chat.Interfaces.Models;

/// <summary></summary>
public interface IChatClearedTags
{
    /// <summary>Optional. The message includes this tag if the user was put in a timeout. The tag contains the duration of the timeout</summary>
    TimeSpan? BanDuration { get; }
    /// <summary>The ID of the channel where the messages were removed from</summary>
    string ChannelId { get; }
    /// <summary>Optional. The ID of the user that was banned or put in a timeout. The user was banned if the message doesn’t include the <see cref="BanDuration"/> tag</summary>
    string? TargetUserId { get; }
    /// <summary>Sent time</summary>
    DateTime SentTime { get; }
}
