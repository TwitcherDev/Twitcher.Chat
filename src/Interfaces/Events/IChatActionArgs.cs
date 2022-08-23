namespace Twitcher.Chat.Interfaces.Events;

/// <summary></summary>
public interface IChatActionArgs
{
    /// <summary>The name of the channel where the action occurred</summary>
    string Channel { get; }
}
