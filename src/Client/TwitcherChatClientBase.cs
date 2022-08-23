using Microsoft.Extensions.Logging;
using System.Text;

namespace Twitcher.Chat.Client;

/// <summary>A base abstract Twitch chat client with no implementation of connection and communication</summary>
public abstract class TwitcherChatClientBase : ITwitchChat
{
    private int _reconnectInterval;
    private bool _isConnected;
    private bool _isShouldBeConnected;
    private bool _isReconnecting;
    private Task<bool>? _reconnectingTask;

    private readonly string _username;
    private readonly string _token;
    /// <summary>Twitch chat options</summary>
    protected readonly ITwitchChatOptions _options;
    /// <summary><see cref="ILogger"/> for logging</summary>
    protected readonly ILogger? _logger;

    private readonly List<TwitcherIrcChannel> _channels;
    private IGlobalUserStateTags? _globalUserTags;

    /// <inheritdoc/>
    public bool IsConnected => _isConnected;
    /// <inheritdoc/>
    public IReadOnlyCollection<ITwitchChatChannel> Channels => _channels.AsReadOnly();
    /// <inheritdoc/>
    public IGlobalUserStateTags? GlobalUserState => _globalUserTags;

    /// <inheritdoc/>
    public event EventHandler? OnConnected;
    /// <inheritdoc/>
    public event EventHandler? OnDisconnected;
    /// <inheritdoc/>
    public event EventHandler? OnReconnecting;

    /// <inheritdoc/>
    public event EventHandler? OnReconnectReceived;

    /// <inheritdoc/>
    public event EventHandler<IChatActionArgs>? OnChatJoined;
    /// <inheritdoc/>
    public event EventHandler<IChatActionArgs>? OnChatLeft;

    /// <inheritdoc/>
    public event EventHandler<IChatMessageReceivedArgs>? OnMessageReceived;
    /// <inheritdoc/>
    public event EventHandler<IChatUserActionArgs>? OnUserJoined;
    /// <inheritdoc/>
    public event EventHandler<IChatUserActionArgs>? OnUserLeft;

    /// <inheritdoc/>
    public event EventHandler<IChatNoticeReceivedArgs>? OnNoticeReceived;
    /// <inheritdoc/>
    public event EventHandler<IChatHostTargetReceivedArgs>? OnHostTargetReceived;
    /// <inheritdoc/>
    public event EventHandler<IChatClearedArgs>? OnChatCleared;
    /// <inheritdoc/>
    public event EventHandler<IChatMessageClearedArgs>? OnMessageCleared;
    /// <inheritdoc/>
    public event EventHandler<IWhisperReceivedArgs>? OnWhisperReceived;

    /// <inheritdoc/>
    public event EventHandler<IGlobalUserStateReceivedArgs>? OnGlobalUserStateReceived;
    /// <inheritdoc/>
    public event EventHandler<IRoomStateReceivedArgs>? OnRoomStateReceived;
    /// <inheritdoc/>
    public event EventHandler<IUserStateReceivedArgs>? OnUserStateReceived;

    /// <inheritdoc/>
    public event EventHandler<IUserNoticeReceivedArgs<IUserNoticeReceivedTags>>? OnUserNoticeReceived;
    /// <inheritdoc/>
    public event EventHandler<IUserNoticeReceivedArgs<IUserNoticeSubReceivedTags>>? OnSubscribeReceived;
    /// <inheritdoc/>
    public event EventHandler<IUserNoticeReceivedArgs<IUserNoticeSubGiftReceivedTags>>? OnSubscribeGiftReceived;
    /// <inheritdoc/>
    public event EventHandler<IUserNoticeReceivedArgs<IUserNoticeGiftPaidUpgradeReceivedTags>>? OnGiftPaidUpgradeReceived;
    /// <inheritdoc/>
    public event EventHandler<IUserNoticeReceivedArgs<IUserNoticeReceivedTags>>? OnRewardGiftReceived;
    /// <inheritdoc/>
    public event EventHandler<IUserNoticeReceivedArgs<IUserNoticeRaidReceivedTags>>? OnRaidReceived;
    /// <inheritdoc/>
    public event EventHandler<IUserNoticeReceivedArgs<IUserNoticeReceivedTags>>? OnUnraidReceived;
    /// <inheritdoc/>
    public event EventHandler<IUserNoticeReceivedArgs<IUserNoticeRitualReceivedTags>>? OnRitualReceived;
    /// <inheritdoc/>
    public event EventHandler<IUserNoticeReceivedArgs<IUserNoticeBitsBadgeTierReceivedTags>>? OnBitsBadgeTierReceived;

