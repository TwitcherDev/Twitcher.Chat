using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitcher.Client
{
    public class TwitcherChatChannel
    {
        public string Channel { get; set; }
        public bool IsConnected { get; set; }

        public TwitcherChatChannel(string channel)
        {
            Channel = channel;
            IsConnected = false;
        }
    }
}
