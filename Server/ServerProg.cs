using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Server
{
    public class ServerProg
    {
        public void Server(string name)
        {
            UdpClient udpClient = new UdpClient(12345);
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, 0);

            Console.WriteLine("Сервер ждет сообщение от клиента");

            while (true)
            {
                byte[] buffer = udpClient.Receive(ref iPEndPoint);
                //if (buffer == null) break;
                var messageText = Encoding.UTF8.GetString(buffer);

                Message message = Message.DeserializeFromJson(messageText);
                message.Print();

                byte[] responseBytes = Encoding.UTF8.GetBytes($"Ваше сообщение | {message.Text} | доставлено");
                udpClient.Send(responseBytes, responseBytes.Length, iPEndPoint);


            }
        }
    }
}
