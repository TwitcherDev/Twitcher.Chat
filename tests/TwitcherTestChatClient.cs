using Microsoft.Extensions.Logging;
using Twitcher.Chat.Client;
using Twitcher.Chat.Interfaces;

namespace Twitcher.Chat.Tests;

internal class TwitcherTestChatClient : TwitcherChatClientBase
{
    internal bool ConnectCalled { get; set; }
    internal bool DisconnectCalled { get; set; }
    internal bool DisposeCalled { get; set; }
    internal bool ReceiveCalled { get; set; }
    internal bool SendCalled { get; set; }

    internal bool ConnectBalance { get; set; }
    internal bool ConnectBalanceError { get; set; }

    internal bool ConnectResult { get; set; }
    internal string? ReceiveResult { get; set; }
    internal bool SendResult { get; set; }

    internal protected TaskCompletionSource _taskSource = new();
    internal void Receive()
    {
        _taskSource.TrySetResult();
        _taskSource = new TaskCompletionSource();
    }

    internal List<string> SendResults = new();

    internal TwitcherTestChatClient(string username, string token, ITwitchChatOptions? options = null, ILogger? logger = null)
        : base(username, token, options ?? TwitcherChatOptions.Default, logger) { }

    protected override Task<bool> ConnectCore()
    {
        ConnectCalled = true;
        if (ConnectBalance == true)
            ConnectBalanceError = true;
        ConnectBalance = true;
        return Task.FromResult(ConnectResult);
    }

    protected override Task DisconnectCore()
    {
        DisconnectCalled = true;
        if (ConnectBalance == false)
            ConnectBalanceError = true;
        ConnectBalance = false;
        return Task.CompletedTask;
    }

    protected override void Dispose(bool disposing)
    {
        DisposeCalled = true;
    }

    protected override string? ReceiveMessageCore()
    {
        ReceiveCalled = true;
        _taskSource.Task.Wait();
        return ReceiveResult;
    }

    protected override bool SendMessageCore(string text)
    {
        SendCalled = true;
        SendResults.AddRange(text.Split('\n'));
        return SendResult;
    }
}
