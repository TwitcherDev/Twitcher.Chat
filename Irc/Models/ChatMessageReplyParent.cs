namespace Twitcher.Chat.Irc.Models;

internal class ChatMessageReplyParent : IChatMessageReplyParent
{
    public Guid Id { get; }
    public string UserId { get; }
    public string UserLogin { get; }
    public string DisplayName { get; }
    public string Message { get; }

    internal ChatMessageReplyParent(IReadOnlyDictionary<string, string> tags)
    {
        Id = tags.GetRequiredTagValue("reply-parent-msg-id", s => Guid.Parse(s));
        UserId = tags.GetRequiredTagValue("reply-parent-user-id");
        UserLogin = tags.GetRequiredTagValue("reply-parent-user-login");
        DisplayName = tags.GetRequiredTagValue("reply-parent-display-name");
        Message = tags.GetRequiredTagValue("reply-parent-msg-body");
    }
}
