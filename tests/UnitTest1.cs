namespace Twitcher.Chat.Tests;

[TestClass]
public class ChatClientTests
{
    [TestMethod]
    public async Task SuccessfulConnectTest()
    {
        var client = new TwitcherTestChatClient("name", "token");

        client.ConnectResult = true;
        Assert.IsTrue(await client.Connect());
        Assert.IsTrue(client.ConnectCalled);
        client.ReceiveResult = ":tmi.twitch.tv 001 <user> :Welcome, GLHF!\r\n:tmi.twitch.tv 002 <user> :Your host is tmi.twitch.tv\r\n:tmi.twitch.tv 003 <user> :This server is rather new\r\n:tmi.twitch.tv 004 <user> :-\r\n:tmi.twitch.tv 375 <user> :-\r\n:tmi.twitch.tv 372 <user> :You are in a maze of twisty passages, all alike.\r\n:tmi.twitch.tv 376 <user> :>\r\n@badge-info=;badges=;color=;display-name=<user>;emote-sets=0,300374282;user-id=12345678;user-type= :tmi.twitch.tv GLOBALUSERSTATE\r\n";
        client.SendResult = true;
        client.Receive();
        Assert.AreEqual(client.SendResults.Count, 2);
        Assert.AreEqual(client.SendResults[0], "PASS oauth:token");
        Assert.AreEqual(client.SendResults[1], "NICK name");
    }
}