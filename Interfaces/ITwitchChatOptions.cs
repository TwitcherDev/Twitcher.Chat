namespace Twitcher.Chat.Interfaces;

/// <summary>Twitch chat options</summary>
public interface ITwitchChatOptions
{
    /// <summary>Use SSL to secure the connection</summary>
    bool UseSsl { get; }
    /// <summary>Automatically reconnect when disconnected</summary>
    bool AutoReconnect { get; }
    /// <summary>Automatically rejoin to channels when left</summary>
    bool AutoRejoin { get; }
    /// <summary>Lets your bot send messages that include Twitch chat commands and receive Twitch-specific IRC messages</summary>
    bool CommandsCapability { get; }
    /// <summary>Lets your bot receive messages when users join and leave the chat room</summary>
    bool MembershipCapability { get; }
    /// <summary>Adds additional information to the command and membership messages</summary>
    bool TagsCapability { get; }
}
