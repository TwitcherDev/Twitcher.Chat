namespace Twitcher.Chat.Interfaces.Models;

/// <summary></summary>
public interface IGlobalUserStateTags
{
    /// <summary>The type of user. Possible values are: <see cref="string.Empty"/> - normal user; 'mod' - moder on channel; 'admin', 'global_mod', 'staff' - twitch employee user types</summary>
    string UserType { get; }
    /// <summary>The user’s ID</summary>
    string UserId { get; }
    /// <summary>The user’s display name. This tag may be <see cref="string.Empty"/> if it is never set</summary>
    string DisplayName { get; }
    /// <summary>List of chat badges</summary>
    IReadOnlyCollection<IChatMessageBadge> Badges { get; }
    /// <summary>Contains metadata related to the chat badges in the <see cref="Badges"/> tag</summary>
    IReadOnlyDictionary<string, string> BadgesInfo { get; }
    /// <summary>The color of the user’s name in the chat room. This is a hexadecimal RGB color code in the form, #RRGGBB. This tag may be <see cref="string.Empty"/> if it is never set.</summary>
    string Color { get; }
    /// <summary>List of IDs that identify the emote sets that the user has access to. Is always set to at least zero (0)</summary>
    IReadOnlyCollection<string> EmoteSets { get; }
    /// <summary>Whether the user has site-wide commercial free mode enabled</summary>
    bool IsTurbo { get; }
}
