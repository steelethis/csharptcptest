using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace tcpServerDemo
{
    class Program
    {
        static TcpListener Listener;
        
        static void Main()
        {
            Listener = new TcpListener(IPAddress.Loopback, 5556);
            Listener.Start();

            Console.WriteLine("Listening on port 5556");

            while (true)
            {
                // Blocking call that waits on a client.
                Console.WriteLine("Waiting on connection...");
                TcpClient client = Listener.AcceptTcpClient();

                Console.WriteLine($"Connection Accepted! {client.Client.RemoteEndPoint}");

                try
                {
                    NetworkStream stream = client.GetStream();

                    StreamReader reader = new StreamReader(stream);
                    StreamWriter writer = new StreamWriter(stream);
                    writer.AutoFlush = true;

                    while (true)
                    {
                        string message = reader.ReadLine();

                        string response = "";
                        if (message == "Hello")
                        {
                            response = "Bye";
                        }
                        else
                        {
                            response = "What?";
                        }

                        writer.WriteLine(response);
                    }
                    stream.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                Console.WriteLine("Disconnected");

                client.Close();
            }
        }
    }
}
