using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using Net = System.Net;

namespace Tcp
{
    public class TcpListener
    {
        public IPAddress IpAddress { get; private set; }
        public int Port { get; private set; }

        private List<TcpClient> clients = new List<TcpClient>();

        public TcpListener(string ipAddress = "127.0.0.1", int port = 8080)
        {
            this.IpAddress = IPAddress.Parse(ipAddress);
            this.Port = port;
        }

        public void Start()
        {
            var server = new Net.Sockets.TcpListener(IpAddress, Port);
            server.Start();

            while (true)
            {
                using Net.Sockets.TcpClient client = server.AcceptTcpClient();

                NetworkStream stream = client.GetStream();

                try
                {
                    int msb = stream.ReadByte();                // 218 == 0xDA
                    int lsb = stream.ReadByte();             // 173 == 0xAD

                    int magicNum = ((msb << 8) | lsb);

                    if(magicNum == 0xDAAD)
                    {
                        IPEndPoint endpoint = (IPEndPoint)client.Client.RemoteEndPoint;
                        IPAddress blacklistedIp = endpoint.Address;

                    }

                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                    stream.Close();
                    client.Close();
                }
               


            }

        }
    }
}
