namespace Twitcher.Chat.Irc.Models;

internal class ChatMessageEmotePosition : IChatMessageEmotePosition
{
    public int StartPosition { get; }
    public int EndPosition { get; }

    internal ChatMessageEmotePosition(int start, int end)
    {
        StartPosition = start;
        EndPosition = end;
    }
}
