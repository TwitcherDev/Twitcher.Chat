﻿namespace Twitcher.Chat.Client.Events;

internal class OnChatJoinedArgs : EventArgs, IChatActionArgs
{
    public string Channel { get; }

    internal OnChatJoinedArgs(string channel)
    {
        Channel = channel;
    }
}
