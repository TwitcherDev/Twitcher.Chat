using Microsoft.Extensions.Logging;
using System.Net.Security;
using System.Net.Sockets;
using System.Text;

namespace Twitcher.Chat.Irc;

internal class TwitcherIrcChatClient : ITwitchChat
{
    private const string SERVER = "irc.chat.twitch.tv";
    private const int PORT = 6667;
    private const int SSL_PORT = 6697;

    private TcpClient? _tcpClient;
    private Stream? _stream;
    private StreamReader? _reader;
    private StreamWriter? _writer;

    private int _reconnectInterval;
    private bool _isConnected;
    private bool _isShouldBeConnected;
    private bool _isReconnecting;
    private Task<bool>? _reconnectingTask;

    private readonly string _username;
    private readonly string _token;
    private readonly ITwitchChatOptions _options;
    private readonly ILogger? _logger;

    private readonly List<TwitcherIrcChannel> _channels;
    private IGlobalUserStateTags? _globalUserTags;

    public bool IsConnected => _isConnected;
    public IReadOnlyCollection<ITwitchChatChannel> Channels => _channels.AsReadOnly();
    public IGlobalUserStateTags? GlobalUserState => _globalUserTags;

    public event EventHandler? OnConnected;
    public event EventHandler? OnDisconnected;
    public event EventHandler? OnReconnecting;

    public event EventHandler? OnReconnectReceived;

    public event EventHandler<IChatActionArgs>? OnChatJoined;
    public event EventHandler<IChatActionArgs>? OnChatLeft;

    public event EventHandler<IChatMessageReceivedArgs>? OnMessageReceived;
    public event EventHandler<IChatUserActionArgs>? OnUserJoined;
    public event EventHandler<IChatUserActionArgs>? OnUserLeft;

    public event EventHandler<IChatNoticeReceivedArgs>? OnNoticeReceived;
    public event EventHandler<IChatHostTargetReceivedArgs>? OnHostTargetReceived;
    public event EventHandler<IChatClearedArgs>? OnChatCleared;
    public event EventHandler<IChatMessageClearedArgs>? OnMessageCleared;
    public event EventHandler<IWhisperReceivedArgs>? OnWhisperReceived;

    public event EventHandler<IGlobalUserStateReceivedArgs>? OnGlobalUserStateReceived;
    public event EventHandler<IRoomStateReceivedArgs>? OnRoomStateReceived;
    public event EventHandler<IUserStateReceivedArgs>? OnUserStateReceived;

    public event EventHandler<IUserNoticeReceivedArgs<IUserNoticeReceivedTags>>? OnUserNoticeReceived;
    public event EventHandler<IUserNoticeReceivedArgs<IUserNoticeSubReceivedTags>>? OnSubscribeReceived;
    public event EventHandler<IUserNoticeReceivedArgs<IUserNoticeSubGiftReceivedTags>>? OnSubscribeGiftReceived;
    public event EventHandler<IUserNoticeReceivedArgs<IUserNoticeGiftPaidUpgradeReceivedTags>>? OnGiftPaidUpgradeReceived;
    public event EventHandler<IUserNoticeReceivedArgs<IUserNoticeReceivedTags>>? OnRewardGiftReceived;
    public event EventHandler<IUserNoticeReceivedArgs<IUserNoticeRaidReceivedTags>>? OnRaidReceived;
    public event EventHandler<IUserNoticeReceivedArgs<IUserNoticeReceivedTags>>? OnUnraidReceived;
    public event EventHandler<IUserNoticeReceivedArgs<IUserNoticeRitualReceivedTags>>? OnRitualReceived;
    public event EventHandler<IUserNoticeReceivedArgs<IUserNoticeBitsBadgeTierReceivedTags>>? OnBitsBadgeTierReceived;

    internal TwitcherIrcChatClient(string username, string token, ITwitchChatOptions? options = null, ILogger? logger = null)
    {
        _username = username.ToLower();
        _token = token.StartsWith("oauth:") ? token : "oauth:" + token;
        _options = options ?? new TwitcherChatOptions();
        _channels = new List<TwitcherIrcChannel>();
        _logger = logger;

        _reconnectInterval = 1000;
        _isConnected = false;
        _isShouldBeConnected = false;
        _isReconnecting = false;
    }

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

    public Task<bool> Reconnect()
    {
        _isShouldBeConnected = true;
        if (_isReconnecting)
            return _reconnectingTask!;
        if (_tcpClient != null)
            Disconnecting();
        _isReconnecting = true;
        _logger?.LogInformation("Reconnecting");
        OnReconnecting?.Invoke(this, EventArgs.Empty);
        return _reconnectingTask = Reconnecting();
    }

