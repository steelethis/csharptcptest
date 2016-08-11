using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace tcpServerDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpListener tcpListener = new TcpListener(IPAddress.Loopback, 5556);

            Socket listenSock = new Socket(SocketType.Stream, ProtocolType.Tcp);

            tcpListener.Start();

            Console.WriteLine("Server Started");

            Console.WriteLine("Waiting for connection...");

            TcpClient client = tcpListener.AcceptTcpClient();

            Console.WriteLine("Connection Accepted " + client.Client.RemoteEndPoint);

            while (client.Connected)
            {
                Thread.Sleep(10);

                byte[] bytes = new byte[256];
                NetworkStream stream = client.GetStream();

                stream.Read(bytes, 0, bytes.Length);
                SocketHelper helper = new SocketHelper();

                helper.processMsg(client, stream, bytes);
            }
        }
    }
}
