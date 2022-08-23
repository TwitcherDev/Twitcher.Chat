namespace Twitcher.Chat.Interfaces.Models;

/// <summary></summary>
public interface IChatMessageEmotePosition
{
    /// <summary>Start position. The position indices are zero-based</summary>
    int StartPosition { get; }
    /// <summary>End position. The position indices are zero-based</summary>
    int EndPosition { get; }
}
