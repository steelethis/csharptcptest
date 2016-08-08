using System;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Xml;

namespace tcpClientDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            string serverIP = IPAddress.Loopback.ToString();
            string message = "Hello";

            Connect(serverIP, message);


            Console.ReadKey();
        }

        private static void Connect(string serverIp, string message)
        {
            string output = "";

            int port = 5556;
            TcpClient client = new TcpClient(IPAddress.Loopback.ToString(), port);

            Byte[] data = new byte[256];

            data = System.Text.Encoding.ASCII.GetBytes(message);

            NetworkStream stream = client.GetStream();

            stream.Write(data, 0, data.Length);

            output = "Sent: " + message;
            Console.WriteLine(output);

            data = new Byte[256];

            String responseData = String.Empty;

            int bytes = stream.Read(data, 0, data.Length);
            responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);

            output = "Received: " + responseData;

            Console.WriteLine(output);

            stream.Close();
            client.Close();
        }
    }
}
