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
        UdpClient udpClient = new UdpClient(12345);
        IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, 0);
        private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        public void HandleClient(string name)
        {
            Console.WriteLine("Сервер ждет сообщение от клиента");

            while (!cancellationTokenSource.IsCancellationRequested)
            {

                byte[] buffer = udpClient.Receive(ref iPEndPoint);
                //if (buffer == null) break;
                var messageText = Encoding.UTF8.GetString(buffer);

                Message message = Message.DeserializeFromJson(messageText);
                message.Print();

                if (message.Text == "Exit")
                {
                    byte[] responseBytesExit = Encoding.UTF8.GetBytes($"Ваше сообщение | {message.Text} | запрещено, сервер выключается. ");
                    udpClient.Send(responseBytesExit, responseBytesExit.Length, iPEndPoint);
                    cancellationTokenSource.Cancel();

                }
                else
                {
                    byte[] responseBytes = Encoding.UTF8.GetBytes($"Ваше сообщение | {message.Text} | доставлено");
                    udpClient.Send(responseBytes, responseBytes.Length, iPEndPoint);
                }




            }
        }
    }
}
