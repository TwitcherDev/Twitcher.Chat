namespace Twitcher.Chat.Client;

internal static class ParseHelpers
{
    internal static IReadOnlyCollection<IChatMessageBadge> ParseBadges(string rawBadges)
    {
        return rawBadges.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(b =>
        {
            var sep = b.IndexOf('/');
            if (sep == -1)
                throw new FormatException($"Tags parsing error: badge '{b}' has the wrong format");
            return (IChatMessageBadge)new ChatMessageBadge(b[..sep], b[(sep + 1)..]);
        }).ToList().AsReadOnly();
    }

    internal static IReadOnlyDictionary<string, string> ParseBadgesInfo(string rawBadgesInfo)
    {
        return rawBadgesInfo.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(i =>
        {
            var sep = i.IndexOf('/');
            if (sep == -1)
                throw new FormatException($"Tags parsing error: badge-info '{i}' has the wrong format");
            return (key: i[..sep], value: i[(sep + 1)..]);
        }).ToDictionary(k => k.key, k => k.value);
    }

    internal static IReadOnlyDictionary<string, IReadOnlyCollection<IChatMessageEmotePosition>> ParseEmotes(string rawEmotes)
    {
        return rawEmotes.Split('/', StringSplitOptions.RemoveEmptyEntries).Select(e =>
        {
            var sep = e.IndexOf(':');
            if (sep == -1)
                throw new FormatException($"Tags parsing error: emote '{e}' has wrong format");
            try
            {
                return (key: e[..sep], value: (IReadOnlyCollection<IChatMessageEmotePosition>)e[(sep + 1)..].Split(',')
                    .Select(p =>
                    {
                        var sep1 = p.IndexOf('-');
                        if (sep1 == -1)
                            throw new FormatException($"Tags parsing error: emote position '{p}' has wrong format");
                        try
                        {
                            return (IChatMessageEmotePosition)new ChatMessageEmotePosition(int.Parse(p.AsSpan(0, sep1)), int.Parse(p.AsSpan(sep1 + 1)));
                        }
                        catch (FormatException e)
                        {
                            throw new FormatException($"Tags parsing error: emote position '{p}' has wrong format", e);
                        }
                    }).ToList().AsReadOnly());
            }
            catch (FormatException ex)
            {
                throw new FormatException($"Tags parsing error: emote id '{e}' has wrong format", ex);
            }
        }).ToDictionary(e => e.key, e => e.value);
    }
}
