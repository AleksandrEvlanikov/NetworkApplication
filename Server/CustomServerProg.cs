using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    internal class CustomServerProg : ServerProg
    {
        protected override void HandleMessage(Message message, IPEndPoint remoteEndPoint)
        {
            messagesList.Add(message);
            Console.WriteLine($"Сообщение || {message} || добавлено в лист.");
            base.HandleMessage(message, remoteEndPoint);
        }
    }

}

