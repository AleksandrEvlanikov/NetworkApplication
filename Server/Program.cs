using Server;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ServerProg serverProg = new ServerProg();
            serverProg.Server("Hello");
        }

        //public void task1()
        //{
        //    Message msg = new Message() { Text = "Hello", DateTime = DateTime.Now, NicknameFrom = "Artem", NicknameTo = "All" };
        //    string json = msg.SerializeMessageToJson();
        //    Console.WriteLine(json);
        //    Message? msgDeserialized = Message.DeserializeFromJson(json);
        //}





    }
}
