namespace Twitcher.Chat.Interfaces.Events;

/// <summary></summary>
public interface IChatNoticeReceivedArgs
{
    /// <summary>The name of the channel where the action occurred</summary>
    string Channel { get; }
    /// <summary>A message that describes the outcome of the action</summary>
    string Message { get; }
    /// <summary>Additional information. <see cref="ITwitchChatOptions.TagsCapability"/> required</summary>
    IChatNoticeReceivedTags? Tags { get; }
}
