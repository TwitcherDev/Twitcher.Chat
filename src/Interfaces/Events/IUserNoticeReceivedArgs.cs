namespace Twitcher.Chat.Interfaces.Events;

/// <summary></summary>
public interface IUserNoticeReceivedArgs<TTag>
{
    /// <summary>The name of the channel that the event occurred in</summary>
    string Channel { get; }
    /// <summary>Optional. The chat message that describes the event</summary>
    string? Message { get; }
    /// <summary>Additional information. <see cref="ITwitchChatOptions.TagsCapability"/> required</summary>
    TTag? Tags { get; }
}
