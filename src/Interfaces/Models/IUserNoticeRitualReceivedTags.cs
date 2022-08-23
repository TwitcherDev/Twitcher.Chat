namespace Twitcher.Chat.Interfaces.Models;

/// <summary></summary>
public interface IUserNoticeRitualReceivedTags : IUserNoticeReceivedTags
{
    /// <summary>The name of the ritual being celebrated. Possible values are: 'new_chatter'</summary>
    string RitualName { get; }
}
