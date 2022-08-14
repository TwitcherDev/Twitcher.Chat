namespace Twitcher.Chat.Irc.Events;

internal class OnRoomStateReceivedArgs : EventArgs, IRoomStateReceivedArgs
{
    public string Channel { get; }
    public IRoomStateTags? Tags { get; }

    internal OnRoomStateReceivedArgs(string channel, IRoomStateTags? tags)
    {
        Channel = channel;
        Tags = tags;
    }
}
