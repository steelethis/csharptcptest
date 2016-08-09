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
            int port = 5556;

            TcpClient client;

            NetworkStream stream;

            try
            {
                client = Connect(serverIP, port);
                stream = client.GetStream();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to get TCP Client, Exception: " + ex.Message);
                return;
            }

            string message = "";

            while (message != "exit()")
            {
                Console.WriteLine("> ");
                message = Console.ReadLine();

                Byte[] data = new byte[256];

                data = System.Text.Encoding.ASCII.GetBytes(message);

                stream.Write(data, 0, data.Length);

                data = new byte[256];
                string responseMessage = "";

                int bytes = stream.Read(data, 0, data.Length);
                responseMessage = System.Text.Encoding.ASCII.GetString(data, 0, bytes);

                Console.WriteLine("Received: " + responseMessage);
            }

            DisconnectFromServer(client, stream);

            Console.ReadKey();
        }

        private static void DisconnectFromServer(TcpClient client, NetworkStream stream)
        {
            Console.WriteLine("Disconnecting");
            client.Close();
            stream.Close();
        }

        private static TcpClient Connect(string serverIP, int port)
        {
            return new TcpClient(serverIP, port);
        }

        private void Connect(string serverIp, string message)
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
