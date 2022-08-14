namespace Twitcher.Chat.Interfaces.Models;

/// <summary></summary>
public interface IChatMessageReplyParent
{
    /// <summary>An ID that uniquely identifies the parent message that this message is replying to</summary>
    Guid Id { get; }
    /// <summary>An ID that identifies the sender of the parent message</summary>
    string UserId { get; }
    /// <summary>The login name of the sender of the parent message</summary>
    string UserLogin { get; }
    /// <summary>The display name of the sender of the parent message</summary>
    string DisplayName { get; }
    /// <summary>The text of the parent message</summary>
    string Message { get; }
}
