using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace zadanie5
{
    class Program
    {
        static void Main(string[] args)
        {
             class Program
        {
            static void Main(string[] args)
            {
                ThreadPool.QueueUserWorkItem(ThreadProcServer);
                ThreadPool.QueueUserWorkItem(ThreadProcClient);
                ThreadPool.QueueUserWorkItem(ThreadProcClient);
                ThreadPool.QueueUserWorkItem(ThreadProcClient);

                Thread.Sleep(10000);


            }

            static void ThreadProcClient(Object stateInfo)
            {
                //-------------------------------------------------------------------------//
                TcpClient client = new TcpClient();
                client.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 2048));
                //-----------------wysyłanie wiadomosci------------------------------------//

                byte[] message = new ASCIIEncoding().GetBytes("wiadomosc");
                client.GetStream().Write(message, 0, message.Length);


                //-----------------------echo---------------------------------------------//
                /*
                NetworkStream stream = client.GetStream();
                stream.Read(message, 0, message.Length);
                writeConsoleMessage(new ASCIIEncoding().GetString(message), ConsoleColor.Cyan);
                */        
                }

            static void ThreadProcServer(Object stateInfo)
            {
                //TcpClient client = (TcpClient)stateInfo;
                TcpListener server = new TcpListener(IPAddress.Any, 2048);
                server.Start();
                while (true)
                {
                    lock (server)
                    {
                        TcpClient client = server.AcceptTcpClient();
                        byte[] buffer = new byte[128];
                        NetworkStream stream = client.GetStream();
                        stream.Read(buffer, 0, 128);
                        writeConsoleMessage(new ASCIIEncoding().GetString(buffer), ConsoleColor.Red);
                        client.GetStream().Write(buffer, 0, buffer.Length);
                        client.Close();
                    }

                }
            }
            static void writeConsoleMessage(string message, ConsoleColor color)
            {
                Console.ForegroundColor = color;
                Console.WriteLine(message);
                Console.ResetColor();
            }

        }
    }
}
