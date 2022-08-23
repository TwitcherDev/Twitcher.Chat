namespace Twitcher.Chat.Client.Events;

internal class OnChatHostTargetReceivedArgs : EventArgs, IChatHostTargetReceivedArgs
{
    public string HostingChannel { get; }
    public string Channel { get; }
    public int Viewers { get; }
    public bool IsHosting => Channel != "-";

    internal OnChatHostTargetReceivedArgs(string hostingChannel, string channel, int viewers)
    {
        HostingChannel = hostingChannel;
        Channel = channel;
        Viewers = viewers;
    }
}
