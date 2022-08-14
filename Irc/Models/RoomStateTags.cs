namespace Twitcher.Chat.Irc.Models;

internal class RoomStateTags : IRoomStateTags
{
    public string ChannelId { get; }
    public bool IsEmoteOnly { get; private set; }
    public bool IsFollowersOnly => FollowersOnly != -1;
    public int FollowersOnly { get; private set; }
    public bool IsR9k { get; private set; }
    public int Slow { get; private set; }
    public bool IsSubscribersOnly { get; private set; }

    internal RoomStateTags(IReadOnlyDictionary<string, string> tags)
    {
        ChannelId = tags.GetRequiredTagValue("room-id");
        IsEmoteOnly = tags.GetRequiredTagValue("emote-only", s => s == "1");
        FollowersOnly = tags.GetRequiredTagValue("followers-only", s => int.Parse(s));
        IsR9k = tags.GetRequiredTagValue("r9k", s => s == "1");
        Slow = tags.GetRequiredTagValue("slow", s => int.Parse(s));
        IsSubscribersOnly = tags.GetRequiredTagValue("subs-only", s => s == "1");
    }

    internal void Change(IReadOnlyDictionary<string, string> tags)
    {
        foreach (var (key, value) in tags)
        {
            switch (key)
            {
                case "emote-only": IsEmoteOnly = value == "1"; continue;
                case "followers-only":
                    try
                    {
                        FollowersOnly = int.Parse(value);
                    }
                    catch (FormatException e)
                    {
                        throw new FormatException($"Tags parsing error: tag 'followers-only' has wrong format", e);
                    }
                    continue;
                case "r9k": IsR9k = value == "1"; continue;
                case "slow":
                    try
                    {
                        Slow = int.Parse(value);
                    }
                    catch (FormatException e)
                    {
                        throw new FormatException($"Tags parsing error: tag 'slow' has wrong format", e);
                    }
                    continue;
                case "subs-only": IsSubscribersOnly = value == "1"; continue;
            }
        }
    }
}
