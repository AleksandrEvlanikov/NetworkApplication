using Client;
using NUnit.Framework.Interfaces;
using Server;
using System.Net;

namespace TestClient
{

    public class Tests
    {
        private ServerProg server;
        private SentMessage client;
        [SetUp]
        public void Setup()
        {
            server = new ServerProg();
            client = new SentMessage();
        }

        [TearDown]
        public void TearDown()
        {
            client.ExitClient();
            server.ExitServer();
        }


        [Test]
        public void CheckIpAddressServerAndClient()
        {
            IPAddress serverIPAddress = server.GetServerIPAddress();
            IPAddress clientIPAddress = client.GetClientIPAddress();


            Assert.AreEqual(serverIPAddress, clientIPAddress);
        }

        [Test]
        public void CheckExitCliet()
        {
            string nameClient = "Вася";
            Task.Run(() => client.SentMessageClient(nameClient));
            Task.WaitAll();

            client.ExitClient();

            Assert.IsTrue(client.IsUdpClientClosed());

        }

        [Test]
        public async Task CheckOutputMessage()
        {
            string testMessage = "Hello world!";
            string testName = "Василий";
            //string exitClient = "Exit";

            using (StringWriter stringWriter = new StringWriter())
            {
                Console.SetOut(stringWriter);

                Task.Run(() => server.HandleClientAsync(""));
                var runClient = Task.Run(() => client.SentMessageClient(testName));

                using (StringReader stringReader = new StringReader(testMessage + Environment.NewLine))
                {
                    Console.SetIn(stringReader);

                    await Task.Delay(1000);

                    string consoleOutput = stringWriter.ToString().Trim();

                    Assert.IsTrue(consoleOutput.Contains("Сообщение отправлено"));

                    //using (StringReader sr = new StringReader(exitClient + Environment.NewLine))
                    //{
                    //    client.ExitClient();
                    //}
                }
            }
        }

    }
}