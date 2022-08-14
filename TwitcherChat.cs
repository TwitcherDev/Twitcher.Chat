using Microsoft.Extensions.Logging;

namespace Twitcher.Chat;

/// <summary>Produces instances of ITwitchChat classes</summary>
public static class TwitcherChat
{
    /// <summary>Creates a new <see cref="ITwitchChat"/> instance that uses an IRC connection</summary>
    /// <param name="username">Username of the <paramref name="token"/> owner</param>
    /// <param name="token">Access token for authentication</param>
    /// <param name="options">Twitch chat options</param>
    /// <param name="logger"><see cref="ILogger"/> for logging</param>
    /// <returns>The <see cref="ITwitchChat"/> that was created</returns>
    public static ITwitchChat CreateIrcClient(string username, string token, ITwitchChatOptions? options = null, ILogger? logger = null)
    {
        return new TwitcherIrcChatClient(username, token, options, logger);
    }
}