    public Task<bool> Disconnect()
    {
        _isShouldBeConnected = false;
        Disconnecting();
        return Task.FromResult(true);
    }

    private async Task<bool> Reconnecting()
    {
        while (true)
        {
            try
            {
                _tcpClient = new TcpClient();
                await _tcpClient.ConnectAsync(SERVER, _options.UseSsl ? SSL_PORT : PORT);
                _stream = _tcpClient.GetStream();
                if (_options.UseSsl)
                {
                    _stream = new SslStream(_stream, true);
                    await ((SslStream)_stream).AuthenticateAsClientAsync(SERVER);
                }
                _reader = new StreamReader(_stream);
                _writer = new StreamWriter(_stream);
            }
            catch (SocketException e)
            {
                _logger?.LogError(e, "Connection error");
                Disconnecting();
                if (!_options.AutoReconnect)
                {
                    _isReconnecting = false;
                    _reconnectingTask = null;
                    return false;
                }
                var reconnectIn = _reconnectInterval + Random.Shared.Next(500);
                _logger?.LogDebug("Try to reconnect in {time} seconds", reconnectIn / 1000d);
                await Task.Delay(reconnectIn);
                _reconnectInterval *= 2;
                _logger?.LogInformation("Reconnecting");
                OnReconnecting?.Invoke(this, EventArgs.Empty);
                continue;
            }

            _ = Task.Run(Receiving);

            Send("PASS " + _token + "\nNICK " + _username);

            var caps = new List<string>();
            if (_options.CommandsCapability)
                caps.Add("twitch.tv/commands");
            if (_options.MembershipCapability)
                caps.Add("twitch.tv/membership");
            if (_options.TagsCapability)
                caps.Add("twitch.tv/tags");
            if (caps.Any())
                Send("CAP REQ :" + string.Join(' ', caps));

            _reconnectInterval = 1000;
            _isReconnecting = false;
            _reconnectingTask = null;
            return true;
        }
    }

    private void Disconnecting()
    {
        if (_tcpClient == null)
            return;
        if (_writer != null)
        {
            _writer.Dispose();
            _writer = null;
        }
        if (_reader != null)
        {
            _reader.Dispose();
            _reader = null;
        }
        if (_stream != null)
        {
            _stream.Dispose();
            _stream = null;
        }
        if (_tcpClient.Connected)
            _tcpClient.Close();
        _tcpClient.Dispose();
        _tcpClient = null;
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
                message = _reader?.ReadLine();
            }
            catch (IOException)
            {
                message = null;
            }
            if (message == null)
            {
                if (_isShouldBeConnected && !_isReconnecting)
                {
                    Disconnecting();
                    if (_options.AutoReconnect)
                    {
                        _isReconnecting = true;
                        _logger?.LogInformation("Reconnecting");
                        OnReconnecting?.Invoke(this, EventArgs.Empty);
                        _reconnectingTask = Reconnecting();
                    }
                }
                break;
            }
            try
            {
                MessageReceived(message);
            }
            catch (Exception e)
            {
                _logger?.LogError(e, "Message processing exception ({raw})", message);
            }
        }
    }

    private void Send(string text)
    {
        if (_writer == null)
            return;
        _writer.WriteLine(text);
        _writer.Flush();
        _logger?.LogTrace("Message sended: {text}", text);
    }

    private void MessageReceived(string text)
    {
        _logger?.LogTrace("Message received: {text}", text);
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
                if (channel != null)
                    channel.AddMembers(message.Params!.Split(' '));
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
                    channel.HostTargetUsername = message.SeparatedParams![0];
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
                {
                    Disconnecting();
                    if (_options.AutoReconnect)
                    {
                        _isReconnecting = true;
                        _logger?.LogInformation("Reconnecting");
                        OnReconnecting?.Invoke(this, EventArgs.Empty);
                        _reconnectingTask = Reconnecting();
                    }
                }
                return;

            default:
                _logger?.LogWarning("Unexpected command: {commang}", message.Command);
                return;
        }
    }

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

    public void LeaveChannel(string username)
    {
        var name = username.ToLower();
        var channel = _channels.FirstOrDefault(c => c.Username == name);
        if (channel == default)
            return;

        _channels.Remove(channel);
        Send("PART #" + name);
    }

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

    public void SendMessage(string channel, string message)
    {
        Send("PRIVMSG #" + channel + " :" + message);
    }

    public void SendMessage(string channel, string message, Guid replyParentMessageId)
    {
        Send("@reply-parent-msg-id=" + replyParentMessageId + " PRIVMSG #" + channel + " :" + message);
    }
}