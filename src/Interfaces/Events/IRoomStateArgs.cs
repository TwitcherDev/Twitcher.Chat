namespace Twitcher.Chat.Interfaces.Events;

/// <summary></summary>
public interface IRoomStateReceivedArgs
{
    /// <summary>The name of the channel that the room state information applies to</summary>
    string Channel { get; }
    /// <summary>Additional information. <see cref="ITwitchChatOptions.TagsCapability"/> required</summary>
    IRoomStateTags? Tags { get; }
}
