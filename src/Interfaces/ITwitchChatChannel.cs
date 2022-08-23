using System.Diagnostics.CodeAnalysis;

namespace Twitcher.Chat.Interfaces;

/// <summary></summary>
public interface ITwitchChatChannel
{
    /// <summary>The username of the user-owner of the chat room</summary>
    public string Username { get; }
    /// <summary>Whether the bot is in the chat room</summary>
    public bool IsJoined { get; }
    /// <summary>The usernames of users who are in the chat room. <see cref="ITwitchChatOptions.MembershipCapability"/> required</summary>
    public List<string>? Members { get; }
    /// <summary>Chat room info. <see cref="ITwitchChatOptions.CommandsCapability"/> and <see cref="ITwitchChatOptions.TagsCapability"/> required</summary>
    public IRoomStateTags? RoomStateTags { get; }
    /// <summary>Information about the bot. <see cref="ITwitchChatOptions.CommandsCapability"/> and <see cref="ITwitchChatOptions.TagsCapability"/> required</summary>
    public IUserStateTags? UserStateTags { get; }
    /// <summary>Does this channel host. <see cref="ITwitchChatOptions.CommandsCapability"/> required</summary>
    [MemberNotNullWhen(true, nameof(HostTargetUsername))]
    public bool IsHosting { get; }
    /// <summary>The channel to which the hosting is going. <see cref="ITwitchChatOptions.CommandsCapability"/> required</summary>
    public string? HostTargetUsername { get; }
}
