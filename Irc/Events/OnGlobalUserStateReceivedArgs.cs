namespace Twitcher.Chat.Irc.Events;

internal class OnGlobalUserStateReceivedArgs : EventArgs, IGlobalUserStateReceivedArgs
{
    public IGlobalUserStateTags? Tags { get; }

    internal OnGlobalUserStateReceivedArgs(IReadOnlyDictionary<string, string>? tags)
    {
        if (tags != null)
            Tags = new GlobalUserStateTags(tags);
    }
}
