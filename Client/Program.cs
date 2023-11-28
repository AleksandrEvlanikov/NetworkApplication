using Server;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SentMessage sentMessage = new SentMessage();

            sentMessage.SentMessageClient("Aleksandr", "127.0.0.1");
        }
    }
}
