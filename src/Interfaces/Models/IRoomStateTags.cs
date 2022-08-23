namespace Twitcher.Chat.Interfaces.Models;

/// <summary></summary>
public interface IRoomStateTags
{
    /// <summary>An ID that identifies the channel</summary>
    string ChannelId { get; }
    /// <summary>Whether the chat room allows only messages with emotes</summary>
    bool IsEmoteOnly { get; }
    /// <summary>Whether only followers can chat in the chat room</summary>
    bool IsFollowersOnly { get; }
    /// <summary>The value indicates how long, in minutes, the user must have followed the broadcaster before posting chat messages. Is <see langword="-1"/> if <see cref="IsFollowersOnly"/> is <see langword="false"/></summary>
    int FollowersOnly { get; }
    /// <summary>Whether a user’s messages must be unique</summary>
    bool IsR9k { get; }
    /// <summary>The value indicates how long, in seconds, users must wait between sending messages</summary>
    int Slow { get; }
    /// <summary>Whether only subscribers and moderators can chat in the chat room</summary>
    bool IsSubscribersOnly { get; }
}
