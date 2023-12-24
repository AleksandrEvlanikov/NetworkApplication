using System.Net.Sockets;
using System.Net;
using System.Text;
using NetMQServer;
using NetMQ.Sockets;
using NetMQ;

namespace NetMQClient
{
    public class SentMessage
    {
        private int id = 1;
        private RequestSocket requestSocket;
        public SentMessage()
        {
            requestSocket = new RequestSocket("@tcp://*:12345");

        }
        public async Task SentMessageClient(string From)
        {

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
                    countMessage = id
                };

                string json = message.SerializeMessageToJson();
                Console.WriteLine($"Отправка сообщения на сервер: {json}");

                requestSocket.SendFrame(json);

                string messagFromServer = requestSocket.ReceiveFrameString();
                Console.WriteLine($"Ответ от сервера: {messagFromServer}");

                id++;

            }

        }

        public void ExitClient()
        {
            requestSocket.Close();
        }

        public bool IsUdpClientClosed()
        {
            try
            {
                return requestSocket == null;
            }
            catch (ObjectDisposedException)
            {
                return true;
            }
        }
    }
}