    /// <param name="username">Username of the <paramref name="token"/> owner</param>
    /// <param name="token">Access token for authentication</param>
    /// <param name="options">Twitch chat options</param>
    /// <param name="logger"><see cref="ILogger"/> for logging</param>
    /// <exception cref="ArgumentNullException"></exception>
    public TwitcherChatClientBase(string username, string token, ITwitchChatOptions options, ILogger? logger = null)
    {
        ArgumentNullException.ThrowIfNull(username);
        ArgumentNullException.ThrowIfNull(token);

        _username = username.ToLower();
        _token = token.StartsWith("oauth:") ? token : "oauth:" + token;
        _options = options ?? throw new ArgumentNullException(nameof(options));
        _channels = new List<TwitcherIrcChannel>();
        _logger = logger;

        _reconnectInterval = 1000;
        _isConnected = false;
        _isShouldBeConnected = false;
        _isReconnecting = false;
    }
    
    /// <summary>Connect to the server</summary>
    /// <returns><see langword="true"/> if successful connected; <see langword="false"/> if connection error</returns>
    protected abstract Task<bool> ConnectCore();
    /// <summary>Disconnect from the server</summary>
    protected abstract Task DisconnectCore();
    /// <summary>Receive message</summary>
    /// <returns>Message text or <see langword="null"/> if the connection was interrupted</returns>
    protected abstract string? ReceiveMessageCore();
    /// <summary>Send message</summary>
    /// <param name="text">Message text</param>
    /// <returns><see langword="true"/> if successful sent; <see langword="false"/> if send error</returns>
    protected abstract bool SendMessageCore(string text);

    /// <inheritdoc/>
    public Task<bool> Connect()
    {
        if (_isConnected)
            throw new InvalidOperationException($"The client is already connected. Use {nameof(Reconnect)} if you want to reconnect. You can also check if the client is connected using the {nameof(IsConnected)} property");
        _isShouldBeConnected = true;
        if (_isReconnecting)
            return _reconnectingTask!;
        _isReconnecting = true;
        return _reconnectingTask = Reconnecting();
    }

    /// <inheritdoc/>
    public async Task<bool> Reconnect()
    {
        _isShouldBeConnected = true;
        if (_isReconnecting)
            return await _reconnectingTask!;
        await ReconnectCore(true);
        return await _reconnectingTask!;
    }

    private async Task ReconnectCore(bool isReconnect)
    {
        if (_isConnected)
            await Disconnecting();
        if (isReconnect)
        {
            _isReconnecting = true;
            _logger?.LogInformation("Reconnecting");
            OnReconnecting?.Invoke(this, EventArgs.Empty);
            _reconnectingTask = Reconnecting();
        }
    }

    /// <inheritdoc/>
    public async Task<bool> Disconnect()
    {
        _isShouldBeConnected = false;
        await Disconnecting();
        return true;
    }

    private async Task<bool> Reconnecting()
    {
        while (true)
        {
            bool isConnected;
            try
            {
                isConnected = await ConnectCore();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Connect exception");
                continue;
            }
            if (isConnected)
            {
                _ = Task.Run(Receiving);

                var caps = new List<string>();
                if (_options.CommandsCapability)
                    caps.Add("twitch.tv/commands");
                if (_options.MembershipCapability)
                    caps.Add("twitch.tv/membership");
                if (_options.TagsCapability)
                    caps.Add("twitch.tv/tags");
                if (caps.Any())
                    Send("CAP REQ :" + string.Join(' ', caps));

                Send("PASS " + _token + "\nNICK " + _username);

                _reconnectInterval = 1000;
                _isReconnecting = false;
                _reconnectingTask = null;
                return true;
            }
            else
            {
                await Disconnecting();
                if (!_options.AutoReconnect)
                {
                    _isReconnecting = false;
                    _reconnectingTask = null;
                    return false;
                }
                var reconnectIn = _reconnectInterval + Random.Shared.Next(500);
                _logger?.LogDebug("Try to reconnect in {time} seconds", reconnectIn / 1000d);
                await Task.Delay(reconnectIn);
                _reconnectInterval = Math.Min(_reconnectInterval * 2, 120000);
                _logger?.LogInformation("Reconnecting");
                OnReconnecting?.Invoke(this, EventArgs.Empty);
                continue;
            }
        }
    }

