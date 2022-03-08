using System.Net.Sockets;
using Twitcher.Client.Events;

namespace Twitcher.Client
{
    public class TwitcherChat
    {
        private const string SERVER = "irc.chat.twitch.tv";
        private const int PORT = 6667;

        private readonly string _username;
        private readonly string _token;
        private readonly List<TwitcherChatChannel> _channels;

        private TcpClient? _tcpClient;
        private NetworkStream? _stream;
        private StreamReader? _reader;
        private StreamWriter? _writer;
        
        private bool _isConnected;
        private int _reconnectInterval;
        private bool _isReconnecting;
        private bool _isJoining;

        public event EventHandler<OnChatErrorArgs>? OnError;
        public event EventHandler<EventArgs>? OnConnected;
        public event EventHandler<EventArgs>? OnReconnected;

        public event EventHandler<OnChatMessageReceivedArgs>? OnMessageReceived;

        public TwitcherChat(string botName, string oauth)
        {
            _username = botName.ToLower();
            if (oauth.StartsWith("oauth:"))
                _token = oauth;
            else
                _token = "oauth:" + oauth;
            _channels = new();

            _isConnected = false;
            _reconnectInterval = 1000;
            _isReconnecting = false;
            _isJoining = false;
        }

        public void Connect()
        {
            if (_tcpClient != null)
                return;
            _ = Reconnecting();
        }

        public void Reconnect()
        {
            if (_tcpClient == null || _isReconnecting)
                return;
            _ = Reconnecting();
        }

        public void Disconnect()
        {
            if (_tcpClient == null)
                return;
            Disconnecting();
        }

        private async Task Reconnecting()
        {
            if (_tcpClient != null || _isReconnecting)
                return;

            _isReconnecting = true;
            while (true)
            {
                try
                {
                    _tcpClient = new TcpClient(SERVER, PORT);
                    _stream = _tcpClient.GetStream();
                    _reader = new StreamReader(_stream);
                    _writer = new StreamWriter(_stream);
                }
                catch (SocketException)
                {
                    Disconnecting();
                    await Task.Delay(_reconnectInterval + Random.Shared.Next(500));
                    _reconnectInterval *= 2;
                    continue;
                }

                _ = Task.Run(Receiving);
                _writer.WriteLine("PASS " + _token);
                _writer.WriteLine("NICK " + _username);
                _writer.Flush();
                _writer.WriteLine("CAP REQ :twitch.tv/tags");
                _writer.Flush();
                if (_isConnected)
                    OnReconnected?.Invoke(this, EventArgs.Empty);
                else
                {
                    _isConnected = true;
                    OnConnected?.Invoke(this, EventArgs.Empty);
                }
                _reconnectInterval = 1000;
                _isReconnecting = false;
                break;
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
        }

        private void Receiving()
        {
            if (_reader == default)
                return;
            while (true)
            {
                var message = _reader.ReadLine();
                if (message == null)
                {
                    _ = Reconnecting();
                    break;
                }
                MessageReceived(message);
            }
        }

        private void MessageReceived(string text)
        {
            Console.WriteLine(text);
            if (text.StartsWith("PING"))
            {
                if (_writer != null)
                {
                    _writer.WriteLine("PONG " + text[5..]);
                    _writer.Flush();
                }
                return;
            }
            var split = text.Split(' ');
            if (split.Length >= 2)
            {
                if (split[1] == "001")
                    JoinNext();
                else if (split[1] == "JOIN" && split.Length >=  3)
                {
                    var username = split[2][1..];
                    var channel = _channels.FirstOrDefault(c => c.Channel == username);
                    if (channel != default)
                        channel.IsConnected = true;
                    _isJoining = false;
                    JoinNext();
                }
                else if (split[1] == "PRIVMSG" && split.Length >= 4)
                {
                    OnMessageReceived?.Invoke(this, new OnChatMessageReceivedArgs(
                        split[2][1..],
                        split[0][1..(split[0].IndexOf('!') - 1)],
                        text[(text[1..].IndexOf(':') + 2)..]));
                }
            }
        }
        
        private void JoinNext()
        {
            if (_isJoining)
                return;
            var channel = _channels.FirstOrDefault(c => !c.IsConnected);
            if (channel == null || _writer == null)
                return;

            _isJoining = true;
            _writer.WriteLine("JOIN #" + channel.Channel);
            _writer.Flush();
        }

        public void AddChannel(string username)
        {
            var name = username.ToLower();
            var channel = _channels.FirstOrDefault(c => c.Channel == name);
            if (channel != default)
                return;
            _channels.Add(new TwitcherChatChannel(name));
            if (_writer != null)
                JoinNext();
        }
        
        public void RemoveChannel(string username)
        {
            var name = username.ToLower();
            var channel = _channels.FirstOrDefault(c => c.Channel == name);
            if (channel == default)
                return;
            _channels.Remove(channel);
            if (channel.IsConnected && _writer != null)
            {
                _writer.WriteLine("PART #" + channel.Channel);
                _writer.Flush();
            }
        }

        public void SendMessage(string channel, string message)
        {
            if (_writer == null)
                return;
            _writer.WriteLine("PRIVMSG #" + channel + " :" + message);
            _writer.Flush();
        }
    }
}