using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Tcp
{
    class TcpServer : IServer
    {
        public Uri Uri { get; private set; }
        public int Port { get; private set; }
        public bool KeepAlive { get; private set; }

        HttpRequestBuilder HttpRequestBuilder;

        public TcpServer(string uri = "127.0.0.1", int port = 8080, bool keepAlive = false)
        {
            this.Uri = new Uri($"http://{uri}");
            this.Port = port;
            this.KeepAlive = keepAlive;

            this.HttpRequestBuilder = new HttpRequestBuilder(this);
        }

        // This is a client socket
        public void SendTcpPacket()
        {
            //string message = "Can you hear me?";

            using Socket socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(Uri.Host, Port);



            while (true)
            {
                string input = Console.ReadLine();
                byte[] bytesToSend = Encoding.UTF8.GetBytes(input);

                int bytesSent = 0;
                while (bytesSent < bytesToSend.Length)
                {
                    bytesSent += socket.Send(bytesToSend, bytesSent, bytesToSend.Length - bytesSent, SocketFlags.None);
                }

                byte[] responseBytes = new byte[256];
                char[] responseChars = new char[256];

                int bytesReceived = socket.Receive(responseBytes);

                int charCount = Encoding.UTF8.GetChars(responseBytes, 0, bytesReceived, responseChars, 0);

                Console.Out.Write($"\nNODEJS: ");

                for(int i = 0; i < charCount; i++)
                {
                    Console.Out.Write(responseChars[i]);
                }
            }
        }

        public void SendHttpRequest()
        {
            byte[] requestBytes = this.HttpRequestBuilder.Serialize();

            using (Socket socket = new Socket(SocketType.Stream, ProtocolType.Tcp))
            {
                socket.Connect(Uri.Host, Port);

                int bytesSent = 0;
                while (bytesSent < requestBytes.Length)
                {
                    bytesSent += socket.Send(requestBytes, bytesSent, requestBytes.Length - bytesSent, SocketFlags.None);
                }

                byte[] responseBytes = new byte[256];
                char[] responseChars = new char[256];

                while (true)
                {
                    int bytesReceived = socket.Receive(responseBytes);

                    if (bytesReceived == 0) break;

                    int charCount = Encoding.UTF8.GetChars(responseBytes, 0, bytesReceived, responseChars, 0);

                    Console.Out.Write(responseChars, 0, charCount);
                }
            }
        }

        public async Task SendHttpRequestAsync()
        {
            CancellationToken cancellationToken = default;

            byte[] requestBytes = this.HttpRequestBuilder.Serialize();


            // Create and connect a dual-stack socket
            using Socket socket = new Socket(SocketType.Stream, ProtocolType.Tcp);

            await socket.ConnectAsync(this.Uri.Host, this.Port, cancellationToken);


            // For the tiny amount of data SendAsync() will likely deliver the buffer completely,
            // this is not normal in real world
            int bytesSent = 0;
            while (bytesSent < requestBytes.Length)
            {
                bytesSent += await socket.SendAsync(requestBytes.AsMemory(bytesSent), SocketFlags.None);
            }

            // Do minimalistic buffering assuming ASCII response
            byte[] responseBytes = new byte[256];
            char[] responseChars = new char[256];

            while (true)
            {
                int bytesReceived = await socket.ReceiveAsync(responseBytes, SocketFlags.None, cancellationToken);

                // Receiving 0 bytes means EOF has been reached
                if (bytesReceived == 0) break;

                // Convert byteCount bytes to ASCII characters using the 'responseChars' buffer as destination
                int charCount = Encoding.UTF8.GetChars(responseBytes, 0, bytesReceived, responseChars, 0);

                // Print the contents of the 'responseChars' buffer to Console.Out
                await Console.Out.WriteAsync(responseChars.AsMemory(0, charCount), cancellationToken);
            }
        }
    }
}
