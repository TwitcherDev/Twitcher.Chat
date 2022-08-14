namespace Twitcher.Chat.Interfaces.Models;

/// <summary></summary>
public interface IUserNoticeReceivedTags
{
    /// <summary>An ID that uniquely identifies the message</summary>
    Guid Id { get; }
    /// <summary>An ID that identifies the channel</summary>
    string ChannelId { get; }
    /// <summary>The type of notice</summary>
    string MessageType { get; }
    /// <summary>The message Twitch shows in the chat room for this notice</summary>
    string SystemMessage { get; }
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
    /// <summary>Number of months the user has been a subscriber</summary>
    int SubscribeMonths { get; }
    /// <summary>Who was voted for in the voting predictions</summary>
    string? Predictions { get; }
    /// <summary>The color of the user’s name in the chat room. This is a hexadecimal RGB color code in the form, #RRGGBB. This tag may be <see cref="string.Empty"/> if it is never set.</summary>
    string Color { get; }
    /// <summary>List of emotes and their positions in the message</summary>
    IReadOnlyDictionary<string, IReadOnlyCollection<IChatMessageEmotePosition>> Emotes { get; }
    /// <summary>Whether the user is a broadcaster</summary>
    bool IsBroadcast { get; }
    /// <summary>Whether the user is a moderator</summary>
    bool IsMod { get; }
    /// <summary>Whether the user is a VIP (value from badges)</summary>
    bool IsVip { get; }
    /// <summary>Whether the user is a subscriber</summary>
    bool IsSubscriber { get; }
    /// <summary>Whether the user has site-wide commercial free mode enabled</summary>
    bool IsTurbo { get; }
    /// <summary>Sent time</summary>
    DateTime SentTime { get; }
}
