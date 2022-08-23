namespace Twitcher.Chat.Client.Events;

internal class OnUserStateReceivedArgs : EventArgs, IUserStateReceivedArgs
{
    public string Channel { get; }
    public IUserStateTags? Tags { get; }

    internal OnUserStateReceivedArgs(string channel, IReadOnlyDictionary<string, string>? tags)
    {
        Channel = channel;
        if (tags != null)
            Tags = new UserStateTags(tags);
    }
}
