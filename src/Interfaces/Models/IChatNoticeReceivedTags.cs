namespace Twitcher.Chat.Interfaces.Models;

/// <summary></summary>
public interface IChatNoticeReceivedTags
{
    /// <summary>An ID that you can use to programmatically determine the action’s outcome. Possible values are listed in <see cref="NoticeMessageIds"/></summary>
    string MessageId { get; }
    /// <summary>The ID of the user that the action targeted</summary>
    string? TargetUserId { get; }
}
