namespace Twitcher.Chat.Interfaces.Events;

/// <summary></summary>
public interface IWhisperReceivedArgs
{
    /// <summary>The user that’s sending the whisper message</summary>
    string FromUser { get; }
    /// <summary>The user that’s receiving the whisper message</summary>
    string ToUser { get; }
    /// <summary>Whisper message</summary>
    string Message { get; }
    /// <summary>Additional information. <see cref="ITwitchChatOptions.TagsCapability"/> required</summary>
    IWhisperReceivedTags? Tags { get; }
}
