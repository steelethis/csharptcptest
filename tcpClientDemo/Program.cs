using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Xml;

namespace tcpClientDemo
{
    class Program
    {
        static void Main()
        {
            TcpClient client = new TcpClient(IPAddress.Loopback.ToString(), 5556);

            try
            {
                NetworkStream stream = client.GetStream();
                StreamReader reader = new StreamReader(stream);
                StreamWriter writer = new StreamWriter(stream);
                writer.AutoFlush = true;

                while (true)
                {
                    Console.Write("> ");
                    string message = Console.ReadLine();
                    writer.WriteLine(message);
                    Console.WriteLine(reader.ReadLine());
                }
                stream.Close();
            }
            finally
            {
                client.Close();
            }
        }
    }
}
