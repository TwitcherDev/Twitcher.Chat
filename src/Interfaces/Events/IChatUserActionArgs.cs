namespace Twitcher.Chat.Interfaces.Events;

/// <summary></summary>
public interface IChatUserActionArgs : IChatActionArgs
{
    /// <summary>The login name of the user who performs the action</summary>
    string Username { get; }
}
