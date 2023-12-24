namespace NetMQClient
{
    internal class Program
    {

        static async Task Main(string[] args)
        {
            SentMessage sentMessage = new SentMessage();
            Console.WriteLine("Введите ваше имя: ");


            string nameClient = Console.ReadLine();
            await Task.Run(() => sentMessage.SentMessageClient(nameClient));


        }
        
    }
}
