namespace Twitcher.Chat.Client.Events;

internal class OnUserJoinedArgs : EventArgs, IChatUserActionArgs
{
    public string Channel { get; }
    public string Username { get; }

    internal OnUserJoinedArgs(string channel, string username)
    {
        Channel = channel;
        Username = username;
    }
}
