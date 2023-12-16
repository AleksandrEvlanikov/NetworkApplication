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
        protected UdpClient udpClient = new UdpClient(12345);
        protected IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, 0);
        private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        protected List<Message> messagesList = new List<Message>();
        public async Task HandleClientAsync(string name)
        {
            Console.WriteLine("Сервер ждет сообщение от клиента");

            while (!cancellationTokenSource.IsCancellationRequested)
            {
                try
                {


                    //UdpReceiveResult receiveResult = await udpClient.ReceiveAsync();
                    //byte[] buffer = receiveResult.Buffer;
                    byte[] buffer = udpClient.Receive(ref iPEndPoint);

                    var messageText = Encoding.UTF8.GetString(buffer);

                    Message message = Message.DeserializeFromJson(messageText);
                    message.Print();
                    await HandleMessageAsync(message, iPEndPoint);

                }
                catch (SocketException ex)
                {
                    Console.WriteLine($"SocketException: {ex.SocketErrorCode}");
                    Console.WriteLine($"Message: {ex.Message}");
                }
            }
        }
        protected virtual async Task HandleMessageAsync(Message message, IPEndPoint remoteEndPoint)
        {
            if (message.Text == "Exit")
            {
                byte[] responseBytesExit = Encoding.UTF8.GetBytes($"Ваше сообщение | {message.Text} | запрещено, сервер выключается. ");
                await udpClient.SendAsync(responseBytesExit, responseBytesExit.Length, remoteEndPoint);
                cancellationTokenSource.Cancel();
                udpClient.Close();

            }
            else
            {
                byte[] responseBytes = Encoding.UTF8.GetBytes($"Ваше сообщение | {message.Text} | доставлено");
                await udpClient.SendAsync(responseBytes, responseBytes.Length, remoteEndPoint);
                //await Task.Delay(3000);

            }
        }
    }
}
