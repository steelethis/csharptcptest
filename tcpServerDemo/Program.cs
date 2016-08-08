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
            TcpListener serverSocket = new TcpListener(IPAddress.Loopback, 5556);

            TcpClient clientSocket = null;

            serverSocket.Start();

            Console.WriteLine("Server Started");

            while (true)
            {
                Thread.Sleep(10);

                TcpClient tcpClient = serverSocket.AcceptTcpClient();

                byte[] bytes = new byte[256];
                NetworkStream stream = tcpClient.GetStream();

                stream.Read(bytes, 0, bytes.Length);
                SocketHelper helper = new SocketHelper();

                helper.processMsg(tcpClient, stream, bytes);
            }
        }
    }
}
