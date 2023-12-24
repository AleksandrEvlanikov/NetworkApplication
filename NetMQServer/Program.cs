namespace NetMQServer
{
    internal class Program
    {
        static async Task Main(string[] args)
        {

            CustomServerProg serverProgCustom = new CustomServerProg();

            //await Task.Run(() => serverProgCustom.HandleClientAsync(""));
            await serverProgCustom.HandleClientAsync("");
        }
    }
}
