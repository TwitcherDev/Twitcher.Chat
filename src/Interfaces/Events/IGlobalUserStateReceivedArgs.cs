namespace Twitcher.Chat.Interfaces.Events;

/// <summary></summary>
public interface IGlobalUserStateReceivedArgs
{
    /// <summary>Additional information. <see cref="ITwitchChatOptions.TagsCapability"/> required</summary>
    IGlobalUserStateTags? Tags { get; }
}
