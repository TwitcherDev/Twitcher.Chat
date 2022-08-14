namespace Twitcher.Chat.Interfaces.Events;

/// <summary></summary>
public interface IChatHostTargetReceivedArgs
{
    /// <summary>The channel that’s hosting the viewers</summary>
    string HostingChannel { get; }
    /// <summary>The channel being hosted or "<see langword="-"/>" if the channel is no longer hosted</summary>
    string Channel { get; }
    /// <summary>The number of viewers from <see cref="Channel"/> that are watching the broadcast. <see langword="-1"/> unless otherwise specified</summary>
    int Viewers { get; }

    /// <summary><see langword="true"/> if <see cref="Channel"/> is hosted; otherwise <see langword="false"/></summary>
    bool IsHosting { get; }
}
