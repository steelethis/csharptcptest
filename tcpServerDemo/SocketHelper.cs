using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace tcpServerDemo
{
    public class SocketHelper
    {
        TcpClient mscClient;
        string mstrMessage;
        string mstrResponse;

        byte[] bytesSent;

        public void processMsg(TcpClient client, NetworkStream stream, byte[] bytesReceived)
        {
            mstrMessage = Encoding.ASCII.GetString(bytesReceived, 0, bytesReceived.Length);

            mscClient = client;
            mstrMessage = mstrMessage.Substring(0, 5);
            if (mstrMessage.Equals("Hello"))
            {
                mstrResponse = "Goodbye";
            }
            else
            {
                mstrResponse = "What?";
            }

            bytesSent = Encoding.ASCII.GetBytes(mstrResponse);
            stream.Write(bytesSent, 0, bytesSent.Length);
        }
    }
}
