namespace Twitcher.Chat.Irc;

internal class TwitcherIrcMessage
{
    internal string? Username { get; }
    internal string? Host { get; }
    internal string Command { get; }
    internal string? Channel { get; }
    internal string? Params { get; }
    internal IReadOnlyDictionary<string, string>? Tags { get; }

    private string[]? _separatedParams;
    internal string[]? SeparatedParams
    {
        get
        {
            if (string.IsNullOrEmpty(Params))
                return null;
            if (_separatedParams == null)
                _separatedParams = Params.Split(' ');
            return _separatedParams;
        }
    }

    internal TwitcherIrcMessage(string? username, string? host, string command, string? channel, string? parameters, IReadOnlyDictionary<string, string>? tags)
    {
        Username = username;
        Host = host;
        Command = command;
        Channel = channel;
        Params = parameters;
        Tags = tags;
    }

    internal static TwitcherIrcMessage Parse(string message)
    {
        var cursor = 0;
        int endCursor;

        Dictionary<string, string>? tags = null;
        if (message[cursor] == '@')
        {
            cursor += 1;
            endCursor = message.IndexOf(' ');
            tags = message[cursor..endCursor].Split(';').ToDictionary(s => s[..s.IndexOf('=')], s => s[(s.IndexOf('=') + 1)..]);
            cursor = endCursor + 1;
        }

        string? username = null;
        string? host = null;
        if (message[cursor] == ':')
        {
            cursor += 1;
            endCursor = message.IndexOf(' ', cursor);
            var rawSource = message[cursor..endCursor];

            var id = rawSource.IndexOf('!');
            if (id > -1)
            {
                username = rawSource[..id];
                host = rawSource[(id + 1)..];
            }
            else
                host = rawSource;

            cursor = endCursor + 1;
        }

        string? parameters = null;
        endCursor = message.IndexOf(":", cursor) - 1;
        if (endCursor != -2)
            parameters = message[(endCursor + 2)..];
        else
            endCursor = message.Length;

        string command;
        string? channel = null;
        {
            var rawCommand = message[cursor..endCursor];
            var id = rawCommand.IndexOf(' ');
            if (id > -1)
            {
                command = rawCommand[..id];
                var id1 = rawCommand.IndexOf('#');
                if (id1 > -1)
                    channel = rawCommand[(id1 + 1)..];
            }
            else
                command = rawCommand;
        }

        return new TwitcherIrcMessage(username, host, command, channel, parameters, tags);
    }
}
