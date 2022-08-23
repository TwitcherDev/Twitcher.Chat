namespace Twitcher.Chat.Client.Events;

internal class OnUserLeftArgs : EventArgs, IChatUserActionArgs
{
    public string Channel { get; }
    public string Username { get; }

    internal OnUserLeftArgs(string channel, string username)
    {
        Channel = channel;
        Username = username;
    }
}
