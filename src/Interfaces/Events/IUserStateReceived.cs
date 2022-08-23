namespace Twitcher.Chat.Interfaces.Events;

/// <summary></summary>
public interface IUserStateReceivedArgs
{
    /// <summary>The name of the channel that the bot joined or sent a privmsg in</summary>
    string Channel { get; }
    /// <summary>Additional information. <see cref="ITwitchChatOptions.TagsCapability"/> required</summary>
    IUserStateTags? Tags { get; }
}
