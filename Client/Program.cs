using Server;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            SentMessage sentMessage = new SentMessage();
            Console.WriteLine("Введите ваше имя: ");


            string nameClient = Console.ReadLine();
            await Task.Run(() => sentMessage.SentMessageClient(nameClient, "127.0.0.1"));


        }
    }
}
