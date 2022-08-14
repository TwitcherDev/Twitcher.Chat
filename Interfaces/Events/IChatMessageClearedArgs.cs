namespace Twitcher.Chat.Interfaces.Events;

/// <summary></summary>
public interface IChatMessageClearedArgs
{
    /// <summary>The name of the channel where the message was removed from</summary>
    string Channel { get; }
    /// <summary>The chat message that was removed</summary>
    string? Message { get; }
    /// <summary>Additional information. <see cref="ITwitchChatOptions.TagsCapability"/> required</summary>
    IChatMessageClearedTags? Tags { get; }
}
