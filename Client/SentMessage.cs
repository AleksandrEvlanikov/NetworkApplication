using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Server;

namespace Client
{
    public class SentMessage
    {
        private int id = 1;
        public async Task SentMessageClient(string From, string ip)
        {

            UdpClient udpClient = new UdpClient();
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(ip), 12345);


            while (true)
            {
                string messageText;
                do
                {
                    
                    //Console.Clear();
                    Console.WriteLine("Введите сообщение.");
                    messageText = Console.ReadLine();
                    if (messageText != null)
                    {
                        Console.WriteLine("Сообщение отправлено.");
                    }
                    await Task.Delay(2000);

                }
                while (string.IsNullOrEmpty(messageText));

                Message message = new Message() 
                { 
                    Text = messageText,
                    NicknameFrom = From,
                    NicknameTo = "Server",
                    DateTime = DateTime.Now,
                    countMessage = id };

                string json = message.SerializeMessageToJson();

                byte[] data = Encoding.UTF8.GetBytes(json);
                await udpClient.SendAsync(data, data.Length, iPEndPoint);
                 id++;

                //UdpReceiveResult receiveResult = await udpClient.ReceiveAsync();
                //byte[] buffer = receiveResult.Buffer;

                byte[] responseBytes = udpClient.Receive(ref iPEndPoint);
                string responseMessage = Encoding.UTF8.GetString(responseBytes);
                Console.WriteLine($"Ответ от сервера: {responseMessage}");
            }

        }
    }
}
