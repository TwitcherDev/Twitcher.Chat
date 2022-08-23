namespace Twitcher.Chat.Irc;

internal class TwitcherIrcChannel : ITwitchChatChannel
{
    public string Username { get; private set; }
    public bool IsJoined { get; internal set; }
    public List<string>? Members { get; private set; }
    public IRoomStateTags? RoomStateTags { get; internal set; }
    public IUserStateTags? UserStateTags { get; internal set; }
    public bool IsHosting => HostTargetUsername != null;
    public string? HostTargetUsername { get; internal set; }

    internal TwitcherIrcChannel(string username, bool isMembers)
    {
        Username = username.ToLower();
        if (isMembers)
            Members = new List<string>();
    }

    internal void AddMembers(IEnumerable<string> names)
    {
        foreach (var name in names)
            AddMember(name);
    }

    internal void AddMember(string name)
    {
        if (!Members!.Contains(name))
            Members.Add(name);
    }

    internal void RemoveMember(string name)
    {
        Members!.Remove(name);
    }
}
