namespace Twitcher.Chat;

/// <summary>Twitcher chat options</summary>
public class TwitcherChatOptions : ITwitchChatOptions
{
    /// <summary>Twitcher chat options instance with default options</summary>
    public static TwitcherChatOptions Default => new();

    /// <summary>Twitcher chat options instance with all additional events and tags information</summary>
    public static TwitcherChatOptions AllCapability => new() { CommandsCapability = true, MembershipCapability = true, TagsCapability = true };

    /// <summary>Use SSL to secure the connection. Default: true</summary>
    public bool UseSsl { get; set; } = true;
    /// <summary>Automatically reconnect when disconnected. Default: true</summary>
    public bool AutoReconnect { get; set; } = true;
    /// <summary>Automatically join to all channels when bot connect to a twitch and rejoin if bot left a channel. Default: true</summary>
    public bool AutoRejoin { get; set; } = true;
    /// <summary>Lets your bot send messages that include Twitch chat commands and receive Twitch-specific IRC messages. Default: false</summary>
    public bool CommandsCapability { get; set; } = false;
    /// <summary>Lets your bot receive messages when users join and leave the chat room. Default: false</summary>
    public bool MembershipCapability { get; set; } = false;
    /// <summary>Adds additional tags information to the command and membership messages. Default: false</summary>
    public bool TagsCapability { get; set; } = false;
}
