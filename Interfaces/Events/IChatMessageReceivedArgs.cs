namespace Twitcher.Chat.Interfaces.Events;

/// <summary></summary>
public interface IChatMessageReceivedArgs
{
    /// <summary>The name of the channel on which the message was received</summary>
    string Channel { get; }
    /// <summary>Username of the user who sent the message</summary>
    string Username { get; }
    /// <summary>The message that the user posted in the channel chat</summary>
    string Message { get; }
    /// <summary>Additional information. <see cref="ITwitchChatOptions.TagsCapability"/> required</summary>
    IChatMessageReceivedTags? Tags { get; }
}
