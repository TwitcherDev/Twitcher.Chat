namespace Twitcher.Chat.Client.Events;

internal class OnChatLeftArgs : EventArgs, IChatActionArgs
{
    public string Channel { get; }

    internal OnChatLeftArgs(string channel)
    {
        Channel = channel;
    }
}
