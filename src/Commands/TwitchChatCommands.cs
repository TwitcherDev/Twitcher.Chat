using Twitcher.Chat.Client;

namespace Twitcher.Chat.Commands;

/// <summary>Extension methods for chat commands</summary>
public static class TwitchChatCommands
{
    /// <summary>Bans a user from the channel</summary>
    /// <param name="chat">The instance of the chat that should use command</param>
    /// <param name="channel">The channel where the command should be executed</param>
    /// <param name="username">Login of the user to be banned</param>
    /// <param name="reason">Ban reason</param>
    public static void Ban(this ITwitchChat chat, string channel, string username, string? reason = null) => chat.SendMessage(channel, "/ban " + username + (reason != null ? ' ' + reason : ""));

    /// <summary>Unbans a user from the channel</summary>
    /// <param name="chat">The instance of the chat that should use command</param>
    /// <param name="channel">The channel where the command should be executed</param>
    /// <param name="username">Login of the user to be unbanned</param>
    public static void Unban(this ITwitchChat chat, string channel, string username) => chat.SendMessage(channel, "/unban " + username);

    /// <summary>Clears all messages from the chat room</summary>
    /// <param name="chat">The instance of the chat that should use command</param>
    /// <param name="channel">The channel where the command should be executed</param>
    public static void Clear(this ITwitchChat chat, string channel) => chat.SendMessage(channel, "/clear");

    /// <summary>Changes the color used for the bot’s username</summary>
    /// <param name="chat">The instance of the chat that should use command</param>
    /// <param name="channel">The channel where the command should be executed</param>
    /// <param name="color">Color string or color hex code with #</param>
    public static void Color(this ITwitchChat chat, string channel, string color) => chat.SendMessage(channel, "/color " + color);

    /// <summary>Runs a commercial</summary>
    /// <param name="chat">The instance of the chat that should use command</param>
    /// <param name="channel">The channel where the command should be executed</param>
    /// <param name="length">Desired length of the commercial in seconds</param>
    public static void Commercial(this ITwitchChat chat, string channel, int length = 30) => chat.SendMessage(channel, "/commercial " + length);

    /// <summary>Deletes the specified message from the chat room</summary>
    /// <param name="chat">The instance of the chat that should use command</param>
    /// <param name="channel">The channel where the command should be executed</param>
    /// <param name="messageId">Message id</param>
    public static void Delete(this ITwitchChat chat, string channel, Guid messageId) => chat.SendMessage(channel, "/delete " + messageId);

    /// <summary>Restricts users to posting chat messages that contain only emoticons</summary>
    /// <param name="chat">The instance of the chat that should use command</param>
    /// <param name="channel">The channel where the command should be executed</param>
    /// <param name="isEmoteonly">Is enable emoteonly mode</param>
    public static void Emoteonly(this ITwitchChat chat, string channel, bool isEmoteonly) => chat.SendMessage(channel, isEmoteonly ? "/emoteonly" : "/emoteonlyoff");

    /// <summary>Restricts who can post chat messages to followers only</summary>
    /// <param name="chat">The instance of the chat that should use command</param>
    /// <param name="channel">The channel where the command should be executed</param>
    /// <param name="length">Minimum length of time following channel in minutes. <see langword="null"/> if need to disable</param>
    public static void Followers(this ITwitchChat chat, string channel, int? length = null) => chat.SendMessage(channel, length != null ? "/followers " + length : "/followersoff");

    /// <summary>Returns a usage statement for the specified command. If you don’t specify a command, it returns all commands that the user can use</summary>
    /// <param name="chat">The instance of the chat that should use command</param>
    /// <param name="channel">The channel where the command should be executed</param>
    /// <param name="command">Command you need to get information about</param>
    public static void Help(this ITwitchChat chat, string channel, string? command = null) => chat.SendMessage(channel, "/help" + (command != null ? ' ' + command : ""));

    /// <summary>Hosts another channel in this channel. To stop hosting the other channel call this method without <paramref name="username"/></summary>
    /// <param name="chat">The instance of the chat that should use command</param>
    /// <param name="channel">The channel where the command should be executed</param>
    /// <param name="username">Login of the user to be hosted. <see langword="null"/> if need to stop hosting</param>
    public static void Host(this ITwitchChat chat, string channel, string? username = null) => chat.SendMessage(channel, username != null ? "/host " + username : "/unhost");

    /// <summary>Marks a section of the broadcast to highlight later</summary>
    /// <param name="chat">The instance of the chat that should use command</param>
    /// <param name="channel">The channel where the command should be executed</param>
    /// <param name="description">Marker description</param>
    public static void Marker(this ITwitchChat chat, string channel, string? description = null) => chat.SendMessage(channel, "/marker" + (description != null ? ' ' + description : ""));

