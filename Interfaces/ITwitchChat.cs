namespace Twitcher.Chat.Interfaces;

/// <summary>Interface containing basic functionality for working with Twitch chat. Is based on the docs <see href="https://dev.twitch.tv/docs/irc"/></summary>
public interface ITwitchChat
{
    /// <summary>Information about the channels to which the bot is joined</summary>
    IReadOnlyCollection<ITwitchChatChannel> Channels { get; }
    /// <summary>Global information about bot. <see cref="ITwitchChatOptions.CommandsCapability"/> and <see cref="ITwitchChatOptions.TagsCapability"/> required</summary>
    IGlobalUserStateTags? GlobalUserState { get; }

    /// <summary>Invoked when the bot is connected to the server</summary>
    event EventHandler? OnConnected;
    /// <summary>Invoked when the bot is disconnected from the server</summary>
    event EventHandler? OnDisconnected;
    /// <summary>Invoked when the bot tries to reconnect to the server</summary>
    event EventHandler? OnReconnecting;

    /// <summary>Invoked when the Twitch server needs to terminate the connection for maintenance reasons. If <see cref="ITwitchChatOptions.AutoReconnect"/> is <see langword="true"/>, bot tries to reconnect automatically</summary>
    event EventHandler? OnReconnectReceived;

    /// <summary>Invoked when the bot joins the chat room</summary>
    event EventHandler<IChatActionArgs>? OnChatJoined;
    /// <summary>Invoked when the bot has left the chat room. If <see cref="ITwitchChatOptions.AutoRejoin"/> is <see langword="true"/>, bot tries to rejoin automatically</summary>
    event EventHandler<IChatActionArgs>? OnChatLeft;

    /// <summary>Invoked when someone wrote in the chat room to which the bot is joined</summary>
    event EventHandler<IChatMessageReceivedArgs>? OnMessageReceived;
    /// <summary>Invoked when someone has joined the chat room. <see cref="ITwitchChatOptions.MembershipCapability"/> required</summary>
    event EventHandler<IChatUserActionArgs>? OnUserJoined;
    /// <summary>Invoked when someone has left the chat room. <see cref="ITwitchChatOptions.MembershipCapability"/> required</summary>
    event EventHandler<IChatUserActionArgs>? OnUserLeft;

    /// <summary>Indicate the outcome of an action like banning a user. <see cref="ITwitchChatOptions.CommandsCapability"/> required</summary>
    event EventHandler<IChatNoticeReceivedArgs>? OnNoticeReceived;
    /// <summary>Invoked when a channel starts or stops hosting viewers from another channel. <see cref="ITwitchChatOptions.CommandsCapability"/> required</summary>
    event EventHandler<IChatHostTargetReceivedArgs>? OnHostTargetReceived;
    /// <summary>Invoked when the bot or moderator removes all messages from the chat room or removes all messages for the specified user. <see cref="ITwitchChatOptions.CommandsCapability"/> required</summary>
    event EventHandler<IChatClearedArgs>? OnChatCleared;
    /// <summary>Invoked when the bot or moderator removes a single message from the chat room. <see cref="ITwitchChatOptions.CommandsCapability"/> required</summary>
    event EventHandler<IChatMessageClearedArgs>? OnMessageCleared;
    /// <summary>Invoked when a user sends a whisper message. <see cref="ITwitchChatOptions.CommandsCapability"/> required</summary>
    event EventHandler<IWhisperReceivedArgs>? OnWhisperReceived;

    /// <summary>Invoked when a <see cref="GlobalUserState"/> has been changed. <see cref="ITwitchChatOptions.CommandsCapability"/> required</summary>
    event EventHandler<IGlobalUserStateReceivedArgs>? OnGlobalUserStateReceived;
    /// <summary>Invoked when a <see cref="ITwitchChatChannel.RoomStateTags"/> has been changed. <see cref="ITwitchChatOptions.CommandsCapability"/> required</summary>
    event EventHandler<IRoomStateReceivedArgs>? OnRoomStateReceived;
    /// <summary>Invoked when a <see cref="ITwitchChatChannel.UserStateTags"/> has been changed. <see cref="ITwitchChatOptions.CommandsCapability"/> required</summary>
    event EventHandler<IUserStateReceivedArgs>? OnUserStateReceived;

