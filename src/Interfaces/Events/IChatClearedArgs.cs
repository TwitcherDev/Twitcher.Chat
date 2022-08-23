namespace Twitcher.Chat.Interfaces.Events;

/// <summary></summary>
public interface IChatClearedArgs
{
    /// <summary>The name of the channel where the messages were removed from</summary>
    string Channel { get; }
    /// <summary>The login name of the user whose messages were removed from the chat room because they were banned or put in a timeout</summary>
    string? Username { get; }
    /// <summary>Additional information. <see cref="ITwitchChatOptions.TagsCapability"/> required</summary>
    IChatClearedTags? Tags { get; }
}