    /// <summary>Removes the colon that typically appears after your chat name and italicizes the chat message’s text</summary>
    /// <param name="chat">The instance of the chat that should use command</param>
    /// <param name="channel">The channel where the command should be executed</param>
    /// <param name="message">Message text</param>
    public static void Me(this ITwitchChat chat, string channel, string message) => chat.SendMessage(channel, "/me " + message);

    /// <summary>Gives a user moderator privileges</summary>
    /// <param name="chat">The instance of the chat that should use command</param>
    /// <param name="channel">The channel where the command should be executed</param>
    /// <param name="username">Login of the user to give privileges</param>
    public static void Mod(this ITwitchChat chat, string channel, string username) => chat.SendMessage(channel, "/mod " + username);

    /// <summary>Revoke a user moderator privileges</summary>
    /// <param name="chat">The instance of the chat that should use command</param>
    /// <param name="channel">The channel where the command should be executed</param>
    /// <param name="username">Login of the user to revoke privileges</param>
    public static void Unmod(this ITwitchChat chat, string channel, string username) => chat.SendMessage(channel, "/unmod " + username);

    /// <summary>Lists the users that are moderators on the channel</summary>
    /// <param name="chat">The instance of the chat that should use command</param>
    /// <param name="channel">The channel where the command should be executed</param>
    public static void Mods(this ITwitchChat chat, string channel) => chat.SendMessage(channel, "/mods");

    /// <summary>Starts/stops a raid. A raid sends your viewers to the specified channel</summary>
    /// <param name="chat">The instance of the chat that should use command</param>
    /// <param name="channel">The channel where the command should be executed</param>
    /// <param name="username">Login of the user to be raided. <see langword="null"/> if need to stop raiding</param>
    public static void Raid(this ITwitchChat chat, string channel, string? username = null) => chat.SendMessage(channel, username != null ? "/raid " + username : "/unraid");

    /// <summary>Restricts how often users can post messages. This sets the minimum time, in seconds, that a user must wait before being allowed to post another message</summary>
    /// <param name="chat">The instance of the chat that should use command</param>
    /// <param name="channel">The channel where the command should be executed</param>
    /// <param name="length">Minimum wait time in seconds. <see langword="null"/> if need to disable</param>
    public static void Slow(this ITwitchChat chat, string channel, int? length = null) => chat.SendMessage(channel, length != null ? "/slow " + length : "/slowoff");

    /// <summary>Restricts who can post chat messages to subscribers only</summary>
    /// <param name="chat">The instance of the chat that should use command</param>
    /// <param name="channel">The channel where the command should be executed</param>
    /// <param name="isSubscribersonly">Is enable subscribersonly mode</param>
    public static void Subscribers(this ITwitchChat chat, string channel, bool isSubscribersonly) => chat.SendMessage(channel, isSubscribersonly ? "/subscribers" : "/subscribersoff");

    /// <summary>Restricts a user’s chat messages to unique messages only; a user cannot send duplicate chat messages</summary>
    /// <param name="chat">The instance of the chat that should use command</param>
    /// <param name="channel">The channel where the command should be executed</param>
    /// <param name="isUniquechat">Is enable uniquechat mode</param>
    public static void Uniquechat(this ITwitchChat chat, string channel, bool isUniquechat) => chat.SendMessage(channel, isUniquechat ? "/uniquechat" : "/uniquechatoff");

    /// <summary>Grants VIP status to a user</summary>
    /// <param name="chat">The instance of the chat that should use command</param>
    /// <param name="channel">The channel where the command should be executed</param>
    /// <param name="username">Login of the user to grants VIP</param>
    public static void Vip(this ITwitchChat chat, string channel, string username) => chat.SendMessage(channel, "/vip " + username);

    /// <summary>Revoke VIP status of a user</summary>
    /// <param name="chat">The instance of the chat that should use command</param>
    /// <param name="channel">The channel where the command should be executed</param>
    /// <param name="username">Login of the user to revoke VIP</param>
    public static void Unvip(this ITwitchChat chat, string channel, string username) => chat.SendMessage(channel, "/unvip " + username);

    /// <summary>Lists the users with VIP status in the channel</summary>
    /// <param name="chat">The instance of the chat that should use command</param>
    /// <param name="channel">The channel where the command should be executed</param>
    public static void Vips(this ITwitchChat chat, string channel) => chat.SendMessage(channel, "/vips");

    /// <summary>Sends a private message to another user on Twitch. NOTE The whisper chat command is available to legacy chatbots only</summary>
    /// <param name="chat">The instance of the chat that should use command</param>
    /// <param name="channel">The channel where the command should be executed</param>
    /// <param name="username">The login of the user to receive the whisper</param>
    /// <param name="message">The whisper message to send</param>
    public static void Whisper(this ITwitchChat chat, string channel, string username, string message) => chat.SendMessage(channel, "/w " + username + ' ' + message);
}
