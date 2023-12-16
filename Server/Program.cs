using Server;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            
            CustomServerProg serverProgCustom = new CustomServerProg();
            await Task.Run(() => serverProgCustom.HandleClientAsync(""));
            //Task task = serverProgCustom.HandleClientAsync("");
            //await Task.WhenAll(task, Task.Delay(1000));
            
            
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
