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

            if (message.countMessage == 2 || message.countMessage == 4)
            {
                messagesList.Add(message);
                Console.WriteLine($"Сообщение || {message}|| добавлено в лист.");
                byte[] responseBytes1 = Encoding.UTF8.GetBytes($"Сообщение не получено. Если хотите получить сообщение напишите | 000 | ");
                await udpClient.SendAsync(responseBytes1, responseBytes1.Length, remoteEndPoint);
            }
            else if (message.Text == "000")
            {

                byte[] responseBytes3 = Encoding.UTF8.GetBytes($"Пропущеное сообщения: {string.Join(" =и> ",messagesList.Select(x => x.Text))}");
                await udpClient.SendAsync(responseBytes3, responseBytes3.Length, remoteEndPoint);
                
            }
            else
            {
                await base.HandleMessageAsync(message, remoteEndPoint);
            }

            //await Task.WhenAll();

            await Task.Delay(1500);
        }
    }


}

