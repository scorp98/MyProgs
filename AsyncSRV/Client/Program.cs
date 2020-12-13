using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        public class ObjectState
        {
            public Socket wSocket = null;
            public const int bufferSize = 1024;
            public byte[] buffer = new byte[bufferSize];
            public StringBuilder sb = new StringBuilder();
        }
        public class AsyncSocketListener
        {
            public static ManualResetEvent allCompleted = new ManualResetEvent(false);
            public static void StartListener()
            {
                byte[] bytes = new byte[1024];
                IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ip = ipHost.AddressList[0];
                IPEndPoint localEndPoint = new IPEndPoint(ip, 4343);
                Socket listener = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                try
                {
                    listener.Bind(localEndPoint);
                    listener.Listen(100);
                    while (true)
                    {
                        allCompleted.Reset();
                        Console.WriteLine($"Waitinig for invoming connecting...");
                        listener.BeginAccept(new AsyncCallback(AcceptCallback), listener);
                        allCompleted.WaitOne();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                Console.WriteLine($"press enter to cony");
                Console.ReadLine();
            }

            private static void AcceptCallback(IAsyncResult ar)
            {
                allCompleted.Set();
                Socket listener = (Socket)ar.AsyncState;
                Socket handler = listener.EndAccept(ar);
                ObjectState state = new ObjectState();
                state.wSocket = handler;
                handler.BeginReceive(state.buffer, 0, ObjectState.bufferSize, 0, new AsyncCallback(ReadCallvack));
            }
        }
        static void Main(string[] args)
        {

        }
    }
}