    private async Task Disconnecting()
    {
        try
        {
            await DisconnectCore();
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "Disconnect exception");
        }
        _isConnected = false;
        _logger?.LogInformation("Disconnected");
        OnDisconnected?.Invoke(this, EventArgs.Empty);
    }

    private void Receiving()
    {
        while (true)
        {
            string? message;
            try
            {
                message = ReceiveMessageCore();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Receive message exception");
                continue;
            }
            if (message == null)
            {
                if (_isShouldBeConnected && !_isReconnecting)
                    _ = ReconnectCore(_options.AutoReconnect);
                break;
            }
            _logger?.LogTrace("Message received: {text}", message);
            try
            {
                ProcessMessage(message);
            }
            catch (Exception e)
            {
                _logger?.LogError(e, "Message processing exception ({raw})", message);
            }
        }
    }

    private bool Send(string text)
    {
        try
        {
            var isSent = SendMessageCore(text);
            if (isSent)
                _logger?.LogTrace("Message sended: {text}", text);
            return isSent;
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, "Send message exception");
            return false;
        }
    }

    private void ProcessMessage(string text)
    {
        var message = TwitcherIrcMessage.Parse(text);

        switch (message.Command)
        {
            case "PING":
                Send("PONG " + message.Params);
                return;

            case "001":
                _isConnected = true;
                _logger?.LogInformation("Connected");
                OnConnected?.Invoke(this, EventArgs.Empty);
                foreach (var channel in _channels)
                    channel.IsJoined = false;
                if (_options.AutoRejoin)
                    Send("JOIN " + string.Join(',', _channels.Select(c => '#' + c.Username)));
                return;

            case "002":
            case "003":
            case "004":
            case "375":
            case "372":
            case "376":
            case "366":
            case "CAP":
                return;

            case "353":
            {
                if (!_options.MembershipCapability)
                    return;
                var channel = _channels.FirstOrDefault(c => c.Username == message.Channel);
                channel?.AddMembers(message.Params!.Split(' '));
            }
            return;

            case "JOIN":
            {
                var channel = _channels.FirstOrDefault(c => c.Username == message.Channel);
                if (message.Username == _username)
                {
                    if (channel != default)
                        channel.IsJoined = true;
                    _logger?.LogDebug("Joined '{channel}'", message.Channel);
                    OnChatJoined?.Invoke(this, new OnChatJoinedArgs(message.Channel!));
                }
                else if (_options.MembershipCapability)
                {
                    if (channel != default)
                        channel.AddMember(message.Username!);
                    OnUserJoined?.Invoke(this, new OnUserJoinedArgs(message.Channel!, message.Username!));
                }
            }
            return;

            case "PART":
            {
                var channel = _channels.FirstOrDefault(c => c.Username == message.Channel);
                if (message.Username == _username)
                {
                    if (channel != default)
                        channel.IsJoined = false;
                    _logger?.LogDebug("Left '{channel}'", message.Channel);
                    OnChatLeft?.Invoke(this, new OnChatLeftArgs(message.Channel!));
                    if (_options.AutoRejoin && channel != default)
                        JoinChannel(channel.Username);
                }
                else if (_options.MembershipCapability)
                {
                    if (channel != default)
                        channel.RemoveMember(message.Username!);
                    OnUserLeft?.Invoke(this, new OnUserJoinedArgs(message.Channel!, message.Username!));
                }
            }
            return;

            case "PRIVMSG":
                OnMessageReceived?.Invoke(this, new OnChatMessageReceivedArgs(message.Channel!, message.Username!, message.Params!, _options.TagsCapability ? message.Tags! : null));
                return;

            case "CLEARCHAT":
                OnChatCleared?.Invoke(this, new OnChatClearedArgs(message.Channel!, message.Params, _options.TagsCapability ? message.Tags : null));
                return;

            case "CLEARMSG":
                OnMessageCleared?.Invoke(this, new OnChatMessageClearedArgs(message.Channel!, message.Params, _options.TagsCapability ? message.Tags : null));
                return;

            case "HOSTTARGET":
            {
                var channel = _channels.FirstOrDefault(c => c.Username == message.Channel);
                if (channel != null)
                {
                    var target = message.SeparatedParams![0];
                    channel.HostTargetUsername = target != "-" ? target : null;
                }
                OnHostTargetReceived?.Invoke(this, new OnChatHostTargetReceivedArgs(message.Channel!, message.SeparatedParams![0], int.TryParse(message.SeparatedParams[1], out var viewers) ? viewers : -1));
                return;
            }

            case "GLOBALUSERSTATE":
            {
                var args = new OnGlobalUserStateReceivedArgs(_options.TagsCapability ? message.Tags! : null);
                _globalUserTags = args.Tags;
                OnGlobalUserStateReceived?.Invoke(this, args);
                return;
            }

            case "ROOMSTATE":
            {
                var channel = _channels.FirstOrDefault(c => c.Username == message.Channel!);
                if (_options.TagsCapability && channel != null)
                    if (channel.RoomStateTags == default)
                        channel.RoomStateTags = new RoomStateTags(message.Tags!);
                    else
                        ((RoomStateTags)channel.RoomStateTags).Change(message.Tags!);
                OnRoomStateReceived?.Invoke(this, new OnRoomStateReceivedArgs(message.Channel!, channel?.RoomStateTags));
                return;
            }

            case "USERSTATE":
            {
                var args = new OnUserStateReceivedArgs(message.Channel!, _options.TagsCapability ? message.Tags! : null);
                var channel = _channels.FirstOrDefault(c => c.Username == message.Channel!);
                if (channel != null)
                    channel.UserStateTags = args.Tags;
                OnUserStateReceived?.Invoke(this, args);
                return;
            }

            case "USERNOTICE":
            #region UserNotice
            {
                IUserNoticeReceivedTags? tags = null;
                if (_options.TagsCapability)
                {
                    var msgId = message.Tags!.GetValueOrDefault("msg-id");
                    switch (msgId)
                    {
                        case "sub":
                        case "resub":
                            tags = new UserNoticeSubReceivedTags(message.Tags!);
                            OnSubscribeReceived?.Invoke(this, new OnUserNoticeReceivedArgs<IUserNoticeSubReceivedTags>(message.Channel!, message.Params, (IUserNoticeSubReceivedTags)tags));
                            break;

                        case "subgift":
                            tags = new UserNoticeSubGiftReceivedTags(message.Tags!);
                            OnSubscribeGiftReceived?.Invoke(this, new OnUserNoticeReceivedArgs<IUserNoticeSubGiftReceivedTags>(message.Channel!, message.Params, (IUserNoticeSubGiftReceivedTags)tags));
                            break;

                        case "giftpaidupgrade":
                        case "anongiftpaidupgrade":
                            tags = new UserNoticeGiftPaidUpgradeReceivedTags(message.Tags!);
                            OnGiftPaidUpgradeReceived?.Invoke(this, new OnUserNoticeReceivedArgs<IUserNoticeGiftPaidUpgradeReceivedTags>(message.Channel!, message.Params, (IUserNoticeGiftPaidUpgradeReceivedTags)tags));
                            break;

                        case "rewardgift":
                            tags = new UserNoticeReceivedTags(message.Tags!);
                            OnRewardGiftReceived?.Invoke(this, new OnUserNoticeReceivedArgs<IUserNoticeReceivedTags>(message.Channel!, message.Params, tags));
                            break;

                        case "raid":
                            tags = new UserNoticeRaidReceivedTags(message.Tags!);
                            OnRaidReceived?.Invoke(this, new OnUserNoticeReceivedArgs<IUserNoticeRaidReceivedTags>(message.Channel!, message.Params, (IUserNoticeRaidReceivedTags)tags));
                            break;

                        case "unraid":
                            tags = new UserNoticeReceivedTags(message.Tags!);
                            OnUnraidReceived?.Invoke(this, new OnUserNoticeReceivedArgs<IUserNoticeReceivedTags>(message.Channel!, message.Params, tags));
                            break;

                        case "ritual":
                            tags = new UserNoticeRitualReceivedTags(message.Tags!);
                            OnRitualReceived?.Invoke(this, new OnUserNoticeReceivedArgs<IUserNoticeRitualReceivedTags>(message.Channel!, message.Params, (IUserNoticeRitualReceivedTags)tags));
                            break;

                        case "bitsbadgetier":
                            tags = new UserNoticeBitsBadgeTierReceivedTags(message.Tags!);
                            OnBitsBadgeTierReceived?.Invoke(this, new OnUserNoticeReceivedArgs<IUserNoticeBitsBadgeTierReceivedTags>(message.Channel!, message.Params, (IUserNoticeBitsBadgeTierReceivedTags)tags));
                            break;

                        default:
                            tags = new UserNoticeReceivedTags(message.Tags!);
                            break;
                    }
                }
                OnUserNoticeReceived?.Invoke(this, new OnUserNoticeReceivedArgs<IUserNoticeReceivedTags>(message.Channel!, message.Params, tags));
                return;
            }
            #endregion

            case "WHISPER":
                OnWhisperReceived?.Invoke(this, new OnWhisperReceivedArgs(message.Channel!, message.Username!, message.Params!, _options.TagsCapability ? message.Tags : null));
                return;

            case "NOTICE":
                OnNoticeReceived?.Invoke(this, new OnChatNoticeReceivedArgs(message.Channel!, message.Params!, _options.TagsCapability ? message.Tags : null));
                return;

            case "RECONNECT":
                OnReconnectReceived?.Invoke(this, EventArgs.Empty);
                if (_isShouldBeConnected && !_isReconnecting)
                    _ = ReconnectCore(_options.AutoReconnect);
                return;

            default:
                _logger?.LogWarning("Unexpected command: {commang}", message.Command);
                return;
        }
    }

    /// <inheritdoc/>
    public void JoinChannel(string username)
    {
        var name = username.ToLower();
        var channel = _channels.FirstOrDefault(c => c.Username == name);
        if (channel != null && channel.IsJoined)
            return;

        if (channel == null)
            _channels.Add(new TwitcherIrcChannel(name, _options.MembershipCapability));
        Send("JOIN #" + name);
    }

    /// <inheritdoc/>
    public void JoinChannels(IEnumerable<string> usernames)
    {
        var isAny = false;
        var names = new StringBuilder();
        foreach (var username in usernames)
        {
            var name = username.ToLower();
            var channel = _channels.FirstOrDefault(c => c.Username == name);
            if (channel != null && channel.IsJoined)
                continue;

            if (channel == null)
                _channels.Add(new TwitcherIrcChannel(name, _options.MembershipCapability));
            if (!isAny)
            {
                isAny = true;
                names.Append("#" + name);
            }
            else
                names.Append(",#" + name);
        }
        if (isAny)
            Send("JOIN " + names.ToString());
    }

    /// <inheritdoc/>
    public void LeaveChannel(string username)
    {
        var name = username.ToLower();
        var channel = _channels.FirstOrDefault(c => c.Username == name);
        if (channel == default)
            return;

        _channels.Remove(channel);
        Send("PART #" + name);
    }

    /// <inheritdoc/>
    public void LeaveChannels(IEnumerable<string> usernames)
    {
        var isAny = false;
        var names = new StringBuilder();
        foreach (var username in usernames)
        {
            var name = username.ToLower();
            var channel = _channels.FirstOrDefault(c => c.Username == name);
            if (channel == default)
                continue;

            _channels.Remove(channel);
            if (!isAny)
            {
                isAny = true;
                names.Append("#" + name);
            }
            else
                names.Append(",#" + name);
        }
        if (isAny)
            Send("PART " + names.ToString());
    }

    /// <inheritdoc/>
    public void SendMessage(string channel, string message)
    {
        Send("PRIVMSG #" + channel + " :" + message);
    }

    /// <inheritdoc/>
    public void SendMessage(string channel, string message, Guid replyParentMessageId)
    {
        Send("@reply-parent-msg-id=" + replyParentMessageId + " PRIVMSG #" + channel + " :" + message);
    }

    /// <summary>Use this field in the <see cref="Dispose(bool)"/> implementation</summary>
    protected bool disposedValue;
    /// <summary><see cref="IDisposable"/> implementation</summary>
    /// <param name="disposing">disposing</param>
    protected abstract void Dispose(bool disposing);

    /// <inheritdoc/>
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
