namespace Twitcher.Chat.Interfaces.Models;

/// <summary></summary>
public interface IUserStateTags
{
    /// <summary>If a privmsg was sent, an ID that uniquely identifies the message</summary>
    Guid? Id { get; }
    /// <summary>The type of user. Possible values are: <see cref="string.Empty"/> - normal user; 'mod' - moder on channel; 'admin', 'global_mod', 'staff' - twitch employee user types</summary>
    string UserType { get; }
    /// <summary>The user’s display name. This tag may be <see cref="string.Empty"/> if it is never set</summary>
    string DisplayName { get; }
    /// <summary>List of chat badges</summary>
    IReadOnlyCollection<IChatMessageBadge> Badges { get; }
    /// <summary>Contains metadata related to the chat badges in the <see cref="Badges"/> tag</summary>
    IReadOnlyDictionary<string, string> BadgesInfo { get; }
    /// <summary>Number of months the user has been a subscriber</summary>
    int SubscribeMonths { get; }
    /// <summary>Who was voted for in the voting predictions</summary>
    string? Predictions { get; }
    /// <summary>The color of the user’s name in the chat room. This is a hexadecimal RGB color code in the form, #RRGGBB. This tag may be <see cref="string.Empty"/> if it is never set.</summary>
    string Color { get; }
    /// <summary>List of IDs that identify the emote sets that the user has access to. Is always set to at least zero (0)</summary>
    IReadOnlyCollection<string> EmoteSets { get; }
    /// <summary>Whether the user is a moderator</summary>
    bool IsMod { get; }
    /// <summary>Whether the user is a VIP</summary>
    bool IsVip { get; }
    /// <summary>Whether the user is a subscriber</summary>
    bool IsSubscriber { get; }
    /// <summary>Whether the user has site-wide commercial free mode enabled</summary>
    bool IsTurbo { get; }
}
