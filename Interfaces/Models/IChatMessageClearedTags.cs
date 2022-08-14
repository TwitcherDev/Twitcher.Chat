namespace Twitcher.Chat.Interfaces.Models;

/// <summary></summary>
public interface IChatMessageClearedTags
{
    /// <summary>The name of the user who sent the message</summary>
    string Username { get; }
    /// <summary>Optional. The ID of the channel where the message was removed from</summary>
    string? ChannelId { get; }
    /// <summary>A UUID that identifies the message that was removed</summary>
    Guid TargetMessageId { get; }
    /// <summary>Sent time</summary>
    DateTime SentTime { get; }
}
