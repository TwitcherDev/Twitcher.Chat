using Twitcher.Chat.Events;

namespace Twitcher.Chat
{
    public interface ITwitchChat
    {
        event EventHandler<EventArgs>? OnConnected;
        event EventHandler<EventArgs>? OnReconnected;
        event EventHandler<IChatMessageReceivedArgs>? OnMessageReceived;

        void Connect();
        void Disconnect();

        void AddChannel(string username);
        void RemoveChannel(string username);

        void SendMessage(string channel, string message);

    }
}
