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
    public class ServerProg
    {

        protected ResponseSocket responseSocket;
        private CancellationTokenSource cancellationTokenSource;
        protected List<Message> messagesList;
        protected List<Task> taskList;


        public ServerProg()
        {
            responseSocket = new ResponseSocket(">tcp://localhost:12345");
            cancellationTokenSource = new CancellationTokenSource();
            messagesList = new List<Message>();
            taskList = new List<Task>();
        }

        public async Task HandleClientAsync(string name)
        {
            Console.WriteLine("Сервер ждет сообщение от клиента");

            while (!cancellationTokenSource.IsCancellationRequested)
            {
                try
                {
                    var messageText = responseSocket.ReceiveFrameString();
                    Console.WriteLine($"Получено сообщение от клиента: {messageText}");

                    Message message = Message.DeserializeFromJson(messageText);
                    message.Print();
                    await HandleMessageAsync(message, responseSocket); 

                }
                catch (SocketException ex)
                {
                    Console.WriteLine($"SocketException: {ex.SocketErrorCode}");
                    Console.WriteLine($"Message: {ex.Message}");
                }
            }
        }
        protected virtual async Task HandleMessageAsync(Message message, ResponseSocket responseSocket)
        {
            if (message.Text == "Exit")
            {
                var responseExit = $"Ваше сообщение | {message.Text} | запрещено, сервер выключается. ";
                responseSocket.SendFrame(responseExit);
                cancellationTokenSource.Cancel();
                ExitServer();

            }
            else
            {
                var response = $"Ваше сообщение | {message.Text} | доставлено";
                responseSocket.SendFrame(response);
                //await Task.Delay(3000);

            }
        }

        public void ExitServer()
        {
            responseSocket.Close();
        }
    }
}
