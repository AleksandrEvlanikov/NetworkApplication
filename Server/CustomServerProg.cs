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
        protected override async Task HandleMessageAsync(Message message, IPEndPoint remoteEndPoint)
        {
            messagesList.Add(message);
            Console.WriteLine($"Сообщение || {message} || добавлено в лист.");

            await base.HandleMessageAsync(message, remoteEndPoint);

            await Task.WhenAll();

            await Task.Delay(1500);
        }
    }


}

