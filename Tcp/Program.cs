using System.Text;

namespace Tcp
{
    internal class Program
    {

        static void Main(string[] args)
        {
            var server = new TcpListener("127.0.0.1", 8080);
            server.Start();
        }

        //static void Example(string[] args)
        //{

        //    try
        //    {
        //        Net.IPAddress ip = Net.IPAddress.Parse("127.0.0.1");
        //        Int32 port = 8080;

        //        Program.server = new Net.Sockets.TcpListener(ip, port);

        //        server.Start();

        //        byte[] bytes = new byte[256];
        //        string data = null;

        //        while (true)
        //        {
        //            Console.WriteLine("waiting for connection...");

        //            using Net.Sockets.TcpClient client = server.AcceptTcpClient();
        //            Console.WriteLine("Client Connected!");

        //            data = null;    

        //            Net.Sockets.NetworkStream stream = client.GetStream();

        //            int i;

        //            while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
        //            {
        //                data = Encoding.UTF8.GetString(bytes, 0, i);
        //                Console.WriteLine($"Received: {data}");

        //                data = data.ToUpper();

        //                byte[] msg = Encoding.UTF8.GetBytes(data);

        //                stream.Write(msg, 0, msg.Length);
        //                Console.WriteLine($"Sent: {data}");
        //            }
        //        }
        //    }
        //    catch(Net.Sockets.SocketException e)
        //    {
        //        Console.WriteLine($"SocketException: {e}");
        //    }
        //    finally
        //    {
        //        server.Stop();
        //    }

        //    Console.WriteLine("Hit enter to continue...");
        //    Console.Read();
        //}

        //Main
        //{
        //    TcpServer tcpServer = new TcpServer("127.0.0.1", 8080);

        //    //tcpServer.SendHttpRequest();
        //    //tcpServer.SendHttpRequestAsync().Wait();

        //    tcpServer.SendTcpPacket();
        //}
        
    }
}
