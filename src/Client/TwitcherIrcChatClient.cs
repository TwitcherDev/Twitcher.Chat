using Microsoft.Extensions.Logging;
using System.Net.Security;
using System.Net.Sockets;

namespace Twitcher.Chat.Client;

internal class TwitcherIrcChatClient : TwitcherChatClientBase
{
    private const string SERVER = "irc.chat.twitch.tv";
    private const int PORT = 6667;
    private const int SSL_PORT = 6697;

    private TcpClient? _tcpClient;
    private Stream? _stream;
    private StreamReader? _reader;
    private StreamWriter? _writer;

    internal TwitcherIrcChatClient(string username, string token, ITwitchChatOptions? options = null, ILogger? logger = null)
        : base(username, token, options ?? TwitcherChatOptions.Default, logger) { }

    protected override async Task<bool> ConnectCore()
    {
        try
        {
            _tcpClient = new TcpClient();
            await _tcpClient.ConnectAsync(SERVER, _options.UseSsl ? SSL_PORT : PORT);
            _stream = _tcpClient.GetStream();
            if (_options.UseSsl)
            {
                _stream = new SslStream(_stream, true);
                await((SslStream)_stream).AuthenticateAsClientAsync(SERVER);
            }
            _reader = new StreamReader(_stream);
            _writer = new StreamWriter(_stream);
            return true;
        }
        catch (SocketException ex)
        {
            _logger?.LogError(ex, "Connection error");
            return false;
        }
    }

    protected override Task DisconnectCore()
    {
        DisposeClient();
        return Task.CompletedTask;
    }

    private void DisposeClient()
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

    protected override string? ReceiveMessageCore()
    {
        try
        {
            return _reader?.ReadLine();
        }
        catch (IOException)
        {
            return null;
        }
    }

    protected override bool SendMessageCore(string text)
    {
        if (_writer == null)
            return false;
        _writer.WriteLine(text);
        try
        {
            _writer.Flush();
            return true;
        }
        catch (IOException)
        {
            return false;
        }
    }

    protected override void Dispose(bool disposing)
    {
        if (disposedValue)
            return;

        if (disposing)
            DisposeClient();

        disposedValue = true;
    }
}
