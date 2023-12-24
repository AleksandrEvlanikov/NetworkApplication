using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using NetMQ.Sockets;
using NetMQ;

namespace NetMQServer
{
    internal class CustomServerProg : ServerProg
    {
        protected override async Task HandleMessageAsync(Message message, ResponseSocket responseSocket)
        {

            if (message.countMessage == 2 || message.countMessage == 4)
            {
                messagesList.Add(message);
                Console.WriteLine($"Сообщение || {message}|| добавлено в лист.");
                var responseSTR1 = $"Сообщение не получено. Если хотите получить сообщение напишите | 000 | ";
                responseSocket.SendFrame(responseSTR1);
            }
            else if (message.Text == "000")
            {

                var responseSTR3 = $"Пропущеное сообщения: {string.Join(" =и> ", messagesList.Select(x => x.Text))}";
                responseSocket.SendFrame(responseSTR3);
            }
            else
            {
                await base.HandleMessageAsync(message, responseSocket);
            }

            //await Task.WhenAll();

            await Task.Delay(1500);
        }
    }
}
