namespace Twitcher.Chat.Interfaces.Models;

/// <summary></summary>
public interface IChatMessageBadge
{
    /// <summary>Badge name. There are many possible badge values, but here are few: 'admin', 'bits', 'broadcaster', 'moderator', 'subscriber', 'staff', 'turbo'</summary>
    string Badge { get; }
    /// <summary>Badge version. Most badges have only 1 version, but some badges like subscriber badges offer different versions of the badge depending on how long the user has subscribed</summary>
    string Version { get; }
}
