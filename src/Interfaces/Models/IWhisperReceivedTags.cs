namespace Twitcher.Chat.Interfaces.Models;

/// <summary></summary>
public interface IWhisperReceivedTags
{
    /// <summary>An ID that uniquely identifies the whisper message</summary>
    int Id { get; }
    /// <summary>An ID that uniquely identifies the whisper thread. The ID is in the form, '{smaller-value-user-id}_{larger-value-user-id}'</summary>
    string ThreadId { get; }
    /// <summary>The type of user sending the whisper message. Possible values are: <see cref="string.Empty"/> - normal user; 'admin', 'global_mod', 'staff' - twitch employee user types</summary>
    string UserType { get; }
    /// <summary>The ID of the user sending the whisper message</summary>
    string UserId { get; }
    /// <summary>The display name of the user sending the whisper message. This tag may be <see cref="string.Empty"/> if it is never set</summary>
    string DisplayName { get; }
    /// <summary>List of chat badges</summary>
    IReadOnlyCollection<IChatMessageBadge> Badges { get; }
    /// <summary>Contains metadata related to the chat badges in the <see cref="Badges"/> tag</summary>
    IReadOnlyDictionary<string, string> BadgesInfo { get; }
    /// <summary>The color of the user’s name in the chat room. This is a hexadecimal RGB color code in the form, #RRGGBB. This tag may be <see cref="string.Empty"/> if it is never set.</summary>
    string Color { get; }
    /// <summary>List of emotes and their positions in the message</summary>
    IReadOnlyDictionary<string, IReadOnlyCollection<IChatMessageEmotePosition>> Emotes { get; }
    /// <summary>Indicates whether the user has site-wide commercial free mode enabled</summary>
    bool IsTurbo { get; }
}