    /// <summary>Invoked when events like someone subscribing to the channel occurs. <see cref="ITwitchChatOptions.CommandsCapability"/> required</summary>
    event EventHandler<IUserNoticeReceivedArgs<IUserNoticeReceivedTags>>? OnUserNoticeReceived;
    /// <summary>Invoked when someone subscribes to the channel. <see cref="ITwitchChatOptions.CommandsCapability"/> and <see cref="ITwitchChatOptions.TagsCapability"/> required</summary>
    event EventHandler<IUserNoticeReceivedArgs<IUserNoticeSubReceivedTags>>? OnSubscribeReceived;
    /// <summary>Invoked when someone gives a subscription to the channel. <see cref="ITwitchChatOptions.CommandsCapability"/> and <see cref="ITwitchChatOptions.TagsCapability"/> required</summary>
    event EventHandler<IUserNoticeReceivedArgs<IUserNoticeSubGiftReceivedTags>>? OnSubscribeGiftReceived;
    /// <summary>Invoked when an event 'giftpaidupgrade' occurs. <see cref="ITwitchChatOptions.CommandsCapability"/> and <see cref="ITwitchChatOptions.TagsCapability"/> required</summary>
    event EventHandler<IUserNoticeReceivedArgs<IUserNoticeGiftPaidUpgradeReceivedTags>>? OnGiftPaidUpgradeReceived;
    /// <summary>Invoked when an event 'rewardgift' occurs. <see cref="ITwitchChatOptions.CommandsCapability"/> and <see cref="ITwitchChatOptions.TagsCapability"/> required</summary>
    event EventHandler<IUserNoticeReceivedArgs<IUserNoticeReceivedTags>>? OnRewardGiftReceived;
    /// <summary>Invoked when someone raids a channel. <see cref="ITwitchChatOptions.CommandsCapability"/> and <see cref="ITwitchChatOptions.TagsCapability"/> required</summary>
    event EventHandler<IUserNoticeReceivedArgs<IUserNoticeRaidReceivedTags>>? OnRaidReceived;
    /// <summary>Invoked when someone unraids a channel. <see cref="ITwitchChatOptions.CommandsCapability"/> and <see cref="ITwitchChatOptions.TagsCapability"/> required</summary>
    event EventHandler<IUserNoticeReceivedArgs<IUserNoticeReceivedTags>>? OnUnraidReceived;
    /// <summary>Invoked when someone performs a ritual, such as writing the first message in a chat room. <see cref="ITwitchChatOptions.CommandsCapability"/> and <see cref="ITwitchChatOptions.TagsCapability"/> required</summary>
    event EventHandler<IUserNoticeReceivedArgs<IUserNoticeRitualReceivedTags>>? OnRitualReceived;
    /// <summary>Invoked when someone has earned the tier of the bits badge. <see cref="ITwitchChatOptions.CommandsCapability"/> and <see cref="ITwitchChatOptions.TagsCapability"/> required</summary>
    event EventHandler<IUserNoticeReceivedArgs<IUserNoticeBitsBadgeTierReceivedTags>>? OnBitsBadgeTierReceived;

    /// <summary>Connects to the server</summary>
    /// <returns>Connection task, returning <see langword="true"/> if successful, otherwise <see langword="false"/>. If <see cref="ITwitchChatOptions.AutoReconnect"/> is <see langword="true"/> bot will try to connect forever and never return <see langword="fakse"/></returns>
    Task<bool> Connect();
    /// <summary>Reconnects to the server</summary>
    /// <returns>Connection task, returning <see langword="true"/> if successful, otherwise <see langword="false"/>. If <see cref="ITwitchChatOptions.AutoReconnect"/> is <see langword="true"/> bot will try to connect forever and never return <see langword="fakse"/></returns>
    Task<bool> Reconnect();
    /// <summary>Disconnects from the server</summary>
    /// <returns>Disconnection task, returning <see langword="true"/> if successful, otherwise <see langword="false"/></returns>
    Task<bool> Disconnect();

    /// <summary>Join the chat room</summary>
    /// <param name="username">The username of the user whose chat you want to join</param>
    void JoinChannel(string username);
    /// <summary>Join the chat rooms</summary>
    /// <param name="usernames">The usernames of the users whose chats you want to join</param>
    void JoinChannels(IEnumerable<string> usernames);
    /// <summary>Leave the chat room</summary>
    /// <param name="username">The username of the user whose chat you want to leave</param>
    void LeaveChannel(string username);
    /// <summary>Leave the chat rooms</summary>
    /// <param name="usernames">The usernames of the users whose chats you want to leave</param>
    void LeaveChannels(IEnumerable<string> usernames);

    /// <summary>Send a message to the chat room</summary>
    /// <param name="channel">Username of the user to whom you want to send a message in the chat room</param>
    /// <param name="message">Message text</param>
    void SendMessage(string channel, string message);
    /// <summary>Send a message to the chat room with a reply to another message</summary>
    /// <param name="channel">Username of the user to whom you want to send a message in the chat room</param>
    /// <param name="message">Message text</param>
    /// <param name="replyParentMessageId">The id of the message you want to reply to</param>
    void SendMessage(string channel, string message, Guid replyParentMessageId);

}
